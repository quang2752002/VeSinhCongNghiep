'use client';
import { GoogleLogin } from '@react-oauth/google';
import { authService } from '@/service/auth.service'; 
import toast from 'react-hot-toast';
export default function GoogleLoginButton() {
  const handleGoogleSuccess = async (credentialResponse) => {
    const idToken = credentialResponse.credential;
    if (!idToken) {
      toast.error("Failed to get Google ID token.");
      return;
    }

    const promise = authService.loginWithGoogle(idToken);

    toast.promise(promise, {
      loading: 'Verifying with Google...',
      success: (data) => {
        // === THÊM LOGIC CHUYỂN HƯỚNG TƯƠNG TỰ Ở ĐÂY ===
        if (data && data.roles && data.roles.includes('Admin')) {
          window.location.href = '/admin/category';
        } else {
          window.location.href = '/';
        }

        return 'Successfully logged in with Google!';
      },
      error: (err) => err.message || 'Google login failed.',
    });
  };

  const handleGoogleError = () => {
    toast.error('Google login was cancelled or failed.');
  };

  return (
    <GoogleLogin
      onSuccess={handleGoogleSuccess}
      onError={handleGoogleError}
    />
  );
}