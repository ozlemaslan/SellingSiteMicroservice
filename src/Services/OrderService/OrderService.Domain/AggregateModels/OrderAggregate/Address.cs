namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    /// <summary>
    /// Record tipleri valueobjectteki equal işlemlerini yapar.
    /// Ancak aynı property adları olmak zorundadır.
    /// </summary>
    public record Address 
    {
        public Address(string street, string city, string country, string state, string zipCode)
        {
            Street = street;
            City = city;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }
        public Address()
        {
            
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
