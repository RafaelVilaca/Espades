import { BaseInterface } from "./base.interface/base.interface";
import { ICargo } from "./cargo.interface";

export interface ISetor extends BaseInterface {
    descricao?: string,
    cargos?: ICargo[]
}
