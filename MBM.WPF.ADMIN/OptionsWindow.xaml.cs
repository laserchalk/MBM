using MBM.DL;
using System;
using System.Collections.Generic;
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

            MbmSqlConnection DatabaseConnection = new MbmSqlConnection();
            this.DataContext = DatabaseConnection;
        }

        MbmSqlConnection DatabaseConnection = new MbmSqlConnection();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();

                using (conn)
                {

                }
                ConnectionLabel.Content = "Connection was successful";
            }
            catch (Exception ex)
            {
                ConnectionLabel.Content = ex.Message.ToString();
            }

            Mouse.OverrideCursor = Cursors.Arrow;

        }
    }
}
