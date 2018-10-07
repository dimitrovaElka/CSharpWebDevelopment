namespace CakesWebApp.Models
{
    using System;
    using System.Collections.Generic;

    public class Product : BaseModel<int>
    {
        // The product has name, price and image URL
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }
    }
}
