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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text))
            {
                ClientCallback callback = new ClientCallback();
                Four_in_rowServiceClient client = new Four_in_rowServiceClient(
                    new InstanceContext(callback));
                string userId = tbUsername.Text.Trim();
                string password = tbPassword.Text.Trim();
                try
                {
                    client.Connect(userId, password);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Client = client;
                    mainWindow.Callback = callback;
                    mainWindow.Username = userId;
                    mainWindow.Title = userId;
                    this.Close();
                    mainWindow.Show();
                }
                catch (FaultException<UserPasswordFault> ex)
                {
                    MessageBox.Show(ex.Detail.Message);
                }
                catch (FaultException<UserIdNotExistFault> ex)
                {
                    MessageBox.Show(ex.Detail.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            this.Close();
            registerWindow.Show();
        }
    }

}
