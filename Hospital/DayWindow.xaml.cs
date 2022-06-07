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
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для DayWindow.xaml
    /// </summary>
    public partial class DayWindow : Window
    {
        private DayOfWeek day;
        private static List<Data.Patient> patients;

        private Action patientsUpdated;

        public DayWindow(DayOfWeek day, List<Data.Patient> patients, Action onAdded)
        {
            InitializeComponent();
            this.day = day;
            DayWindow.patients = patients;
            patientsUpdated = onAdded;
            UpdateList();
        }

        public void UpdateList()
        {
            _Users.ItemsSource = TodayPatients(day);
            patientsUpdated?.Invoke();
        }

        public static List<Data.Patient> TodayPatients(DayOfWeek day)
        {
            List<Data.Patient> today = new List<Data.Patient>();

            foreach(Data.Patient p in patients)
            {
                if(p.day == day)
                {
                    today.Add(p);
                }
            }
            return today;
        }

        private void Edit_click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _Users.Items.IndexOf(item);

            Data.Patient p = (Data.Patient)item;
            PatientWindow ps = new PatientWindow(day, UpdateList, p);
            ps.ShowDialog();
        }

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _Users.Items.IndexOf(item);

            Data.Patient p = (Data.Patient)item;
            MainWindow.CurrentAccount.patients.Remove(p);
            patientsUpdated?.Invoke();
            UpdateList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow pw = new PatientWindow(day, UpdateList);
            pw.ShowDialog();
        }
    }
}
