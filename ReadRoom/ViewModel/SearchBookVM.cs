using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using ReadRoom.Model;
using ReadRoom.View;
using Dapper;
using System.Data.SqlClient;

namespace ReadRoom.ViewModel
{
    public class SearchBookVM : ViewModelBase
    {
        public string ConnectionString { get; set; }
        private ObservableCollection<Books> books;

        public ObservableCollection<Books> Books
        {
            get => books;
            set => Set(ref books, value);
        }

        private string searchTitle;
        public string SearchTitle
        {
            get => searchTitle;
            set => Set(ref searchTitle, value);
        }

        private string searchAuthorName;
        public string SearchAuthorName
        {
            get => searchAuthorName;
            set => Set(ref searchAuthorName, value);
        }

        private string searchAuthorSurname;
        public string SearchAuthorSurname
        {
            get => searchAuthorSurname;
            set => Set(ref searchAuthorSurname, value);
        }

        private string searchGenre;
        public string SearchGenre
        {
            get => searchGenre;
            set => Set(ref searchGenre, value);
        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ChangeWindowCommand { get; set; }

        public SearchBookVM()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;

            Books = new ObservableCollection<Books>();

            SearchCommand = new RelayCommand(SearchBooks);
            ChangeWindowCommand = new RelayCommand(ChangeWindow);
        }

        private void SearchBooks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"
                    SELECT * 
                    FROM Books
                    WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%')
                    AND (@AuthorName IS NULL OR AuthorName LIKE '%' + @AuthorName + '%')
                    AND (@AuthorSurname IS NULL OR AuthorSurname LIKE '%' + @AuthorSurname + '%')
                    AND (@Genre IS NULL OR Genre LIKE '%' + @Genre + '%')";

                var parameters = new
                {
                    Title = string.IsNullOrEmpty(SearchTitle) ? (string)null : SearchTitle,
                    AuthorName = string.IsNullOrEmpty(SearchAuthorName) ? (string)null : SearchAuthorName,
                    AuthorSurname = string.IsNullOrEmpty(SearchAuthorSurname) ? (string)null : SearchAuthorSurname,
                    Genre = string.IsNullOrEmpty(SearchGenre) ? (string)null : SearchGenre
                };

                var books = connection.Query<Books>(query, parameters).ToList();
                Books.Clear();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
        }

        private void ChangeWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                var window = Application.Current.Windows.OfType<ShopWindow>().FirstOrDefault();
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
