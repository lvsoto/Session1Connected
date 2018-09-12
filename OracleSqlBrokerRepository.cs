using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session1Connected
{
    public class OracleSqlBrokerRepository : IBrokerRepository
    {

        string _connectionString;


        public OracleSqlBrokerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Broker> GetAllBrokers()
        {
            List<Broker> list = new List<Broker>();
            string _sqlStatement = "SELECT broker_id, first_name, last_name FROM brokers";
            OracleConnection connection = new OracleConnection(_connectionString);
            OracleCommand command = new OracleCommand(_sqlStatement, (OracleConnection)connection);
            try
            {
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Broker()
                    {
                        id = int.Parse((reader["broker_id"].ToString())),
                        firstName = reader["first_name"].ToString(),
                        lastName = reader["last_name"].ToString()
                    });
                }
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally 
            {
                connection.Close();
            }
            return list;
        }

        public void AddNewBroker(Broker brokerToAdd)
        {
            string _sqlStatement = "INSERT INTO brokers(broker_id, first_name, last_name) VALUES (:broker_id, :first_name, :last_name)";

            OracleConnection connection = new OracleConnection(_connectionString);
            OracleCommand command = new OracleCommand(_sqlStatement, (OracleConnection)connection);
            command.BindByName = true;

            IDbDataParameter param = new OracleParameter(":first_name", OracleDbType.Varchar2, 25);
            param.Value = brokerToAdd.firstName;
            command.Parameters.Add(param);

            param = new OracleParameter(":last_name", OracleDbType.Varchar2, 25);
            param.Value = brokerToAdd.lastName;
            command.Parameters.Add(param);

            param = new OracleParameter(":broker_id", OracleDbType.Int16, 50);
            param.Value = brokerToAdd.id;
            command.Parameters.Add(param);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally
            {
                connection.Close();
            }
            _repository.AddNewBroker(new Broker() { id = 200, firstName = "I Have Just Been Added", lastName = "Yay" });

        }

        public void UpdateBroker(Broker brokerToUpdate)
        {
            string _sqlStatement = "UPDATE brokers SET first_name = :first_name, last_name = :last_name WHERE broker_id = :broker_id"; ;

            IDbConnection connection = new OracleConnection(_connectionString);

            OracleCommand command = new OracleCommand(_sqlStatement, (OracleConnection)connection);

            command.BindByName = true;

            IDbDataParameter param = new OracleParameter(":first_name", OracleDbType.Varchar2, 25);

                 param.Value = brokerToUpdate.firstName;
                 command.Parameters.Add(param);

            param = new OracleParameter(":last_name", OracleDbType.Varchar2, 25);
            param.Value = brokerToUpdate.lastName;
            command.Parameters.Add(param);
            param = new OracleParameter(":broker_id", OracleDbType.Int16, 50);
            param.Value = brokerToUpdate.id;
            command.Parameters.Add(param);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }

        public void RemoveBroker(Broker brokerToRemove)
        {
            string _sqlStatement = "DELETE FROM BROKERS WHERE BROKER_ID = :broker_id";

            IDbConnection connection = new OracleConnection(_connectionString);

            OracleCommand command = new OracleCommand(_sqlStatement, (OracleConnection)connection);

            command.BindByName = true;

            IDbDataParameter param = new OracleParameter(":broker_id", OracleDbType.Int16, 50);

            param.Value = brokerToRemove.id;
            command.Parameters.Add(param);
            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
