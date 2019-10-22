import { BaseInterface } from "./base.interface/base.interface";

export interface IEndereco extends BaseInterface {
  rua?: string,
  cep?: string,
  numero?: number,
  complemento?: string,
  cidade?: string,
  estado?: string,
  id_Pessoa?: number
}
