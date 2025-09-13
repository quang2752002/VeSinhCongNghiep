// "use client";

// import { useEffect, useState } from "react";
// import { CategoryService } from "@/service/category.service";
// import Cookies from "js-cookie";
// import Sidebar from "../layout/sidebar/page";
// import TopBar from "../layout/topbar/page";
// import Footer from "../layout/footer/page";
// import ScrollTop from "../layout/scroll/page";


// export default function CategoryPage() {
//   const [categories, setCategories] = useState([]);
//   const [loading, setLoading] = useState(true);
//   const [error, setError] = useState(null);

//   useEffect(() => {
//     const fetchCategories = async () => {
//       try {
//         const token = Cookies.get("token");
//         const service = new CategoryService();
//         const data = await service.getAll({
//           headers: {
//             Authorization: `Bearer ${token}`,
//           },
//         });
//         setCategories(data);
//       } catch (err) {
//         console.error(err);
//         setError("Failed to load categories.");
//       } finally {
//         setLoading(false);
//       }
//     };

//     fetchCategories();
//   }, []);

//   if (loading) return <p>Loading categories...</p>;
//   if (error) return <p>{error}</p>;

//   return (
//     <>
//       <div id="wrapper">
//         <Sidebar />

//         <div id="content-wrapper" className="d-flex flex-column">
//           <div id="content">
//             <TopBar />

//             <div className="container-fluid">
//               {/* Dashboard header */}
//              <div className="d-sm-flex align-items-center justify-content-between mb-4">
//                        <a href="/" className="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm">Thêm mới</a>
//                         <a href="#" className="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"><i
//                                 className="fas fa-download fa-sm text-white-50"></i> Generate Report</a>
//                     </div>
//               {/* Example Progress bars */}
//               <div className="row">
//                 <div className="col-md-12 mb-4">
//                   <div className="card shadow mb-4">
//                     <div className="card-header py-3">
//                       <h4 className="m-0 font-weight-bold text-primary">
//                         Danh Sách Danh Mục
//                       </h4>
//                     </div>
//                     <div className="card-body">
//                       <div className="table-responsive">
//                         <table className="table table-bordered" id="dataTable" width="100%" cellSpacing="0">
//                           <thead>
//                             <tr>
//                               <th> STT </th>
//                               <th>Tên Danh Mục</th>
//                               <th>Trạng Thái</th>
                             
//                               <th>Chức năng</th>
//                             </tr>
//                           </thead>

//                           <tbody>
//                             { categories.map((cate,index)=>{
//                               return(
//                                 <tr key={cate.id}>
//                                   <td>{index+1}</td>
//                                   <td><a href="#">{cate.name}</a></td>
                                 
//                                   <td>
//                                     {cate.state == "APPROVED"
//                                       ? "Đã duyệt"
//                                       // : cate.state == "REJECTED"
//                                       // ? "Từ chối"
//                                       : cate.state == "PENDING"
//                                       ? "Chờ duyệt"
//                                       : "Không xác định"}
//                                   </td>
//                                   <td>
//                                   <a href="" className={`btn ${cate.state === "APPROVED" ? "btn-secondary" : "btn-success"} btn-icon-split`}>
//                                             <span className="icon text-white-50">
//                                               {cate.state === "APPROVED" ? "✖" : "✔"}
//                                             </span>
                                            
//                                           </a>

//                                           <a href="" className="btn btn-warning btn-icon-split mx-2">
//                                             <span className="icon text-white-50">
//                                               ✎
//                                             </span>
                                            
//                                           </a>

//                                           <a href="" className="btn btn-danger btn-icon-split">
//                                             <span className="icon text-white-50">
//                                               <i className="fas fa-trash"></i>
//                                             </span>
                                           
//                                           </a>


//                                   </td>
//                                 </tr>
//                               )
//                             })
//                             }
                           
//                           </tbody>
//                         </table>
//                       </div>
//                     </div>
//                   </div>
//                 </div>
//               </div>
//             </div>
//           </div>
//           <Footer />
//         </div>
//       </div>

//       <ScrollTop />
//     </>
//   );
// }
// app/products/page.js
'use client';

import { useEffect, useState } from 'react';


export default function CategoryPage() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await api.get('/category'); 
        setProducts(response.data);
      } catch (err) {
        setError('Failed to fetch products. You may not be authorized.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  return (
    <div>
      <h1>Our Products</h1>
      <ul>
        {products.map((product) => (
          <li key={product.id}>{product.name} - ${product.price}</li>
        ))}
      </ul>
    </div>
  );
}

