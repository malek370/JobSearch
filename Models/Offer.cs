using JobSearch.DTOs.FieldDTOs;

namespace JobSearch.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Company { get; set; } = "";
        public string JobDescription { get; set; } = "";
        public DateTime publishedOn { get; set; }= DateTime.UtcNow;
        public User recruiter { get; set; }
        public Field Field { get; set; }
    }
}
