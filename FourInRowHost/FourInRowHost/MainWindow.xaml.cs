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
using WcfFourInRowServer;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace FourInRowHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ServiceHost host;

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                host = new ServiceHost(typeof(Four_in_rowService)/*,new Uri("Http://localhost:8000/FourInARowService")*/);
                host.Description.Behaviors.Add(
                    new ServiceMetadataBehavior { HttpGetEnabled = true });
                host.Open();
                lb.Content = "Host Connected...";
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (host != null)
            {
                host.Close();
            }
        }
    }
}
