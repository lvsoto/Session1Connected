using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session1Connected
{
    class Program
    {

        public string host = ConfigurationManager.AppSettings["host"].ToString();
        public string port = ConfigurationManager.AppSettings["port"].ToString();
        public string oracleSID = ConfigurationManager.AppSettings["oracleSID"].ToString();
        public string username = ConfigurationManager.AppSettings["username"].ToString();
        public string password = ConfigurationManager.AppSettings["password"].ToString();
         
        public string connectionString = ConfigurationManager.ConnectionStrings["FDMOracle"].ToString();

        connectionString = String.Format(connectionString, host, port, oracleSID, username, password);

        static void Main(string[] args)
        {
            string connectionString = "DataSource=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Pooling=false;User Id=trainee1;Password=!QAZSE4;";
            IBrokerRepository _repository = new OracleSqlBrokerRepository(connectionString);
            List<Broker> brokers = _repository.GetAllBrokers();
            foreach (Broker broker in brokers)
            {
                Console.WriteLine(broker.firstName);
            }
            Console.ReadLine();
        }
    }
}
