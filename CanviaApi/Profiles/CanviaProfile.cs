using AutoMapper;
using Canvia.Model;
using Canvia.Model.Enums;
using Canvia.Model.Models;
using CanviaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanviaApi.Profiles
{
    public class CanviaProfile : Profile
    {
        public CanviaProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(t => t.Sex, m => m.MapFrom(s => this.ResolveBit(s.Sex)));

            CreateMap<PersonDto, Person>();

            CreateMap<FamilyModel, FamilyDto>();

        }

        private ESex ResolveBit(bool? sex)
        {
            return sex.HasValue ? sex.Value ? ESex.Man : ESex.Woman : ESex.Man;
        }
    }
}