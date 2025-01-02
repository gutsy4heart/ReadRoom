using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using Dapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ReadRoom.Model;
using ReadRoom.View;

namespace ReadRoom.ViewModel
{
    public class UpdateBookVM : ViewModelBase
    {
        public string ConnectionString { get; set; }
        public ObservableCollection<Books> Books { get; set; }

        private Books selectedBook;
        public Books SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                if (selectedBook != null)
                {
                    
                    NewBook = new Books
                    {
                        Id = selectedBook.Id,
                        Title = selectedBook.Title,
                        AuthorName = selectedBook.AuthorName,
                        AuthorSurname = selectedBook.AuthorSurname,
                        Publisher = selectedBook.Publisher,
                        Pages = selectedBook.Pages,
                        Genre = selectedBook.Genre,
                        PublicationYear = selectedBook.PublicationYear,
                        CostPrice = selectedBook.CostPrice,
                        SalePrice = selectedBook.SalePrice,
                        IsContinuation = selectedBook.IsContinuation,
                        PrequelBookTitle = selectedBook.PrequelBookTitle
                    };
                }
                RaisePropertyChanged(nameof(SelectedBook));
                RaisePropertyChanged(nameof(NewBook));
            }
        }

        private Books newBook;
        public Books NewBook
        {
            get => newBook;
            set
            {
                newBook = value;
                RaisePropertyChanged(nameof(NewBook));
            }
        }

        public RelayCommand UpdateBookCommand { get; }
        public RelayCommand BackToShopCommand { get; }

        public UpdateBookVM()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;
            Books = new ObservableCollection<Books>();
            UpdateBookCommand = new RelayCommand(UpdateBook);
            BackToShopCommand = new RelayCommand(BackToShop);
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var booksList = connection.Query<Books>("SELECT * FROM Books").ToList();
                Books = new ObservableCollection<Books>(booksList);
            }
        }

        public void UpdateBook()
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Выберите книгу для обновления.");
                return;
            }

            Books.Remove(SelectedBook);


            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "UPDATE Books SET Title = @Title, AuthorName = @AuthorName, AuthorSurname = @AuthorSurname, " +
                             "Publisher = @Publisher, Pages = @Pages, Genre = @Genre, PublicationYear = @PublicationYear, " +
                             "CostPrice = @CostPrice, SalePrice = @SalePrice, IsContinuation = @IsContinuation, " +
                             "PrequelBookTitle = @PrequelBookTitle WHERE Id = @Id";

                connection.Execute(sql, NewBook);
            }


            Books.Add(NewBook);


            RaisePropertyChanged(nameof(Books));
            MessageBox.Show("Книга успешно обновлена.", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void BackToShop()
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
