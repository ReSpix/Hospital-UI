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

        public PatientWindow(DayOfWeek day)
        {
            InitializeComponent();
            Title = Title + $" ({day.ToString()})";
            this.day = day;
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
        }
    }
}
