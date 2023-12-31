﻿using JobSearch.DTOs.FieldDTOs;
using JobSearch.Models;

namespace JobSearch.DTOs.OfferDTOs
{
    public class GetOfferDTO
    {
        public int Id { get; set; }
        public string Company { get; set; } = "";
        public string JobDescription { get; set; } = "";
        public DateTime publishedOn { get; set; }
        public GetFieldDTO Field { get; set; }
    }
}
