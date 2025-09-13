"use client"

import ScrollTop from "../layout/scroll/page";
import TopBar from "../layout/topbar/page";
import Sidebar from "../layout/sidebar/page";
import Footer from "../layout/footer/page";
import "@/app/globals.css";
import { useEffect, useState } from "react";
import Cookies from "js-cookie"; // Nếu dùng js-cookie
import { CategoryService } from "@/service/category.service"; 

export default function CategoryCreatePage() {
      const [categories, setCategories] = useState([]);
      const[categoryName, setCategoryName] = useState("");
      const[categorySlug, setCategorySlug] = useState("");
      const[parentId, setParentId] = useState(null);
      const[categoryDescription, setCategoryDescription] = useState("");
      const[isChecked, setIsChecked] = useState(false);
      
      const [loading, setLoading] = useState(true);
      const [error, setError] = useState(null);
    
      useEffect(() => {
        const fetchCategories = async () => {
          try {
            const token = Cookies.get("token");
            const service = new CategoryService();
            const data = await service.getAll({
              headers: {
                Authorization: `Bearer ${token}`,
              },
            });
            setCategories(data);
          } catch (err) {
            console.error(err);
            setError("Failed to load categories.");
          } finally {
            setLoading(false);
          }
        };
    
        fetchCategories();
      }, []);
   useEffect(() => {
    const slug = categoryName.trim()
        .toLowerCase()
        .normalize("NFD")                  // tách dấu ra
        .replace(/[\u0300-\u036f]/g, "")   // loại bỏ dấu
        .replace(/\s+/g, "-")              // thay khoảng trắng bằng "-"
        .replace(/[^\w-]+/g, "");          // chỉ giữ chữ, số, dấu "-"
    setCategorySlug(slug);
}, [categoryName]);

const handleSubmit = async (e) => {
    e.preventDefault();
    try {
        const token = Cookies.get("token");
        const service = new CategoryService();
        const payload={
            name: categoryName,
            slug: categorySlug,
            parentId: parentId||null,
            description: categoryDescription,
            isChecked: isChecked
        }
        await service.create({
            name: categoryName,
            slug: categorySlug,
            parentId: document.getElementById("parentId").value,
            description: document.getElementById("categoryDescription").value,
            isChecked: document.getElementById("checkChecked").checked
        }, {
            headers: { Authorization: `Bearer ${token}` }
        });
        alert("Thêm danh mục thành công!");
    } catch (err) {
        console.error(err);
        alert("Thêm danh mục thất bại!");
    }
};

    return (
        <>
            <div id="wrapper">
                <Sidebar />

                <div id="content-wrapper" className="d-flex flex-column">
                    <div id="content">
                        <TopBar />
                        <div className="container-fluid">
                            {/* Example Progress bars */}
                            <div className="row">
                                <div className="col-md-12 mb-4">
                                    <div className="card shadow mb-4">
                                        <div className="card-header py-3">
                                            <h5 className="m-0 font-weight-bold text-primary">
                                                Thêm Mới Danh Mục
                                            </h5>
                                        </div>
                                        <div className="card-body">
                                            <form>
                                                <div className="mb-3">
                                                    <label htmlFor="categoryName" className="form-label">Tên Danh Mục</label>
                                                    <input type="text" className="form-control"  value={categoryName} onChange={(e)=>setCategoryName(e.target.value)} placeholder="Nhập tên danh mục" />
                                                </div>
                                                 <div className="mb-3">
                                                    <label htmlFor="categorySlug" className="form-label">Slug</label>
                                                    <input type="text" className="form-control" value={categorySlug} onChange={(e)=>setCategorySlug(e.target.value)} placeholder="Nhập tên danh mục" readOnly />
                                                </div>
                                                <div className="mb-3">
                                                    <label className="form-label">Chọn Danh mục cha</label>
                                                    <select className="form-select" aria-label="Default select example" value={parentId} onChange={(e)=>setParentId(e.target.value)}>
                                                        <option selected>Chọn danh mục cha</option>
                                                        {categories.map((cate) => (
                                                            <option key={cate.id} value={cate.id}>{cate.name}</option>))}
                                                    </select>
                                                  
                                                </div>
                                                <div className="form-check">
                                                    <input className="form-check-input" type="checkbox" value="" checked="{isChecked}" onChange={(e)=>setIsChecked(e.target.checked)} />
                                                    <label className="form-check-label" for="checkChecked">
                                                        Checked checkbox
                                                    </label>
                                                </div>
                                                
                                                <div className="mb-3">
                                                    <label htmlFor="categoryDescription" className="form-label">Mô Tả</label>
                                                    <textarea className="form-control" value={categoryDescription} onChange={(e)=>setCategoryDescription(e.target.value)} rows="3" placeholder="Nhập mô tả danh mục"></textarea>
                                                </div>
                                               <div className="d-sm-flex align-items-center justify-content-center mb-4">
                                                     <button type="submit" onClick={handleSubmit} className="btn btn-primary mr-2">Thêm mới</button>
                                                      <a href="/admin/category" className="btn btn-secondary">Quay Lại</a>
                                               </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <Footer />
                </div>
            </div>

            <ScrollTop />
        </>
    );
}
