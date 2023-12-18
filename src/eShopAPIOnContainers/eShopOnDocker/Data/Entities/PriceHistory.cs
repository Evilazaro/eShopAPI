using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Data.Entities
{
    public class PriceHistory
    {
        public Guid Id { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime ChangeDate { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
