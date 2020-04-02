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