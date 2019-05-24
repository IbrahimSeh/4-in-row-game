using FourInRowClient.FourInRowServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FourInRowClient
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void submit_click(object sender, RoutedEventArgs e)
        {
            if (allFieldsFilled())
            {
                string userId = tbUserId.Text.Trim();
                string userName = tbUsername.Text.Trim();
                string password = tbPassword.Text;

                ClientCallback callback = new ClientCallback();
                Four_in_rowServiceClient client = new Four_in_rowServiceClient(
                    new InstanceContext(callback));

                try
                {
                    client.Register(userId, userName, password);
                    this.Close();                  
                   /* MainWindow mainWindow = new MainWindow();
                    mainWindow.Client = client;
                    mainWindow.Callback = callback;
                    mainWindow.Username = userId;
                    mainWindow.Title = userId;
                    this.Close();
                    mainWindow.Show();*/
                }
                catch (FaultException<UserIdExistsFault> ex)
                {
                    MessageBox.Show(ex.Detail.Message);
                }
            }
        }

        private bool allFieldsFilled()
        {
            if (string.IsNullOrEmpty(tbUserId.Text.Trim()) ||
               string.IsNullOrEmpty(tbUsername.Text.Trim()) ||
               string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                return false;
            }
            return true;
        }
    }
}
