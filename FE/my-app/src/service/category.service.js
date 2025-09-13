// src/service/category.service.js
import { BaseService } from "./base.service";

export class CategoryService extends BaseService {
  constructor() {
    super("/Category");
  }

  // ví dụ thêm logic riêng
  async getActiveCategories() {
    const categories = await this.getAll();
    return categories.filter(c => !c.trash);
  }
}
