﻿using AutoMapper;
using Calculator.BLL.Model;
using Calculator.DAL.Entity;
using Calculator.WebAPI.ViewModel;

namespace Calculator.WebAPI.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OperationViewModel, Operation>();
            CreateMap<OperationResult, OperationResultViewModel>();

            CreateMap<Operation, OperationResult>();


            CreateMap<OperationResult, Expression>();
                // .ForMember(dest => dest.OperationType, source => source.MapFrom(res => res.OperationType));

            CreateMap<Expression, OperationResult>();
                // .ForMember(dest => dest.OperationType, source => source.MapFrom(res => res.OperationType));
        }
    }
}