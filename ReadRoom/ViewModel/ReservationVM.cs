using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using Dapper;
using ReadRoom.Model;
using ReadRoom.View;

namespace ReadRoom.ViewModel
{
    public class ReservationVM : ViewModelBase
    {
        public string ConnectionString { get; set; }

        private ObservableCollection<Books> books;
        public ObservableCollection<Books> Books
        {
            get => books;
            set => Set(ref books, value);
        }

        private ObservableCollection<Users> users;
        public ObservableCollection<Users> Users
        {
            get => users;
            set => Set(ref users, value);
        }

        private ObservableCollection<Books> selectedBooks;
        public ObservableCollection<Books> SelectedBooks
        {
            get => selectedBooks;
            set => Set(ref selectedBooks, value);
        }

        private Books selectedBook;
        public Books SelectedBook
        {
            get => selectedBook;
            set
            {
                Set(ref selectedBook, value);
                if (value != null && !SelectedBooks.Contains(value))
                {
                    SelectedBooks.Add(value);
                }
            }
        }

        private Users selectedUser;
        public Users SelectedUser
        {
            get => selectedUser;
            set => Set(ref selectedUser, value);
        }

        public RelayCommand ReserveBooksCommand { get; }
        public RelayCommand BackToShopCommand { get; }

        public ReservationVM()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;
            Books = new ObservableCollection<Books>(LoadBooks());
            Users = new ObservableCollection<Users>(LoadUsers());
            SelectedBooks = new ObservableCollection<Books>();

            ReserveBooksCommand = new RelayCommand(ReserveBooks, CanReserveBooks);
            BackToShopCommand = new RelayCommand(BackToShop);
        }

        private IEnumerable<Books> LoadBooks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM Books";
                return connection.Query<Books>(sql);
            }
        }

        private IEnumerable<Users> LoadUsers()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM Users";
                return connection.Query<Users>(sql);
            }
        }

        private bool CanReserveBooks()
        {
            return SelectedUser != null && SelectedBooks.Count > 0;
        }

        private void ReserveBooks()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedBooks.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы одну книгу для откладывания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (var book in SelectedBooks)
                {
                    string sql = "INSERT INTO Reservations (BookId, ReservationDate, UserLogin) VALUES (@BookId, @ReservationDate, @UserLogin)";
                    connection.Execute(sql, new { BookId = book.Id, ReservationDate = DateTime.Now, UserLogin = SelectedUser.Login });
                }
            }

            MessageBox.Show("Книги успешно отложены для пользователя.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedBooks.Clear();
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
