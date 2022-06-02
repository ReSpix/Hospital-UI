using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string mail_log;
        private string password_log;

        private string name_reg;
        private string mail_reg;
        private string password_reg;
        private string passwordCheck_reg;

        private Action<int> onLogIn;

        public LoginWindow(Action<int> onLogIn)
        {
            InitializeComponent();
            this.onLogIn = onLogIn;
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            int idx = AccountIndex(mail_log);
            if (string.IsNullOrEmpty(mail_log))
            {
                ErrorMessage("Введите почту");
            }
            else if (string.IsNullOrEmpty(password_log))
            {
                ErrorMessage("Введите пароль");
            }
            else if(idx == -1)
            {
                ErrorMessage("Нет такого аккаунта");
            }
            else if(MainWindow.accounts[idx].password != password_log)
            {
                ErrorMessage("Неправильный пароль");
            }
            else
            {
                onLogIn.Invoke(idx);
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(name_reg) 
                || string.IsNullOrEmpty(mail_reg) 
                || string.IsNullOrEmpty(password_reg) 
                || string.IsNullOrEmpty(passwordCheck_reg))
            {
                ErrorMessage("Заполните все поля");
            }
            else if(!ValidMail(mail_reg))
            {
                ErrorMessage("Неверная почта");
            }
            else if(AccountIndex(mail_reg) != -1)
            {
                ErrorMessage("Такой аккаунт уже зарегистрирован");
            }
            else if(password_reg.Length < 6)
            {
                ErrorMessage("Пароль должен быть не менее 6 символов");
            }
            else if (!PasswordQuality())
            {
                ErrorMessage("Пароль должен включать цифры и буквы в двух регистрах");
            }
            else if(password_reg != passwordCheck_reg)
            {
                ErrorMessage("Пароли не совпадают");
            }
            else
            {
                MainWindow.accounts.Add(new Data.Account() { mail = mail_reg, Name = name_reg, password = password_reg, patients = new List<Data.Patient>() });
                onLogIn(MainWindow.accounts.Count - 1);
            }
            
        }

        private bool PasswordQuality()
        {
            return password_reg.Any(char.IsDigit) && password_reg.Any(char.IsLower) && password_reg.Any(char.IsUpper);
        }

        private int AccountIndex(string mail)
        {
            for(int i = 0; i < MainWindow.accounts.Count; i++)
            {
                if (MainWindow.accounts[i].mail == mail)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool ValidMail(string addr)
        {
            try
            {
                MailAddress mail = new MailAddress(addr);

                return addr == mail.Address;
            }
            catch
            {
                return false;
            }
        }

        private void PasswordLoginChanged(object sender, TextChangedEventArgs e)
        {
            password_log = password_log_f.Text;
        }

        private void MailLoginChanged(object sender, TextChangedEventArgs e)
        {
            mail_log = mail_log_f.Text;
            mail_log_f.Foreground = ValidMail(mail_log) ? Brushes.Green : Brushes.Red;
        }

        private void name_reg_f_TextChanged(object sender, TextChangedEventArgs e)
        {
            name_reg = name_reg_f.Text;
        }

        private void mail_reg_f_TextChanged(object sender, TextChangedEventArgs e)
        {
            mail_reg = mail_reg_f.Text;
            mail_reg_f.Foreground = ValidMail(mail_reg) ? Brushes.Green : Brushes.Red;
        }

        private void password_reg_f_TextChanged(object sender, TextChangedEventArgs e)
        {
            password_reg = password_reg_f.Text;
        }

        private void check_password_reg_f_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordCheck_reg = check_password_reg_f.Text;
        }
        
        private void ErrorMessage(string text)
        {
            MessageBox.Show(text, "Ошибка");
        }
    }
}
