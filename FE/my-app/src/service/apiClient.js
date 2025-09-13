// src/services/apiClient.js
import axios from 'axios';
import { getAccessToken, getRefreshToken, setAuthTokens, clearAuthTokens } from '../utils/tokenManager';

let isRefreshing = false;
let failedQueue = [];

const processQueue = (error, token = null) => {
  failedQueue.forEach(prom => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });
  failedQueue = [];
};

const apiClient = axios.create({
  // Sử dụng biến môi trường cho baseURL là tốt nhất
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7248/api', 
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request Interceptor: Tự động thêm token vào header
apiClient.interceptors.request.use(
  (config) => {
    const token = getAccessToken();
    if (token) {
      config.headers['Authorization'] = 'Bearer ' + token;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response Interceptor: Xử lý khi token hết hạn hoặc các lỗi khác
apiClient.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    const originalRequest = error.config;

    // Chỉ xử lý lỗi 401 (Unauthorized) do token hết hạn
    if (error.response?.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        return new Promise(function (resolve, reject) {
          failedQueue.push({ resolve, reject });
        })
          .then(token => {
            originalRequest.headers['Authorization'] = 'Bearer ' + token;
            return apiClient(originalRequest);
          })
          .catch(err => {
            return Promise.reject(err);
          });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      const currentRefreshToken = getRefreshToken();
      if (!currentRefreshToken) {
         window.location.href = '/login';
         return Promise.reject(error);
      }
      
      try {
        // Quan trọng: Sử dụng axios.create() hoặc instance mới để tránh vòng lặp vô tận
        const response = await axios.post(`${apiClient.defaults.baseURL}/auth/refresh-token`, {
          accessToken: getAccessToken(), 
          refreshToken: currentRefreshToken,
        });

        const { token: newAccessToken, refreshToken: newRefreshToken } = response.data;
        
        setAuthTokens(newAccessToken, newRefreshToken); // Lưu token mới
        
        // Cập nhật header mặc định cho các request sau
        apiClient.defaults.headers.common['Authorization'] = 'Bearer ' + newAccessToken;
        // Cập nhật header cho request hiện tại
        originalRequest.headers['Authorization'] = 'Bearer ' + newAccessToken;
        
        processQueue(null, newAccessToken);
        return apiClient(originalRequest);

      } catch (_error) {
        processQueue(_error, null);
        clearAuthTokens(); // Xóa token cũ
        console.error("Refresh token failed, logging out.", _error);
        window.location.href = '/login'; // Chuyển hướng về trang đăng nhập
        return Promise.reject(_error);
      } finally {
        isRefreshing = false;
      }
    }

    // Nếu lỗi là 403 (Forbidden), cũng nên đăng xuất người dùng
    if (error.response?.status === 403) {
      clearAuthTokens();
      window.location.href = '/login';
    }

    return Promise.reject(error);
  }
);

export default apiClient;