namespace JobSearch.Models
{
    public class Field
    {
        int Id { get; set; }
        public string Name { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
