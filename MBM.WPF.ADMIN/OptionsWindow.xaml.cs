using Common;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace MBM.WPF.ADMIN
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            RetrieveConnectionString();
        }

        /// <summary>Retrieves connection string settings and binds it to the window</summary>
        private void RetrieveConnectionString()
        {
            MbmSqlConnection DatabaseConnection = new MbmSqlConnection();
            this.DataContext = DatabaseConnection;
        }

        /// <summary>Tests the connection to the database</summary>
        private void TestConnection()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();
                ConnectionState connState;

                using (conn)
                {
                    connState = conn.State;
                }

                if (connState == ConnectionState.Open)
                {
                    ConnectionLabel.Content = "Connection was successful";
                }
                else
                {
                    ConnectionLabel.Content = "Failed to connect to database";
                }
            }
            catch (Exception ex)
            {
                ConnectionLabel.Content = ex.Message.ToString();
                LoggingService.Log(ex, "Log.txt");
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>Occurs when the connect button is clicked</summary>
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            TestConnection();
        }
    }
}
