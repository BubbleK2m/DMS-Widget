using DomitoryWidget.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace DomitoryWidget.View
{
    /// <summary>
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            var id = IDText.Text;
            var pw = PWText.Password;

            var response = DMS.Auth(id, pw);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("로그인 실패");
                return;
            }

            SetTokensFromReponse(response);
            mainWindow.NavigatePage(new MainPage());
        }

        private void SetTokensFromReponse(RestResponse response)
        {
            var content = JObject.Parse(response.Content);

            mainWindow.AccesssToken = content["access_token"].ToString();
            mainWindow.RefreshToken = content["refresh_token"].ToString();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // mainWindow.NavigatePage(new RegisterPage());
        }
    }
}

