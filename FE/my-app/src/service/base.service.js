// src/services/base.service.js
import apiClient from "./apiClient";

export class BaseService {
  constructor(baseUrl) {
    if (!baseUrl) {
      throw new Error("Base URL must be provided to the service constructor.");
    }
    this.baseUrl = baseUrl;
  }

  async getAll(config = {}) {
    try {
      const res = await apiClient.get(this.baseUrl, config);
      return res.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  async getById(id, config = {}) {
    try {
      const res = await apiClient.get(`${this.baseUrl}/${id}`, config);
      return res.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  async create(data, config = {}) {
    try {
      const res = await apiClient.post(this.baseUrl, data, config);
      return res.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  async update(id, data, config = {}) {
    try {
      const res = await apiClient.put(`${this.baseUrl}/${id}`, data, config);
      return res.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  async delete(id, config = {}) {
    try {
      const res = await apiClient.delete(`${this.baseUrl}/${id}`, config);
      return res.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  handleError(error) {
    // Ném ra lỗi để component có thể bắt và xử lý
    console.error(`Service error on ${this.baseUrl}:`, error.response || error);
    throw error.response?.data || { message: error.message };
  }
}