using Oracle.ManagedDataAccess.Client;
using System;

namespace OracleCloudDB
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConfiguration.OracleDataSources.Add("orclpdb", @"<YOUR CONNECTION>");
            OracleConfiguration.WalletLocation = AppContext.BaseDirectory + "OracleWallet";

            OracleConnection orclCon = null;
            try
            {
                orclCon = new OracleConnection("user id=hr; password=<YOUR PASSWORD>; data source=orclpdb");
                orclCon.Open();
                OracleCommand orclCmd = orclCon.CreateCommand();
                orclCmd.CommandText = "SELECT FIRST_NAME FROM EMPLOYEES WHERE ROWNUM <= 10";
                OracleDataReader rdr = orclCmd.ExecuteReader();
                while (rdr.Read())
                    Console.WriteLine("Employee Name: " + rdr.GetString(0));

                Console.ReadLine();
                rdr.Dispose();
                orclCmd.Dispose();
            }
            finally
            {
                if (null != orclCon)
                    orclCon.Close();
            }
        }
    }
}
