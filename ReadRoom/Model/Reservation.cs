using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReadRoom.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        [ForeignKey("BookID")]
        public int BookId { get; set; }
        [ForeignKey("Users")]
        public string UserLogin {  get; set; }
        public DateTime ReservationDate { get; set; }

        public virtual Books BookID { get; set; }
        public virtual Users Users { get; set; }
    }
}
