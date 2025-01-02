using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GalaSoft.MvvmLight;
using System.Data.SqlClient;
using ReadRoom.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Data;
using System.Windows.Input;
using System.Collections;
using System.Security.Policy;
using ReadRoom.View;

namespace ReadRoom.ViewModel
{
    public class ShopVM : ViewModelBase
    {
        public string ConnectionString { get; set; }
        public ObservableCollection<Books> Books { get; set; }
        public ObservableCollection<CartItem> CartItems { get; set; }
        private ObservableCollection<Books> AllBooks; 

        private Books selectedBook;
        public Books SelectedBook
        {
            get { return selectedBook; }
            set { Set(ref selectedBook, value); }
        }

        private double? totalPrice;
        public double? TotalPrice
        {
            get { return totalPrice; }
            set { Set(ref totalPrice, value); }
        }

        private CartItem selectedCartItem;
        public CartItem SelectedCartItem
        {
            get { return selectedCartItem; }
            set { Set(ref selectedCartItem, value); }
        }

        private Books newBook;
        public Books NewBook
        {
            get => newBook;
            set
            {
                newBook = value;
                RaisePropertyChanged(nameof(newBook));
            }
        }

        private Users currentUser;
        public Users CurrentUser
        {
            get => currentUser;
            set => Set(ref currentUser, value);
        }

        private Promotions  promotion;
        public  Promotions Promotion
        {
            get => promotion;
            set => Set(ref promotion, value);
        }

        public RelayCommand AddBookCommand { get; }
        public RelayCommand AddToCartCommand { get; }
        public RelayCommand DeleteFromCartCommand { get; }
        public RelayCommand CancelPurchaseCartCommand { get; }
        public RelayCommand WriteOffBookCommand { get; }
        public RelayCommand DeleteBookCommand { get; }
        public RelayCommand UpdateBookCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand SearchBookWindowCommand { get; set; }
        public RelayCommand BookStatisticWindowCommand { get; set; }
        public RelayCommand CompletePurchaseCommand { get; set; }
        public RelayCommand PromotionsWindowCommand { get; set; }

        public RelayCommand ReservationWindowCommand { get; set; }


        public ShopVM()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;
            Books = new ObservableCollection<Books>();
            CartItems = new ObservableCollection<CartItem>();
            LoadBooks();
            NewBook = new Books();
            AddToCartCommand = new RelayCommand(AddToCart);
            DeleteFromCartCommand = new RelayCommand(DeleteFromCart);
            CancelPurchaseCartCommand = new RelayCommand(CancelPurchase);
            WriteOffBookCommand = new RelayCommand(WriteOffBook);
            AddBookCommand = new RelayCommand(AddBook);
            DeleteBookCommand = new RelayCommand(DeleteBook);
            UpdateBookCommand = new RelayCommand(UpdateBookWindow);
            LogOutCommand = new RelayCommand(LogOut);
            SearchBookWindowCommand = new RelayCommand(SearchBooks);
            BookStatisticWindowCommand = new RelayCommand(StatisticBookWindow);
            CompletePurchaseCommand = new RelayCommand(CompletePurchase);
            ReservationWindowCommand = new RelayCommand(ReservationWindow);
            PromotionsWindowCommand = new RelayCommand(PromotionWindow);
        }

        private void LoadBooks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var books = connection.Query<Books>("SELECT * FROM Books").AsList();
                Books = new ObservableCollection<Books>(books);
            }
        }

        private void AddToCart()
        {
            if (SelectedBook == null) MessageBox.Show("Выберите товар!","Ошибка!",MessageBoxButton.OK,MessageBoxImage.Error);
            else if(SelectedBook.Status != "Доступно")
            {
                MessageBox.Show("Книга недоступна!", "Внимание!",MessageBoxButton.OK, MessageBoxImage.Error);
                SelectedBook = null;
                return;
            }
            if (SelectedBook != null)
            {
                var existingCartItem = CartItems.FirstOrDefault(ci => ci.Book.Id == SelectedBook.Id);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    CartItems.Add(new CartItem { Book = SelectedBook, Quantity = 1 });
                }
                SelectedBook = null;
                UpdateTotalPrice();
            }
        }

        private void DeleteFromCart()
        {
            if (SelectedCartItem != null)
            {
                if (SelectedCartItem.Quantity > 1)
                {
                    SelectedCartItem.Quantity--;
                }
                else
                {
                    CartItems.Remove(SelectedCartItem);
                }
                SelectedBook = null;
                UpdateTotalPrice();
            }
            else
            {
                MessageBox.Show("Выберите товар!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = CartItems.Sum(ci => ci.Book.SalePrice * ci.Quantity);
            RaisePropertyChanged(nameof(TotalPrice));
        }

        private void CancelPurchase()
        {
            var result = MessageBox.Show("Вы уверены, что хотите отменить покупку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                CartItems.Clear();
                UpdateTotalPrice();
            }
            else
            {
                return;
            }
            
        }

        private void WriteOffBook()
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Выберите книгу для списания.");
                return;
            }

            if (Books == null)
            {
                MessageBox.Show("Список книг не инициализирован.");
                return;
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                MessageBox.Show("Строка подключения не установлена.");
                return;
            }

            try
            {
                
                SelectedBook.Status = "Недоступно";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Books SET Status = @Status WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Status", SelectedBook.Status);
                        command.Parameters.AddWithValue("@Id", SelectedBook.Id);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Книга списано!");
                    
                }

                
                RaisePropertyChanged(nameof(Books));
                RaisePropertyChanged(nameof(SelectedBook));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddBook()
        {

            if (string.IsNullOrEmpty(NewBook.Title) ||
                string.IsNullOrEmpty(NewBook.AuthorName) ||
                string.IsNullOrEmpty(NewBook.AuthorSurname) ||
                string.IsNullOrEmpty(NewBook.Publisher) ||
                string.IsNullOrEmpty(NewBook.Genre) ||
                string.IsNullOrEmpty(NewBook.IsContinuation) ||
                string.IsNullOrEmpty(NewBook.PrequelBookTitle) ||
                NewBook.PublicationDate == DateTime.MinValue ||  
                NewBook.Pages <= 0 ||
                NewBook.PublicationYear <= 0 ||
                NewBook.CostPrice <= 0 ||
                NewBook.SalePrice <= 0)
            {
                MessageBox.Show("Заполните все обязательные поля и укажите корректные значения для себестоимости и цены продажи.");
                return;
            }
            if (NewBook.IsContinuation != "Да" && NewBook.IsContinuation != "Нет")
            {
                MessageBox.Show("Некорректное значение для поля 'IsContinuation'. Выберите 'Да' или 'Нет'.");
                return;
            }

            NewBook.Status = "Доступно";

                Books.Add(NewBook);

                string sql = "INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Pages, Genre, PublicationYear, CostPrice, SalePrice, IsContinuation, PrequelBookTitle, [Status], PublicationDate) " +
                             "VALUES (@Title, @AuthorName, @AuthorSurname, @Publisher, @Pages, @Genre, @PublicationYear, @CostPrice, @SalePrice, @IsContinuation, @PrequelBookTitle, @Status, @PublicationDate)";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", NewBook.Title);
                        command.Parameters.AddWithValue("@AuthorName", NewBook.AuthorName);
                        command.Parameters.AddWithValue("@AuthorSurname", NewBook.AuthorSurname);
                        command.Parameters.AddWithValue("@Publisher", NewBook.Publisher);
                        command.Parameters.AddWithValue("@Pages", NewBook.Pages);
                        command.Parameters.AddWithValue("@Genre", NewBook.Genre);
                        command.Parameters.AddWithValue("@PublicationYear", NewBook.PublicationYear);
                        command.Parameters.AddWithValue("@CostPrice", NewBook.CostPrice);
                        command.Parameters.AddWithValue("@SalePrice", NewBook.SalePrice);
                        command.Parameters.AddWithValue("@IsContinuation", NewBook.IsContinuation);
                        command.Parameters.AddWithValue("@PrequelBookTitle", NewBook.PrequelBookTitle);
                        command.Parameters.AddWithValue("@Status", NewBook.Status);
                        command.Parameters.AddWithValue("@PublicationDate", NewBook.PublicationDate);

                        command.ExecuteNonQuery();
                    }
                }


                NewBook = new Books(); 

                MessageBox.Show("Книга добавлена!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            
            
        }

        private void DeleteBook()
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            string sql = "DELETE FROM Books WHERE Id = @Id";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", SelectedBook.Id);
                    command.ExecuteNonQuery();
                }
                Books.Remove(SelectedBook);
            }

            SelectedBook = null;


            RaisePropertyChanged("Books");
            RaisePropertyChanged("SelectedBook");

            MessageBox.Show("Книга успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateBookWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                UpdateBookWindow? window = Application.Current.Windows.OfType<UpdateBookWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new UpdateBookWindow();
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

        private void LogOut()
        {
            if (Application.Current.MainWindow != null)
            {
                AuthorizationWindow? window = Application.Current.Windows.OfType<AuthorizationWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new AuthorizationWindow();
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

        private void SearchBooks()
        {
            if (Application.Current.MainWindow != null)
            {
                SearchBookWindow? window = Application.Current.Windows.OfType<SearchBookWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new SearchBookWindow();
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

        private void StatisticBookWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                BookStatisticWindow? window = Application.Current.Windows.OfType<BookStatisticWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new BookStatisticWindow();
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

        public void CompletePurchase()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show("Корзина пуста. Добавьте книги в корзину, прежде чем завершить покупку.");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите завершить покупку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    foreach (var item in CartItems)
                    {
                        if (CurrentUser == null)
                        {
                            MessageBox.Show("Ошибка: информация о текущем пользователе не доступна.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        string sql = "INSERT INTO Sales (BookId, Quantity, SaleDate, TotalPrice, UserLogin) VALUES (@BookId, @Quantity, @SaleDate, @TotalPrice, @UserLogin)";
                        using (var command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@BookId", item.Book.Id);
                            command.Parameters.AddWithValue("@Quantity", item.Quantity);
                            command.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                            command.Parameters.AddWithValue("@TotalPrice", item.Book.SalePrice * item.Quantity);
                            command.Parameters.AddWithValue("@UserLogin", CurrentUser.Login);

                            command.ExecuteNonQuery();
                        }
                    }

                    CartItems.Clear();
                    UpdateTotalPrice();

                    MessageBox.Show("Покупка завершена успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при завершении покупки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PromotionWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                PromotionsWindow? window = Application.Current.Windows.OfType<PromotionsWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new PromotionsWindow();
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

        private void ReservationWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                ReservationWindow? window = Application.Current.Windows.OfType<ReservationWindow>().FirstOrDefault();
                if (window == null)
                {
                    window = new ReservationWindow();
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
