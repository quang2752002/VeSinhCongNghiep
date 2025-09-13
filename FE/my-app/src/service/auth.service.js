// src/services/auth.service.js
import apiClient from "./apiClient";
import { setAuthTokens, clearAuthTokens } from "../utils/tokenManager";

class AuthService {
  async login(username, password) {
    try {
      const response = await apiClient.post('/auth/login', { username, password });
      
      // Sau khi đăng nhập thành công, lưu token
      if (response.data && response.data.token) {
        setAuthTokens(response.data.token, response.data.refreshToken);
      }
      
      return response.data;
    } catch (error) {
      console.error("Login failed:", error.response?.data || error.message);
      throw error.response?.data || { message: "Login failed" };
    }
  }

  async register(userData) {
    try {
        const response = await apiClient.post('/auth/register', userData);
        return response.data;
    } catch (error) {
        console.error("Registration failed:", error.response?.data || error.message);
        throw error.response?.data || { message: "Registration failed" };
    }
  }

    async loginWithGoogle(idToken) {
    try {
      const response = await apiClient.post('/auth/google-signin', { idToken });
      
      if (response.data && response.data.token) {
        setAuthTokens(response.data.token, response.data.refreshToken);
      }
      
      return response.data;
    } catch (error) {
      console.error("Google login failed:", error.response?.data || error.message);
      throw error.response?.data || { message: "Google login failed" };
    }
  }
  
  async logout() {
    try {
      // Gọi API để vô hiệu hóa refresh token trên server
      await apiClient.post('/auth/revoke');
    } catch (error) {
      // Thậm chí nếu API thất bại (ví dụ: mất mạng), chúng ta vẫn phải xóa token ở client
      console.error("Failed to revoke token on server, but logging out locally.", error);
    } finally {
      // Luôn xóa token khỏi localStorage
      clearAuthTokens();
      // Chuyển hướng người dùng về trang đăng nhập
      window.location.href = '/login';
    }
  }
}

// Xuất ra một instance duy nhất (singleton pattern)
export const authService = new AuthService();