import { BaseInterface } from "./base.interface/base.interface";
import { IPessoa } from "./pessoa.interface";
import { ICargo } from "./cargo.interface";

export interface IFuncionario extends BaseInterface {
    id_Pessoa?: number,
    id_Cargo?: number,
    salario?: number,
    pessoa?: IPessoa,
    cargo?: ICargo
}
