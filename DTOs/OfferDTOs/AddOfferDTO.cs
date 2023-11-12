using JobSearch.Models;

namespace JobSearch.DTOs.OfferDTOs
{
    public class AddOfferDTO
    {
        public string Company { get; set; } = "";
        public string JobDescription { get; set; } = "";
        public int UserId { get; set; }
        public int FieldId { get; set; }
    }
}
