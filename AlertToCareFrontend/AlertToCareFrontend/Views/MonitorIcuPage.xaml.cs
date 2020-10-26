using AlertToCareFrontend.Models;
using AlertToCareFrontend.ViewModel;
using AlertToCareFrontend.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlertToCareFrontend
{
    /// <summary>
    /// Interaction logic for MonitorIcuPage.xaml
    /// </summary>
    public partial class MonitorIcuPage : Page
    {
        public MonitorIcuPage()
        {
            InitializeComponent();

            MonitorIcuViewModel viewModel = new MonitorIcuViewModel();
            DataContext = viewModel;

            int count = viewModel.BedDataModelList.Count;
            int bedNum = 1;

            foreach (BedDataModel bedDataModel in viewModel.BedDataModelList)
            {
                BedView bed = new BedView(bedDataModel);
                
                if(bedNum <= count/2)
                {
                    leftStackPanel.Children.Add(bed);
                }
                else
                {
                    bottomStackPanel.Children.Add(bed);
                }
                bedNum++;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new HomePage());
            }
        }
    }
}
