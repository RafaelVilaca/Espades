using AutoMapper;
using Espades.Api.Models;
using Espades.Api.Models.Base;
using Espades.Domain.Entities;
using Espades.Domain.Entities.Base;

namespace Espades.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<Pessoa, PessoaModel>().ReverseMap();
            CreateMap<Endereco, EnderecoModel>().ReverseMap();
            CreateMap<Produto, ProdutoModel>().ReverseMap();
            CreateMap<Setor, SetorModel>().ReverseMap();
            CreateMap<Cargo, CargoModel>().ReverseMap();
            CreateMap<Funcionario, FuncionarioModel>().ReverseMap();
            CreateMap<Patrimonio, PatrimonioModel>().ReverseMap();
            CreateMap<Reserva, ReservaModel>().ReverseMap();
            CreateMap<Despesa, DespesaModel>().ReverseMap();
            CreateMap<Estoque, EstoqueModel>().ReverseMap();
            CreateMap<Cliente, ClienteModel>().ReverseMap();
        }
    }
}
