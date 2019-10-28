import { BaseInterface } from "./base.interface/base.interface";

export interface ICliente extends BaseInterface {
    cnpj?: string,
    nome?: string,
    nome_Fantasia?: string
}
