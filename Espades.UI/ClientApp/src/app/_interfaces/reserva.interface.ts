import { BaseInterface } from "./base.interface/base.interface";
import { IProduto } from "./produto.interface";
import { ICliente } from "./cliente.interface";

export interface IReserva extends BaseInterface {
    descricao?: string,
    id_Cliente?: number,
    id_Produto?: number,
    quantidade?: number,
    data_Final_Reserva?: Date,
    produto?: IProduto,
    cliente?: ICliente
}
