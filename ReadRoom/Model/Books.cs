using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ReadRoom.Model
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Publisher { get; set; }
        public int? Pages { get; set; }
        public string Genre { get; set; }
        public int? PublicationYear { get; set; }
        public double? CostPrice { get; set; }
        public double? SalePrice { get; set; }
        public string IsContinuation { get; set; }
        public string PrequelBookTitle { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Status { get; set; }
    }

    public class AuthorStatistic
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public int TotalSold { get; set; }
    }

    public class GenreStatistic
    {
        public string Genre { get; set; }
        public int TotalSold { get; set; }
    }
}
