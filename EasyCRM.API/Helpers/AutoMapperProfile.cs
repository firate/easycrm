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
            #region Account
            CreateMap<Account, AccountToReturnDTO>()
                .ForMember(d => d.AccountType, opt => opt.MapFrom(src => src.AccountType.Name))
                .ForMember(d => d.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(d => d.Groups,
                            opt => opt.MapFrom(src => src.AccountGroups.Select(ag => ag.Group))
                           );

            CreateMap<AccountCreationDTO, Account>();
            CreateMap<AccountEditDTO, Account>()
                .ForMember(a => a.AccountId, opt => opt.Ignore())
                .ForMember(a => a.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.AccountGroups, opts =>
                    opts.MapFrom(ae => ae.GroupIds.Select(id => new AccountGroup
                    {
                        AccountId=ae.AccountId,
                        GroupId = id
                    }))); 
            #endregion

            #region Contact
            CreateMap<Contact, ContactReturnDTO>();
            CreateMap<ContactCreationDTO, Contact>();
            CreateMap<ContactEditDTO, Contact>();
            #endregion


            #region Address
            CreateMap<AddressCreationDTO, Address>();
            CreateMap<AddressEditDTO, Address>();
            CreateMap<Address, AddressReturnDTO>()
                 .ForMember(d => d.Account, opt => opt.MapFrom(src => src.Account.OrganizationName))
                 .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country.Name));
            #endregion

            #region Product
            CreateMap<ProductCreationDTO, Product>();
            CreateMap<ProductEditDTO, Product>();
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(d => d.Currency, opt => opt.MapFrom(src => src.Currency.Code))
                .ForMember(d => d.UnitCode, opt => opt.MapFrom(src => src.UnitCode.Code));
            #endregion

            #region CommunicationInfo
            CreateMap<CommInfoCreationDTO, CommunicationInfo>();
            CreateMap<CommunicationInfo, CommInfoReturnDTO>();
            #endregion

            #region Group
            CreateMap<Group, GroupReturnDTO>();
            #endregion

        }
    }
}
