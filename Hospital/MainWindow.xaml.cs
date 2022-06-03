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
                new Data.Patient() { fullName = "Дмитриев Дмитрий Дмитриевич", day = DayOfWeek.Monday, start = TimeOnly.Parse("08:00"), end = TimeOnly.Parse("10:00")},
            }
        };
        public static List<Data.Account> accounts = new List<Data.Account>(){ testAccount };

        private LoginWindow loginWindow;
        private Data.Account currentAccount;

        public MainWindow()
        {
            InitializeComponent();
            OnLogIn(0);
        }

        private void OpenLogin(object sender, RoutedEventArgs e)
        {
            loginWindow = new LoginWindow(OnLogIn);
            loginWindow.ShowDialog();
        }

        private void OnLogIn(int index)
        {
            start_grid.Visibility = Visibility.Hidden;
            account_grid.Visibility = Visibility.Visible;
            loginWindow?.Close();

            currentAccount = accounts[index];
            name_l.Content = $"Здравствуйте, {currentAccount.Name}";
            FillDayCounters();
        }

        private void FillDayCounters()
        {
            string end = " пациентов";
            mond_c.Content = PatientsOfDay(DayOfWeek.Monday) + end;
            tue_c.Content = PatientsOfDay(DayOfWeek.Tuesday) + end;
            wed_c.Content = PatientsOfDay(DayOfWeek.Wednesday) + end;
            thur_c.Content = PatientsOfDay(DayOfWeek.Thursday) + end;
            fri_c.Content = PatientsOfDay(DayOfWeek.Friday) + end;
            sat_c.Content = PatientsOfDay(DayOfWeek.Saturday) + end;
            sun_c.Content = PatientsOfDay(DayOfWeek.Sunday) + end;
        }

        private int PatientsOfDay(DayOfWeek day)
        {
            int r = 0;
            foreach(Data.Patient p in currentAccount.patients)
            {
                if(day == p.day)
                {
                    r++;
                }
            }
            return r;
        }

        private void OpenMonday(object sender, RoutedEventArgs e)
        {
            DayWindow dayWindow = new DayWindow(DayOfWeek.Monday, currentAccount.patients);
            dayWindow.ShowDialog();
        }
    }
}
