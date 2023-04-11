using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSemana04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection("Data Source = LAB707-04\\SQLEXPRESS01; Initial Catalog = DBSem04; Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Forma Conectada Procedimiento Alamcenado
            List<Person> people = new List<Person>();

            conn.Open();
            SqlCommand command = new SqlCommand("USP_GetPeople",conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            parameter1.Value = "";
            parameter1.ParameterName = "@LastName";

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;
            parameter2.Value = "";
            parameter2.ParameterName = "@FirstName";

            SqlParameter parameter3 = new SqlParameter();
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Value = 0;
            parameter3.ParameterName = "@Age";

            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);

            SqlDataReader dataReader = command.ExecuteReader();

            while(dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader["PeopleID"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),
                    Age = (int)dataReader["Age"],
                    FullName = string.Concat(dataReader["FirstName"].ToString()," ",
                    dataReader["LastName"].ToString())
                });
            }

            conn.Close();
            dgvPeople.ItemsSource = people;

        }

        private void btnNuevo_click(object sender, RoutedEventArgs e) 
        { }
    }
}
