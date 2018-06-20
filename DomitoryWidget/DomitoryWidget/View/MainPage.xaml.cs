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
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Page
    {
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public MainPage()
        {
            InitializeComponent();
            LoadMyPage();
        }

        private void LoadMyPage()
        {
            var response = DMS.MyPage(mainWindow.AccesssToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("마이페이지 로딩 실패");
                mainWindow.NavigatePage(new LoginPage());

                return;
            }

            SetMyPageFromReponse(response);
        }

        private void SetMyPageFromReponse(RestResponse response)
        {
            var content = JObject.Parse(response.Content);

            ElevenExtensionLabel.Content = ParseStayWeekend(content.Value<int?>("extension_11") ?? 0);
            TwelveExtensionLabel.Content = ParseStayWeekend(content.Value<int?>("extension_12") ?? 0);
            StayWeekendLabel.Content = ParseStayWeekend(content.Value<int?>("stay_value") ?? 0);

            SaturdayGoingoutLabel.Content = ParseGoingout(content["goingout"].Value<bool>("sat"));
            SundayGoingoutLabel.Content = ParseGoingout(content["goingout"].Value<bool>("sun"));
        }

        private static string ParseExtension(int extension)
        {
            switch (extension)
            {
                case 1: return "가온실";
                case 2: return "나온실";
                case 3: return "다온실";
                case 4: return "라온실";
                case 5: return "3층 독서실";
                case 6: return "4층 독서실";
                case 7: return "5층 열린교실";
                case 8: return "2층 여자 자습실";
                default: return "미신청";
            }
        }

        private static string ParseStayWeekend(int stay)
        {
            switch (stay)
            {
                case 1: return "금요귀가";
                case 2: return "토요귀가";
                case 3: return "토요귀사";
                case 4: return "잔류";
                default: return "미신청";
            }
        }

        private static string ParseGoingout(bool goingout)
        {
            return goingout ? "신청" : "미신청";
        }

        private void StayApplyButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigatePage(new StayApplyPage());
        }
    }
}
