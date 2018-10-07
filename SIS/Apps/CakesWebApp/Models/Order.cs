namespace CakesWebApp.Models
{
    using System;
    using System.Collections.Generic;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Product> Products { get; set; }
    }
}
