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
using DT = System.Data;            // System.Data.dll  
using QC = System.Data.SqlClient;  // System.Data.dll  

namespace TestingCSTDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var connection = new QC.SqlConnection(
                   "Server=tcp:cstquiz-server.database.windows.net,1433;Database=cstquiz-db;User ID=group3;Password=Branches!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                   ))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");
                    MainWindow.SelectRows(connection);

                } catch
                {
                    Console.WriteLine("Unsuccessful");
                }
            }
        }

        static public void SelectRows(QC.SqlConnection connection)
        {
            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"SELECT * FROM questions; SELECT * FROM answers";

                QC.SqlDataReader reader = command.ExecuteReader();
 
                while (reader.Read())
                {
                    Console.WriteLine("Q{0}.\t{1}\t{2}\t{3}\t{4}\t{5}",
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5));
                }
            }
        }

    }
}
