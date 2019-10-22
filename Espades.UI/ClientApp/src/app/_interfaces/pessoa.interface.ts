import { BaseInterface } from "./base.interface/base.interface";
import { IEndereco } from "./endereco.interface";

export interface IPessoa extends BaseInterface {
  nome?: string,
  email?: string,
  telefone?: string,
  sexo?: string,
  cpf?: string,
  login?: string,
  senha?: string,
  enderecos?: IEndereco[]
}
