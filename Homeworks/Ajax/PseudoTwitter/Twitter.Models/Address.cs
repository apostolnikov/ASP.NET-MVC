namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public int? Number { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public virtual User User {get; set;}
    }
}