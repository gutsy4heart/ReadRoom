using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadRoom.Model
{
    public class Sales
    {
        public int SaleId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        [ForeignKey("User")]
        public string UserLogin { get; set; }

        public virtual Books Book { get; set; }
        public virtual Users User { get; set; }
    }
}
