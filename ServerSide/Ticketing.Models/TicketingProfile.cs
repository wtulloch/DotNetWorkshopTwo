using System;
using System.Globalization;
using AutoMapper;
using Ticketing.Models.DbModels;
using Ticketing.Models.Dtos;

namespace Ticketing.Models
{
    public class TicketingProfile : Profile
    {
        public TicketingProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Production, ProductionDto>();
              
            CreateMap<Show, ShowSummaryDto>()
                .ForMember(d => d.ShowDate, opt => opt.ConvertUsing(new DateToStringFormatter(), src => src.ShowDate));

            CreateMap<Show, ShowDto>()
                .ForMember(d => d.ShowDate, opt => opt.ConvertUsing(new DateToStringFormatter(), src => src.ShowDate))
                .ForMember(d => d.ProductionName, opt => opt.MapFrom(src => src.Production.Name));

            CreateMap<Ticket, TicketDto>();

            CreateMap<User, UserDto>();
            CreateMap<User, RegisterUserDto>()
                .ReverseMap();

        }
    }

    public class DateToStringFormatter : IValueConverter<DateTime, string>
    {
        
        public string Convert(DateTime sourceMember, ResolutionContext context)
        {
           return  sourceMember.ToLocalTime().ToShortDateString();
        }
    }
}