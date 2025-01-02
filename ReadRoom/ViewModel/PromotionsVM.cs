using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using Dapper;
using ReadRoom.Model;
using ReadRoom.View;

namespace ReadRoom.ViewModel
{
    public class PromotionsVM : ViewModelBase
    {
        public string ConnectionString { get; set; }

        private ObservableCollection<Books> books;
        public ObservableCollection<Books> Books
        {
            get => books;
            set => Set(ref books, value);
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

        private Promotions currentPromotion;
        public Promotions CurrentPromotion
        {
            get => currentPromotion;
            set => Set(ref currentPromotion, value);
        }

        public RelayCommand ApplyDiscountCommand { get; }
        public RelayCommand BackToShopCommand { get; }

        public PromotionsVM()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;
            Books = new ObservableCollection<Books>(LoadBooks());
            SelectedBooks = new ObservableCollection<Books>();

            CurrentPromotion = new Promotions();

            ApplyDiscountCommand = new RelayCommand(ApplyDiscount);

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

        private bool PromotionNameExists(string promotionName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT COUNT(1) FROM Promotions WHERE PromotionName = @PromotionName";
                return connection.ExecuteScalar<int>(sql, new { PromotionName = promotionName }) > 0;
            }
        }

        private void ApplyDiscount()
        {
            if (SelectedBooks.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одну книгу для применения скидки.");
                return;
            }

            if (PromotionNameExists(CurrentPromotion.PromotionName))
            {
                MessageBox.Show("Акция с таким названием уже существует. Пожалуйста, выберите другое название.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var book in SelectedBooks)
            {
                book.SalePrice *= (1 - CurrentPromotion.DiscountPercent / 100.0);
                UpdateBookInDatabase(book);
                AddPromotionToDatabase(book);
            }

            MessageBox.Show("Скидка успешно применена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            CurrentPromotion = new Promotions();
            SelectedBooks.Clear();
        }

        private void UpdateBookInDatabase(Books book)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "UPDATE Books SET SalePrice = @SalePrice WHERE Id = @Id";
                connection.Execute(sql, new { book.SalePrice, book.Id });
            }
        }

        private void AddPromotionToDatabase(Books book)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                
                string checkBookSql = "SELECT COUNT(1) FROM Books WHERE Id = @BookId";
                var bookExists = connection.ExecuteScalar<int>(checkBookSql, new { BookId = book.Id }) > 0;

                if (bookExists)
                {
                    string sql = @"
            INSERT INTO Promotions (PromotionName, BookId, DiscountPercent, StartDate, EndDate) 
            VALUES (@PromotionName, @BookId, @DiscountPercent, @StartDate, @EndDate)";

                    connection.Execute(sql, new
                    {
                        PromotionName = CurrentPromotion.PromotionName,
                        BookId = book.Id,
                        DiscountPercent = CurrentPromotion.DiscountPercent,
                        StartDate = CurrentPromotion.StartDate,
                        EndDate = CurrentPromotion.EndDate
                    });
                }
                else
                {
                    throw new Exception("Book with the specified Id does not exist in the Books table.");
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
