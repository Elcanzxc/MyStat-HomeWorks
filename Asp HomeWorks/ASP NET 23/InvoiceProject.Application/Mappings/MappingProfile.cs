using AutoMapper;
using InvoiceProject.DTO;
using InvoiceProject.Models;
namespace InvoiceProject.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CustomerRequestDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());



        CreateMap<Customer, CustomerResponseDto>();


        CreateMap<CustomerUpdateDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());


        CreateMap<Customer, CustomerDetailsResponseDto>()
            .ForMember(dest => dest.CustomerInvoices, opt => opt.MapFrom(src => src.Invoices));


        CreateMap<InvoiceRequestDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSum, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Models.InvoiceStatus.Created))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.RowsCreate))
            .ForMember(dest => dest.Customer, opt => opt.Ignore());


        CreateMap<Invoice, InvoiceResponseDto>()
        .ForMember(dest => dest.RowsResponse, opt => opt.MapFrom(src => src.Rows));



        CreateMap<InvoiceUpdateDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalSum, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore());


        CreateMap<InvoiceUpdateDto, Invoice>();



        CreateMap<InvoiceRowRequestDto, InvoiceRow>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
          .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Quantity * src.Rate))
          .ForMember(dest => dest.Invoice, opt => opt.Ignore());


        CreateMap<InvoiceRow, InvoiceRowResponseDto>();


        CreateMap<InvoiceRowUpdateDto, InvoiceRow>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
        .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Quantity * src.Rate))
        .ForMember(dest => dest.Invoice, opt => opt.Ignore());



        CreateMap<UserRegister, User>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.Customers,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<UserUpdate, User>()
            .ForMember(dest => dest.UpdatedAt,
                opt => opt.MapFrom(_ => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.Email,
                opt => opt.Ignore())
            .ForMember(dest => dest.UserName,
                opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore())
            .ForMember(dest => dest.Customers,
                opt => opt.Ignore());

        CreateMap<User, UserResponse>();

    
        CreateMap<UserLogin, User>();

        CreateMap<UserPasswordUpdate, User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore());


    }
}
