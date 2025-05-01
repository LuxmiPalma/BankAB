using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerFormViewModel, Customer>().ReverseMap();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.Name,
                           opt => opt.MapFrom(src => src.Givenname + " " + src.Surname));
            CreateMap<AccountsViewModel, Account>().ReverseMap();


        }
    }
}
