// src/services/product.service.js
import { BaseService } from "./base.service";

class ProductService extends BaseService {
  constructor() {
   
    super('product'); 
  }

  
  async search(query) {
    try {
      const response = await this.getAll({ params: { q: query } });
      return response;
    } catch (error) {
      // handleError đã được định nghĩa trong BaseService
      this.handleError(error);
    }
  }
}

export const productService = new ProductService();