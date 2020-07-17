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
using System.IO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace WpfApp4
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
        public void CommandToDB(string msg)
        {
            var host = "mysql11.hostland.ru";
            var database = "host1323541_itstep25";
            var port = "3306";
            var username = "host1323541_itstep";
            var pass = "269f43dc";
            var ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
            MySqlConnection db = new MySqlConnection(ConnString);
            db.Open();
            string sql = msg;
            MySqlCommand command = new MySqlCommand { Connection = db, CommandText = sql };
            MySqlDataReader result = command.ExecuteReader();

        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

            var name = input_name.Text;
            var phone = input_phone.Text;

            if ((name == String.Empty) || (phone == String.Empty))
            {
                MessageBox.Show("Одно или оба поля не заполнены.", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string msg = $"INSERT INTO Phones (name, phone) VALUES ('{name}','{phone}');";
                CommandToDB(msg);
                MessageBox.Show("Абонент успешно добавлен", "УСПЕХ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_import_Click(object sender, RoutedEventArgs e)
        {
            string[] line_inFile;

            using (StreamReader sr = new StreamReader("reg.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line_inFile = line.Split(new char[] { ' ' });
                    string name_inFile = line_inFile[0];
                    string phone_inFile = line_inFile[1];
                    string msg = $"INSERT INTO Phones (name, phone) VALUES ('{name_inFile}','{phone_inFile}');";
                    CommandToDB(msg);
                    MessageBox.Show("Все абоненты из файла были добавлены в телефонную книгу", "УСПЕХ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btn_export_Click(object sender, RoutedEventArgs e)
        {
            var host = "mysql11.hostland.ru";
            var database = "host1323541_itstep25";
            var port = "3306";
            var username = "host1323541_itstep";
            var pass = "269f43dc";
            var ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
            MySqlConnection db = new MySqlConnection(ConnString);
            db.Open();
            string sql = "SELECT name , phone FROM Phones WHERE id;";
            MySqlCommand command = new MySqlCommand { Connection = db, CommandText = sql };
            string result = "";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result += (reader[0].ToString() + " " + reader[1].ToString()+"\n");
                }
               
            }
            using StreamWriter file = new StreamWriter("log.txt");
            file.WriteLine(result);


        }
    }
}
