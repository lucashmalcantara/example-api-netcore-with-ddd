using AutoMapper;
using DddExample.Api.V1.Models.CustomerContext;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.ValueObjects;

namespace DddExample.Api.V1.Mapping
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<Customer, CustomerGetModel>()
                .ForMember(dest => dest.FirstName, m => m.MapFrom(src => src.Name.FirstName))
                .ForMember(dest => dest.LastName, m => m.MapFrom(src => src.Name.LastName))
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.ToString()))
                .ForMember(dest => dest.PhoneAreaCode, m => m.MapFrom(src => src.Phone.AreaCode))
                .ForMember(dest => dest.PhoneNumber, m => m.MapFrom(src => src.Phone.Number))
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.Email.ToString()));

            CreateMap<CustomerPostModel, Customer>()
                .ConstructUsing(src =>
                    new Customer(
                        new PersonName(src.FirstName, src.LastName),
                        new Cpf(src.Cpf),
                        new Phone(src.PhoneAreaCode, src.PhoneNumber),
                        new Email(src.Email),
                       src.Birthdate)
                    ); ;
        }
    }
}
