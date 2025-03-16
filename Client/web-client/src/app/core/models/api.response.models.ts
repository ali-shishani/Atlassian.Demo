import { Pagination } from "./pagination.models"
import { Error } from "./error.models";

export interface IAPIResponse<P = any> {
  data: P,
  errors: Error[],
  pagination: Pagination,
}