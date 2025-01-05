using Microsoft.AspNetCore.Identity;

namespace Test.Models
{
    public class Zamowienia
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProduktId { get; set; }
        public int Quantity { get; set; }
        public DateTime DataZamowienia { get; set; }
        public virtual Asortyment Produkt { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
