using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static Data.Account CurrentAccount;

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

            CurrentAccount = accounts[index];
            name_l.Content = $"Здравствуйте, {CurrentAccount.Name}";
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
            foreach(Data.Patient p in CurrentAccount.patients)
            {
                if(day == p.day)
                {
                    r++;
                }
            }
            return r;
        }

        private void OpenDay(object sender, RoutedEventArgs e)
        {
            int i = days_grid.Children.IndexOf((Button)sender);
            i = (i + 1) / 3;
            if (i == 7) i = 0;
            DayWindow dayWindow = new DayWindow((DayOfWeek)i, CurrentAccount.patients, FillDayCounters);
            dayWindow.ShowDialog();
        }
    }
}
