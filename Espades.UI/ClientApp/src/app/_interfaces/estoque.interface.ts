import { BaseInterface } from "./base.interface/base.interface";
import { IProduto } from "./produto.interface";

export interface IEstoque extends BaseInterface {
    descricao?: string,
    localizacao?: string,
    data_Compra?: Date,
    quantidade?: number,
    valor?: number,
    id_Produto?: number,
    produto?: IProduto
}
