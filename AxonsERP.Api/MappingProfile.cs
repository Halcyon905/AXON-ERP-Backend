﻿using AutoMapper;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaxRateControlForCreate, TaxRateControlDto>();
            CreateMap<TaxRateControlForUpdate, TaxRateControlDto>();

            CreateMap<BillCollectionDate, BillCollectionDateToReturn>();

            CreateMap<BillCollectionDateForUpdate, BillCollectionDateForUpdateDto>();
            CreateMap<BillCollectionDateForUpdate, BillCollectionDateForSingleCustomer>();
            CreateMap<BillCollectionDateForUpdate, BillCollectionDateForGetSingle>();

            CreateMap<BillCollectionDateForCreate, BillCollectionDateForCreateDto>();
            CreateMap<BillCollectionDateForCreate, BillCollectionDateForGetSingle>();

            CreateMap<ConvertToGLForCreate, ConvertToGLForGetSingle>();
        }
    }
}
