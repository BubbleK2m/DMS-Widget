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
    /// GoingoutApplyPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GoingoutApplyPage : Page
    {
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public GoingoutApplyPage()
        {
            InitializeComponent();
            LoadGoingoutApply();
        }

        private void LoadGoingoutApply()
        {
            var response = DMS.GetGoingoutApply(mainWindow.AccesssToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("외출신청 조회 실패");
                mainWindow.NavigatePage(new GoingoutApplyPage());

                return;
            }

            SetGoingoutChecksFromResponse(response);
        }

        private void SetGoingoutChecksFromResponse(RestResponse response)
        {
            var content = JObject.Parse(response.Content);

            SaturdayGoingoutCheck.IsChecked = (bool)content["sat"];
            SundayGoingoutCheck.IsChecked = (bool)content["sun"];
        }

        private void CancelApplyButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MoveBackPage();
        }

        private void SubmitApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitApply();
        }

        private void SubmitApply()
        {
            var saturdayGoingout = (bool)SaturdayGoingoutCheck.IsChecked;
            var sundayGoingout = (bool)SundayGoingoutCheck.IsChecked;
            var response = DMS.SetGoingoutApply(saturdayGoingout, sundayGoingout, mainWindow.AccesssToken);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show("외출신청 실패");
            }

            else
            {
                MessageBox.Show("외출신청 성공");
            }

            mainWindow.NavigatePage(new MainPage());
        }
    }
}
