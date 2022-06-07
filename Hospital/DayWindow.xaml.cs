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

        private Action patientAdded;

        public DayWindow(DayOfWeek day, List<Data.Patient> patients, Action onAdded)
        {
            InitializeComponent();
            this.day = day;
            DayWindow.patients = patients;
            patientAdded = onAdded;
            UpdateList();
        }

        public void UpdateList()
        {
            _Users.ItemsSource = TodayPatients(day);
            patientAdded?.Invoke();
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
            Button button = sender as Button;
        }

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow pw = new PatientWindow(day, UpdateList);
            pw.ShowDialog();
        }
    }
}
