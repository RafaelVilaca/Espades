import { BaseInterface } from "./base.interface/base.interface";

export interface IDespesa extends BaseInterface {
    data_Despesa?: Date,
    valor?: number,
    local?: string,
    descricao?: string
}
