// app/admin/products/page.js  (Ví dụ về đường dẫn)
'use client'; // Đánh dấu đây là Client Component

import { useEffect, useState, useCallback } from 'react';
import { productService } from '@/service/product.service';

export default function ProductsPage() {
  // State
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingProduct, setEditingProduct] = useState(null); // null: tạo mới, object: chỉnh sửa

  // Fetch data
  const fetchProducts = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await productService.getAll();
      setProducts(data);
    } catch (err) {
      console.error("Could not fetch products:", err.message);
      setError("Failed to load products. You might not be authorized.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  // CRUD Handlers
  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this product?")) {
      try {
        await productService.delete(id);
        // Cập nhật UI ngay lập tức bằng cách lọc ra sản phẩm đã xóa
        setProducts(products.filter(p => p.id !== id));
      } catch (err) {
        alert(`Error deleting product: ${err.message}`);
      }
    }
  };

  const handleEdit = (product) => {
    setEditingProduct(product);
  };

  const handleSave = async (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const productData = Object.fromEntries(formData.entries());
    // Chuyển đổi các giá trị sang kiểu số
    productData.price = parseFloat(productData.price);
    productData.stockQuantity = parseInt(productData.stockQuantity, 10);
    productData.categoryId = parseInt(productData.categoryId, 10);
    
    try {
      if (editingProduct && editingProduct.id) {
        // Update
        await productService.update(editingProduct.id, { ...productData, id: editingProduct.id });
      } else {
        // Create
        await productService.create(productData);
      }
      setEditingProduct(null); // Đóng form
      await fetchProducts(); // Tải lại danh sách
    } catch (err) {
      alert(`Error saving product: ${err.message}`);
    }
  };

  // Render Logic
  if (loading) return <p>Loading products...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  return (
    <div>
      <h1>Product Management</h1>

      {/* Form tạo/chỉnh sửa */}
      <div style={{ border: '1px solid #ccc', padding: '1rem', marginBottom: '1rem' }}>
        <h3>{editingProduct ? 'Edit Product' : 'Add New Product'}</h3>
        <form onSubmit={handleSave}>
          <input type="hidden" name="id" defaultValue={editingProduct?.id || ''} />
          <div>
            <label>Name: </label>
            <input name="name" required defaultValue={editingProduct?.name || ''} />
          </div>
          <div>
            <label>Description: </label>
            <input name="description" defaultValue={editingProduct?.description || ''} />
          </div>
          <div>
            <label>Price: </label>
            <input type="number" name="price" step="0.01" required defaultValue={editingProduct?.price || ''} />
          </div>
          <div>
            <label>Stock: </label>
            <input type="number" name="stockQuantity" required defaultValue={editingProduct?.stockQuantity || ''} />
          </div>
          <div>
            <label>Image URL: </label>
            <input name="imageUrl" defaultValue={editingProduct?.imageUrl || ''} />
          </div>
          <div>
            <label>Category ID: </label>
            <input type="number" name="categoryId" required defaultValue={editingProduct?.categoryId || '1'} />
          </div>
          <button type="submit">{editingProduct ? 'Update' : 'Create'}</button>
          {editingProduct && <button type="button" onClick={() => setEditingProduct(null)}>Cancel</button>}
        </form>
      </div>

      {/* Bảng danh sách sản phẩm */}
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Price</th>
            <th>Stock</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>${product.price}</td>
              <td>{product.stockQuantity}</td>
              <td>
                <button onClick={() => handleEdit(product)}>Edit</button>
                <button onClick={() => handleDelete(product.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}