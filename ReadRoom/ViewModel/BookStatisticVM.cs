using ReadRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Security.Policy;
using ReadRoom.View;
using System.Windows;
namespace ReadRoom.ViewModel
{
    public class BookStatisticVM
    {
        public string ConnectionString { get; set; }
        public ObservableCollection<Books> BookStatistics { get; set; }

        public RelayCommand ShowNewReleasesCommand { get; }
        public RelayCommand ShowBestSellingBooksCommand { get; }
        public RelayCommand ShowPopularAuthorsCommand { get; }
        public RelayCommand ShowPopularGenresCommand { get; }
        public RelayCommand BackToShopCommand { get; }
        public BookStatisticVM()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;
            BookStatistics = new ObservableCollection<Books>();

            ShowNewReleasesCommand = new RelayCommand(() => LoadBooks("day"));
            ShowBestSellingBooksCommand = new RelayCommand(() => LoadBestSellingBooks("week"));
            ShowPopularAuthorsCommand = new RelayCommand(() => LoadPopularAuthors("month"));
            ShowPopularGenresCommand = new RelayCommand(() => LoadPopularGenres("year"));
            BackToShopCommand = new RelayCommand(BackToShop);
        }
        private void LoadBooks(string period)
        {
            BookStatistics.Clear();

            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = $@"
                SELECT * FROM Books 
                WHERE PublicationDate >= DATEADD({period}, -1, GETDATE()) 
                ORDER BY PublicationDate DESC";

                var books = connection.Query<Books>(query).ToList();

                foreach (var book in books)
                {
                    BookStatistics.Add(book);
                }
            }
        }

        private void LoadBestSellingBooks(string period)
        {
            BookStatistics.Clear();

            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                 SELECT
                 Books.Title,
                 Books.AuthorName,
                 Books.AuthorSurname,
                 Books.Genre,
                 Books.PublicationYear,
                 SUM(Sales.Quantity) AS TotalQuantity
                 FROM Books
                 JOIN Sales ON Books.Id = Sales.BookId
                 GROUP BY Books.Title, Books.PublicationYear, Books.AuthorName, Books.AuthorSurname, Books.Genre;";

                var bestSellingBooks = connection.Query<Books>(query).ToList();


                foreach (var book in bestSellingBooks)
                {
                    BookStatistics.Add(book);
                }
            }
        }

        private void LoadPopularAuthors(string period)
        {
            BookStatistics.Clear();

            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = $@"
                SELECT TOP 10 b.AuthorName, b.AuthorSurname, COUNT(*) AS BooksSold
                FROM Sales s
                JOIN Books b ON s.BookId = b.Id
                WHERE s.SaleDate >= DATEADD({period}, -1, GETDATE())
                GROUP BY b.AuthorName, b.AuthorSurname
                ORDER BY BooksSold DESC";

                var authors = connection.Query<AuthorStatistic>(query).ToList();

                foreach (var author in authors)
                {
                    BookStatistics.Add(new Books
                    {
                        AuthorName = author.AuthorName,
                        AuthorSurname = author.AuthorSurname,
                       
                    });
                }
            }
        }

        private void LoadPopularGenres(string period)
        {
            BookStatistics.Clear();

            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = $@"
                SELECT TOP 10 b.Genre, COUNT(*) AS GenreCount
                FROM Sales s
                JOIN Books b ON s.BookId = b.Id
                WHERE s.SaleDate >= DATEADD({period}, -1, GETDATE())
                GROUP BY b.Genre
                ORDER BY GenreCount DESC";

                var genres = connection.Query<GenreStatistic>(query).ToList();

                foreach (var genre in genres)
                {
                    BookStatistics.Add(new Books
                    {
                        Genre = genre.Genre
                    });
                }
            }
        }

        private void BackToShop()
        {
            if (Application.Current.MainWindow != null)
            {
                ShopWindow? window = Application.Current.Windows.OfType<ShopWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new ShopWindow();
                    window.Show();
                }
                else
                {
                    window.Activate();
                }

                Application.Current.MainWindow?.Close();

                Application.Current.MainWindow = window;
            }
        }

    }
}
