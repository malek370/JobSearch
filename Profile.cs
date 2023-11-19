using AutoMapper;
using JobSearch.DTOs.FieldDTOs;
using JobSearch.DTOs.OfferDTOs;
using JobSearch.Models;

namespace JobSearch
{
    public class Automapper:Profile
    {
        public Automapper()
        {
            CreateMap<Offer, GetOfferDTO>();
            CreateMap<Field, GetFieldDTO>();
        }
    }
}
