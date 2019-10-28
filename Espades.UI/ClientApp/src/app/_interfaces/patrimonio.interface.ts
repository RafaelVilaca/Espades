import { BaseInterface } from "./base.interface/base.interface";

export interface IPatrimonio extends BaseInterface {
    descricao?: string,
    situacao?: string,
    data_Compra?: Date,
    valor?: number
}
