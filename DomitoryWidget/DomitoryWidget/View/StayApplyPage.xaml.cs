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
    /// StayApplyPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StayApplyPage : Page
    {
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public StayApplyPage()
        {
            InitializeComponent();
            LoadStayApply();
        }
        
        private void LoadStayApply()
        {
            var response = DMS.GetStayApply(mainWindow.AccesssToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("잔류신청 조회 실패");
                mainWindow.NavigatePage(new LoginPage());

                return;
            }

            SetApplyRadiosFromResponse(response);
        }
        
        private void SetApplyRadiosFromResponse(RestResponse response)
        {
            var content = JObject.Parse(response.Content);
            var stay = content.Value<int?>("value") ?? 0;

            switch (stay)
            {
                case 1: FridayHomecomingRadio.IsChecked = true; break;
                case 2: SaturdayHomecomingRadio.IsChecked = true; break;
                case 3: SaturdayComebackRadio.IsChecked = true; break;
                case 4: StayDormitoryRadio.IsChecked = true; break;
            }
        }

        private void CancelApplyButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MoveBackPage();
        }

        private void SubmitApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitStayApply();
        }

        private void SubmitStayApply()
        {
            var stay = GetStayApplyFromRadios();
            var response = DMS.SetStayApply(stay, mainWindow.AccesssToken);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show($"잔류신청 실패");
            }

            else
            {
                MessageBox.Show($"잔류신청 성공");
            }
            
            mainWindow.NavigatePage(new MainPage());
        }

        private int GetStayApplyFromRadios()
        {
            if ((bool)FridayHomecomingRadio.IsChecked) return 1;
            else if ((bool)SaturdayHomecomingRadio.IsChecked) return 2;
            else if ((bool)SaturdayComebackRadio.IsChecked) return 3;
            else if ((bool)StayDormitoryRadio.IsChecked) return 4;
            else return 0;
        }
    }
}
