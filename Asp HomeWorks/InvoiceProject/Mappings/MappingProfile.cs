using AutoMapper;
using InvoiceProject.DataAccess.Entities;
using InvoiceProject.DTO.Customer;
using InvoiceProject.DTO.Invoice;
using InvoiceProject.Models;

namespace InvoiceProject.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CustomerEntity, Customer>()
            .ReverseMap();

        CreateMap<InvoiceEntity, Invoice>()
            .ReverseMap();

        CreateMap<InvoiceRowEntity, InvoiceRow>()
            .ReverseMap();




        CreateMap<InvoiceEntity, InvoiceResponseDto>();


        CreateMap<CustomerRequestDto, CustomerEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());



        CreateMap<CreateInvoiceRequestDto, InvoiceEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Models.InvoiceStatus.Created))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore());
    }
}
