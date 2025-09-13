"use client";
import Script from "next/script";
import "@/styles/vendor/fontawesome-free/css/all.min.css";
import "@/styles/css/sb-admin-2.min.css";

export default function AdminLayout({ children }) {
  return (
    <html lang="en">
      <body id="page-top">


        
        <Script src="/vendor/jquery/jquery.min.js" strategy="beforeInteractive" />
        <Script src="/vendor/bootstrap/js/bootstrap.bundle.min.js" strategy="beforeInteractive" />
        <Script src="/vendor/jquery-easing/jquery.easing.min.js" strategy="beforeInteractive" />
        <Script src="/vendor/js/sb-admin-2.min.js" strategy="beforeInteractive" />
        {children}
       
      </body>
    </html>
  );
}