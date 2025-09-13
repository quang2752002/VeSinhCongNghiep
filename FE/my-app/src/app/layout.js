// app/layout.js
'use client';

import { Toaster } from 'react-hot-toast';
import { GoogleOAuthProvider } from '@react-oauth/google';

export default function RootLayout({ children }) {
  // Lấy Client ID từ biến môi trường.
  // Tạo file .env.local ở thư mục gốc của project Next.js và thêm dòng:
  // NEXT_PUBLIC_GOOGLE_CLIENT_ID="YOUR_CLIENT_ID_FROM_GOOGLE.apps.googleusercontent.com"
  const googleClientId = process.env.NEXT_PUBLIC_GOOGLE_CLIENT_ID;

  if (!googleClientId) {
    console.error("FATAL: NEXT_PUBLIC_GOOGLE_CLIENT_ID is not defined in .env.local");
    // Có thể hiển thị một trang lỗi ở đây
  }

  return (
    <html lang="en">
      <body id="page-top">
        <GoogleOAuthProvider clientId={googleClientId}>
          {children}
        </GoogleOAuthProvider>
        
        <Toaster
          position="top-right"
          toastOptions={{ /* ... các options của bạn ... */ }}
        />
      </body>
    </html>
  );
}