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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data.Account testAccount = new Data.Account() {
            Name = "Иванов Иван Иванович",
            mail = "ivanov@gmail.com",
            password = "123456",
            patients = new List<Data.Patient> {
                new Data.Patient() { Name = "Дмитриев Дмитрий Дмитриевич", day = DayOfWeek.Monday, start = TimeOnly.Parse("08:00"), end = TimeOnly.Parse("10:00")},
            }
        };
        public static List<Data.Account> accounts = new List<Data.Account>(){ testAccount };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenLogin(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(OnLogIn);
            loginWindow.ShowDialog();
        }

        private void OnLogIn(int index)
        {
            MessageBox.Show($"Вход в аккаунт: {accounts[index].Name}");
        }
    }
}
