using AutoMapper;
using Calculator.BLL.Model;
using Calculator.DAL.Entity;
using Calculator.WebAPI.ViewModel;

namespace Calculator.WebAPI.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OperationResult, OperationResult>();

            CreateMap<OperationResult, Expression>();

            CreateMap<Expression, OperationResult>();
        }
    }
}