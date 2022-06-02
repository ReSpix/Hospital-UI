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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string mail;
        private string password;

        private List<Data.Account> accounts;
        private Action<int> onLogIn;

        public LoginWindow(List<Data.Account> account, Action<int> onLogIn)
        {
            InitializeComponent();
            accounts = account;
            this.onLogIn = onLogIn;
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            int idx = AccountIndex();
            if (string.IsNullOrEmpty(mail))
            {
                ErrorMessage("Введите почту");
            }
            else if (string.IsNullOrEmpty(password))
            {
                ErrorMessage("Введите пароль");
            }
            else if(idx == -1)
            {
                ErrorMessage("Нет такого аккаунта");
            }
            else if(accounts[idx].password != password)
            {
                ErrorMessage("Неправильный пароль");
            }
            else
            {
                onLogIn.Invoke(idx);
            }
        }

        private void ErrorMessage(string text)
        {
            MessageBox.Show(text, "Ошибка");
        }

        private int AccountIndex()
        {
            for(int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].mail == mail)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Register(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordChanged(object sender, TextChangedEventArgs e)
        {
            password = password_f.Text;
        }

        private void MailChanged(object sender, TextChangedEventArgs e)
        {
            mail = mail_f.Text;
        }
    }
}
