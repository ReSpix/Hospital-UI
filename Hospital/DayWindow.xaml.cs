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
        private List<Data.Patient> patients;

        public DayWindow(DayOfWeek day, List<Data.Patient> patients)
        {
            InitializeComponent();
            this.day = day;
            this.patients = patients;
            _Users.ItemsSource = TodayPatients(patients);
        }

        private List<Data.Patient> TodayPatients(List<Data.Patient> allPatients)
        {
            List<Data.Patient> today = new List<Data.Patient>();

            foreach(Data.Patient p in allPatients)
            {
                if(p.day == day)
                {
                    today.Add(p);
                }
            }
            return today;
        }
    }
}
