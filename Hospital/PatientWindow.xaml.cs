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

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        private DayOfWeek day;

        private string fullName;
        private TimeOnly startTime;
        private TimeOnly endTime;

        private Action patientAdded;

        public PatientWindow(DayOfWeek day, Action onPatientAdd)
        {
            InitializeComponent();
            Title = Title + $" ({day.ToString()})";
            this.day = day;
            patientAdded = onPatientAdd;
        }

        private void Name_changed(object sender, TextChangedEventArgs e)
        {
            fullName = name_f.Text;
        }

        private void Start_changed(object sender, TextChangedEventArgs e)
        {
            bool r = TimeOnly.TryParseExact(start_f.Text, "HH:mm",out TimeOnly time);
            if (r)
            {
                startTime = time;
                start_f.Foreground = Brushes.Green;
            }
            else
            {
                start_f.Foreground = Brushes.Red;
            }
        }

        private void End_changed(object sender, TextChangedEventArgs e)
        {
            bool r = TimeOnly.TryParseExact(end_f.Text, "HH:mm", out TimeOnly time);
            if (r)
            {
                endTime = time;
                end_f.Foreground = Brushes.Green;
            }
            else
            {
                end_f.Foreground = Brushes.Red;
            }
        }

        private void Save_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Заполните поле \"ФИО\"", "Пустое ФИО");
            }
            else if(start_f.Foreground == Brushes.Red || end_f.Foreground == Brushes.Red)
            {
                MessageBox.Show("Введите время в формате ЧЧ:ММ (12:00)", "Неверное время");
            }
            else if(startTime < TimeOnly.Parse("08:00") || endTime > TimeOnly.Parse("17:00"))
            {
                MessageBox.Show("Время должно быть между 08:00 и 17:00", "Неверное время");
            }
            else if(startTime >= endTime)
            {
                MessageBox.Show("Время начала приема должно быть меньше времени окончания приема", "Неверное время");
            }
            else if(TimeCollision(startTime, endTime))
            {
                MessageBox.Show("Время не должно пересекаться с другими пациентами", "Пересечение времени");
            }
            else
            {
                AddPatient();
                patientAdded();
                Close();
            }
        }

        private void AddPatient()
        {
            Data.Patient p = new Data.Patient() { day = day, fullName = fullName, end = endTime, start = startTime };
            MainWindow.CurrentAccount.patients.Add(p);
        }

        private bool TimeCollision(TimeOnly start, TimeOnly end)
        {
            List<Data.Patient> patients = DayWindow.TodayPatients(day);

            foreach(Data.Patient p in patients)
            {
                if(start >= p.start && start < p.end || end > p.start && end <= p.end)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
