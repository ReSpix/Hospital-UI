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
        private Data.Patient? patient = null;

        public PatientWindow(DayOfWeek day, Action onPatientAdd)
        {
            Init(day, onPatientAdd);
        }

        public PatientWindow(DayOfWeek day, Action onPatientAdd, Data.Patient patient)
        {
            Init(day, onPatientAdd);

            this.patient = patient;
            name_f.Text = patient.fullName;

            string start_s = patient.start.ToString();
            start_f.Text = start_s.Length < 5 ? "0" + start_s : start_s;

            string end_s = patient.end.ToString();
            end_f.Text = end_s.Length < 5 ? "0" + end_s : end_s;
        }

        private void Init(DayOfWeek day, Action onPatientAdd)
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
            else if(patient == null)
            {
                AddPatient();
            }
            else
            {
                EditPatient();
            }
        }

        private void EditPatient()
        {
            int index = MainWindow.CurrentAccount.patients.IndexOf((Data.Patient)patient);
            Data.Patient newPatient = new Data.Patient() { day = day, fullName = fullName, end = endTime, start = startTime };
            MainWindow.CurrentAccount.patients[index] = newPatient;

            patientAdded();
            Close();
        }

        private void AddPatient()
        {
            Data.Patient p = new Data.Patient() { day = day, fullName = fullName, end = endTime, start = startTime };
            MainWindow.CurrentAccount.patients.Add(p);
            patientAdded();
            Close();
        }

        private bool TimeCollision(TimeOnly start, TimeOnly end)
        {
            List<Data.Patient> patients = DayWindow.TodayPatients(day);

            foreach(Data.Patient p in patients)
            {
                if(p.Equals((Data.Patient)patient))
                {
                    continue;
                }
                if(start >= p.start && start < p.end || end > p.start && end <= p.end)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
