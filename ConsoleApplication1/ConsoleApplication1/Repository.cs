using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class water
    {
        private const string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\thisway\homework\ConsoleApplication1\ConsoleApplication1\waters.mdf;Integrated Security=True";


        public void Createwater(Models.water water)
        {
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();


            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
INSERT        INTO    waters(code,name,address,orcode,orname,oraddress,contactpeople,contactemail,phone,[rule],startdate,enddate,visa,remarks)
VALUES          ('{0}',N'{1}',N'{2}','{3}',N'{4}')
", water.Year, water.ExecutingUnit, water.ConsumptionOfWater, water.PopulationServed, water.TheDailyDomesticConsumptionOfWaterPerPerson, water.Remarks);


            command.ExecuteNonQuery();


            connection.Close();
        }
    }
}