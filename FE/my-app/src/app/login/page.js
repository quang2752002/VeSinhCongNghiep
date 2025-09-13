// app/login/page.js
'use client';

// 1. Import useState từ React
import { useState } from 'react'; 
import { authService } from '@/service/auth.service';
import toast from 'react-hot-toast';
import GoogleLoginButton from '../googleLoginButton/page'; 

export default function LoginPage() {
  // 2. Tạo state để quản lý username và password
  // Khởi tạo state với giá trị mặc định để tiện cho việc test
  const [username, setUsername] = useState('admin');
  const [password, setPassword] = useState('Admin@123');

  const handleLogin = async (event) => {
    event.preventDefault();
    
    // 4. Sử dụng trực tiếp các biến state thay vì đọc từ event.target
    // const username = event.target.username.value; // Dòng này không cần nữa
    // const password = event.target.password.value; // Dòng này không cần nữa

    const promise = authService.login(username, password);

    toast.promise(promise, {
      loading: 'Logging in...',
      success: (data) => {
        if (data && data.roles && data.roles.includes('Admin')) {
          window.location.href = '/admin/category'; 
        } else {
          window.location.href = '/';
        }
        
        // Vẫn sử dụng biến state 'username' ở đây
        return `Welcome back, ${username}!`; 
      },
      error: (err) => err.message || 'Login failed. Please check your credentials.',
    });
  };

  return (
    <div style={styles.container}>
      <div style={styles.loginBox}>
        <h2 style={styles.title}>Sign In</h2>
        <p style={styles.subtitle}>Welcome back to QuanLyBanHang</p>
        
        <form onSubmit={handleLogin} style={styles.form}>
          <div style={styles.inputGroup}>
            <label htmlFor="username" style={styles.label}>Username</label>
            {/* 3. Liên kết input với state */}
            <input 
              type="text" 
              id="username" 
              name="username" 
              required 
              style={styles.input} 
              value={username} // Thay defaultValue bằng value
              onChange={(e) => setUsername(e.target.value)} // Cập nhật state khi người dùng gõ
              suppressHydrationWarning 
            />
          </div>
          <div style={styles.inputGroup}>
            <label htmlFor="password" style={styles.label}>Password</label>
            {/* 3. Liên kết input với state */}
            <input 
              type="password" 
              id="password" 
              name="password" 
              required 
              style={styles.input} 
              value={password} // Thay defaultValue bằng value
              onChange={(e) => setPassword(e.target.value)} // Cập nhật state khi người dùng gõ
              suppressHydrationWarning
            />
          </div>
          <button type="submit" style={styles.button}>Login</button>
        </form>

        <div style={styles.divider}>
          <span>OR</span>
        </div>
        
        <div style={styles.googleButtonContainer}>
           <GoogleLoginButton />
        </div>
      </div>
    </div>
  );
}

// Các styles không thay đổi
const styles = {
  container: { display: 'flex', alignItems: 'center', justifyContent: 'center', minHeight: '100vh', background: '#f0f2f5' },
  loginBox: { background: 'white', padding: '40px', borderRadius: '8px', boxShadow: '0 4px 12px rgba(0,0,0,0.1)', width: '100%', maxWidth: '400px', textAlign: 'center' },
  title: { margin: 0, marginBottom: '10px', fontSize: '24px' },
  subtitle: { margin: 0, marginBottom: '30px', color: '#666' },
  form: { display: 'flex', flexDirection: 'column', gap: '15px' },
  inputGroup: { textAlign: 'left' },
  label: { marginBottom: '5px', display: 'block', fontWeight: 'bold', color: '#333' },
  input: { width: '100%', padding: '10px', borderRadius: '4px', border: '1cpx solid #ddd', boxSizing: 'border-box' },
  button: { padding: '12px', borderRadius: '4px', border: 'none', background: '#007bff', color: 'white', fontWeight: 'bold', cursor: 'pointer', marginTop: '10px' },
  divider: { display: 'flex', alignItems: 'center', textAlign: 'center', margin: '20px 0', color: '#aaa' },
  googleButtonContainer: { display: 'flex', justifyContent: 'center' },
};