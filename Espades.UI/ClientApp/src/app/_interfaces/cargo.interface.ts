import { BaseInterface } from "./base.interface/base.interface";
import { ISetor } from "./setor.interface";

export interface ICargo extends BaseInterface {
    descricao?: string,
    id_Setor?: number,
    setor?: ISetor
}
