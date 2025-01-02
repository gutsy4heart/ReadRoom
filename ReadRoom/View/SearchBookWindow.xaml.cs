using ReadRoom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReadRoom.View
{
    /// <summary>
    /// Логика взаимодействия для SearchBookWindow.xaml
    /// </summary>
    public partial class SearchBookWindow : Window
    {
        public SearchBookWindow()
        {
            InitializeComponent();
            DataContext = new SearchBookVM();
        }
    }
}
