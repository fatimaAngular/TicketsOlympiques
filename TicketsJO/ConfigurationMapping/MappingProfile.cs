using AutoMapper;
using TicketsJO.Models;
using TicketsJO.ViewModels;
using Microsoft.AspNetCore.Identity;



namespace TicketsJO.ConfigurationMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventViewModel>()
                .ForMember(dest => dest.DisciplineName, opt => opt.MapFrom(src =>
                src.Discipline.Name))
                .ForMember(dest => dest.IdDiscipline, opt => opt.MapFrom(src =>
                src.Discipline.ID))
                  .ForMember(dest => dest.StatutEventName, opt => opt.MapFrom(src =>
                src.StatutEvent.Name))
                .ForMember(dest => dest.IDStatutEvent, opt => opt.MapFrom(src =>
                src.StatutEvent.Id))
                .ReverseMap();

            CreateMap<Discipline, DisciplineViewModel>()
                .ReverseMap();

            CreateMap<StatutEvent, StatutEventViewModel>()
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ReverseMap();


            //CreateMap<Offre, OffreViewModel>()
            //    .ForMember(dest => dest.EventName, opt => opt.MapFrom(src =>
            //    src.Events.Name))
            //    .ForMember(dest => dest.EventId, opt => opt.MapFrom(src =>
            //    src.Events.Id))
            //    .ReverseMap();


        }
    }
}
