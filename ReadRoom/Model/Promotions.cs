using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRoom.Model
{
    public class Promotions
    {
        public int PromotionId { get; set; }
        public string PromotionName {  get; set; }
        [ForeignKey("BookID")]
        public int BookId {  get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Books BookID {  get; set; }
    }
}
