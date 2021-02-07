using AutoMapper;
using EasyCRM.API.DTOs;
using EasyCRM.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountToReturnDTO>()
                .ForMember(d=>d.AccountType, opt=>opt.MapFrom(src=>src.AccountType.Name))
                .ForMember(d=>d.Contacts, opt=>opt.MapFrom(src=>src.Contacts));
            CreateMap<AccountCreationDTO, Account>();
            CreateMap<AccountEditDTO, Account>()
                .ForMember(a=>a.AccountId, opt=>opt.Ignore())
                .ForMember(a=>a.CreatedAt,opt=>opt.Ignore());
            CreateMap<Contact, ContactReturnDTO>();
            CreateMap<ContactCreationDTO, Contact>();
        }
    }
}
