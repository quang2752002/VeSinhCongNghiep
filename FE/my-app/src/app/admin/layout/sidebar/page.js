"use client";
import { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "@/styles/vendor/fontawesome-free/css/all.min.css";
import "@/styles/css/sb-admin-2.min.css";

export default function AdminLayout({ children }) {
  const [collapseOpen, setCollapseOpen] = useState({
    components: false,
    utilities: false,
    pages: false,
  });
  const [sidebarToggled, setSidebarToggled] = useState(false);

  const toggleCollapse = (key) => setCollapseOpen((prev) => ({ ...prev, [key]: !prev[key] }));
  const toggleSidebar = () => setSidebarToggled(!sidebarToggled);

  return (
    <div className="d-flex" id="wrapper" style={{ minHeight: "100vh" }}>
      {/* Sidebar */}
      <ul
        className={`navbar-nav bg-gradient-primary sidebar sidebar-dark accordion ${sidebarToggled ? "toggled" : ""
          }`}
        id="accordionSidebar"
        style={{ minHeight: "100vh" }}
      >
        {/* Brand */}
        <a className="sidebar-brand d-flex align-items-center justify-content-center" href="#top">
          <div className="sidebar-brand-icon rotate-n-15">
            <i className="fas fa-laugh-wink"></i>
          </div>
          <div className="sidebar-brand-text mx-3">SB Admin <sup>2</sup></div>
        </a>

        <hr className="sidebar-divider my-0" />

        {/* Dashboard */}
        <li className="nav-item active">
          <a className="nav-link" href="index.html">
            <i className="fas fa-fw fa-tachometer-alt"></i>
            <span>Dashboard</span>
          </a>
        </li>

        <hr className="sidebar-divider" />
        <div className="sidebar-heading">Interface</div>

        {/* Components */}
        <li className="nav-item">
          <a
            className="nav-link d-flex justify-content-between align-items-center"
            style={{ cursor: "pointer" }}
            onClick={() => toggleCollapse("components")}
          >
            <span>
              <i className="fas fa-fw fa-cog"></i> Chức năng
            </span>

          </a>
          <div className={`collapse ${collapseOpen.components ? "show" : ""}`}>
            <div className="bg-white py-2 collapse-inner rounded">
              <h6 className="collapse-header">Custom Components:</h6>
              <a className="collapse-item" href="buttons.html">Buttons</a>
              <a className="collapse-item" href="/admin/category">Danh Mục</a>
            </div>
          </div>
        </li>

        {/* Utilities */}
        <li className="nav-item">
          <a className="nav-link" style={{ cursor: "pointer" }} onClick={() => toggleCollapse("utilities")}>
            <i className="fas fa-fw fa-wrench"></i>
            <span>Utilities</span>
          </a>
          <div className={`collapse ${collapseOpen.utilities ? "show" : ""}`}>
            <div className="bg-white py-2 collapse-inner rounded">
              <h6 className="collapse-header">Custom Utilities:</h6>
              <a className="collapse-item" href="utilities-color.html">Colors</a>
              <a className="collapse-item" href="utilities-border.html">Borders</a>
              <a className="collapse-item" href="utilities-animation.html">Animations</a>
              <a className="collapse-item" href="utilities-other.html">Other</a>
            </div>
          </div>
        </li>

        <hr className="sidebar-divider" />
        <div className="sidebar-heading">Addons</div>

        {/* Pages */}
        <li className="nav-item">
          <a className="nav-link" style={{ cursor: "pointer" }} onClick={() => toggleCollapse("pages")}>
            <i className="fas fa-fw fa-folder"></i>
            <span>Pages</span>
          </a>
          <div className={`collapse ${collapseOpen.pages ? "show" : ""}`}>
            <div className="bg-white py-2 collapse-inner rounded">
              <h6 className="collapse-header">Login Screens:</h6>
              <a className="collapse-item" href="login.html">Login</a>
              <a className="collapse-item" href="register.html">Register</a>
              <a className="collapse-item" href="forgot-password.html">Forgot Password</a>
              <div className="collapse-divider"></div>
              <h6 className="collapse-header">Other Pages:</h6>
              <a className="collapse-item" href="404.html">404 Page</a>
              <a className="collapse-item" href="blank.html">Blank Page</a>
            </div>
          </div>
        </li>

        {/* Charts */}
        <li className="nav-item">
          <a className="nav-link" href="charts.html">
            <i className="fas fa-fw fa-chart-area"></i>
            <span>Charts</span>
          </a>
        </li>

        {/* Tables */}
        <li className="nav-item">
          <a className="nav-link" href="tables.html">
            <i className="fas fa-fw fa-table"></i>
            <span>Tables</span>
          </a>
        </li>

        <hr className="sidebar-divider d-none d-md-block" />
        <div className="text-center d-none d-md-inline">
          <button className="rounded-circle border-0" onClick={toggleSidebar}></button>
        </div>
      </ul>

      {/* Page Content */}
      <div id="content-wrapper" className="flex-fill">
        {children}
      </div>
    </div>
  );
}
