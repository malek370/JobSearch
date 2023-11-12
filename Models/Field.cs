namespace JobSearch.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Offer>? Offers { get; set; }
    }
}
