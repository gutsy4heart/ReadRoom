using ReadRoom.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Data.SqlClient;
using Dapper;
using ReadRoom.View;
namespace ReadRoom.ViewModel
{
    public class AuthorizationVM : ViewModelBase
    {
        public string ConnectionString { get; set; }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegistrationOpenCommand { get; }
        private Users user;
        public Users User
        {
            get => user;
            set
            {
                Set(ref user, value);
            }
        }

        public AuthorizationVM()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["localAutoCenterDb"].ConnectionString;
            LoginCommand = new RelayCommand(AuthorizationClick);
            RegistrationOpenCommand = new RelayCommand(OpenRegistrationWindow);
            User = new Users();
        }

        private void OpenRegistrationWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                ResgistrationWindow? resgistration = Application.Current.Windows.OfType<ResgistrationWindow>().FirstOrDefault();
                if (resgistration == null)
                {
                    resgistration = new ResgistrationWindow();
                    resgistration.Show();
                }
                else
                {
                    resgistration.Activate();
                }

                Application.Current.MainWindow?.Close();

                Application.Current.MainWindow = resgistration;
            }
        }

        private void OpenShopWindow(ShopVM shopVM)
        {
            if (Application.Current.MainWindow != null)
            {
                ShopWindow? shop = Application.Current.Windows.OfType<ShopWindow>().FirstOrDefault();
                if (shop == null)
                {
                    shop = new ShopWindow
                    {
                        DataContext = shopVM
                    };
                    shop.Show();
                }
                else
                {
                    shop.DataContext = shopVM;
                    shop.Activate();
                }

                Application.Current.MainWindow?.Close();

                Application.Current.MainWindow = shop;
            }
        }

        private void AuthorizationClick()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(1) FROM Users WHERE Login = @Login AND Password = @Password";
                    var parameters = new { Login = User.Login, Password = User.Password };
                    int userCount = connection.Query<int>(query, parameters).Single();

                    if (userCount == 1)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        string userQuery = "SELECT * FROM Users WHERE Login = @Login";
                        var currentUser = connection.Query<Users>(userQuery, new { Login = User.Login }).SingleOrDefault();

                        var shopVM = new ShopVM
                        {
                            CurrentUser = currentUser
                        };

                        OpenShopWindow(shopVM);
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Authorization Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}
