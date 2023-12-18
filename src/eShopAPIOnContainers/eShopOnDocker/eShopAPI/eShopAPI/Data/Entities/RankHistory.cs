using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopAPI.Data.Entities
{
    public class RankHistory
    {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public DateTime Date { get; set; }
        public  Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
