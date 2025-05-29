using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ReadRoom.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
namespace ReadRoom.ViewModel
{
    public class RegistrationVM : ViewModelBase
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["LocalAutoCenterDb"].ConnectionString;

        public RelayCommand RegistrationCommand { get; }

        private Users user;
        public Users User
        {
            get => user;
            set
            {
                Set(ref user, value);
            }
        }

        public RegistrationVM()
        {
            
            RegistrationCommand = new RelayCommand(RegisterUser);
            
            User = new Users();
        }

        private void OpenAuthorizationWindow()
        {
            if (Application.Current.MainWindow != null)
            {
                AuthorizationWindow? authorization = Application.Current.Windows.OfType<AuthorizationWindow>().FirstOrDefault();
                if (authorization == null)
                {
                    authorization = new AuthorizationWindow();
                    authorization.Show();
                }
                else
                {
                    authorization.Activate();
                }

                Application.Current.MainWindow?.Close();

                Application.Current.MainWindow = authorization;
            }
        }

        private void RegisterUser()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(1) FROM Users WHERE Login = @Login";
                    var checkParameters = new { Login = User.Login };
                    int userCount = connection.ExecuteScalar<int>(checkQuery, checkParameters);

                    if (userCount > 0)
                    {
                        MessageBox.Show("The login is already in use. Please choose a different login.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    string query = @"
                    INSERT INTO Users (Login, Password, Email) 
                    VALUES (@Login, @Password, @Email)";

                    var parameters = new
                    {
                        Login = User.Login,
                        Password = User.Password,
                        Email = User.Email
                    };
                    int rowsAffected = connection.Execute(query, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        OpenAuthorizationWindow();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
