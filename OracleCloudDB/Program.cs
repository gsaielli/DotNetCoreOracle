using Oracle.ManagedDataAccess.Client;
using System;

namespace OracleCloudDB
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConfiguration.OracleDataSources.Add("orclpdb", @"(description= (retry_count=20)(retry_delay=3)(address=(protocol=tcps)(port=1522)(host=adb.eu-zurich-1.oraclecloud.com))(connect_data=(service_name=cfdnkbdx0aohvkj_db202005200815_high.atp.oraclecloud.com))(security=(ssl_server_cert_dn=""CN=adb.eu-zurich-1.oraclecloud.com,OU=Oracle ADB ZURICH,O=Oracle Corporation,L=Redwood City,ST=California,C=US"")))");
            OracleConfiguration.WalletLocation = AppContext.BaseDirectory + "OracleWallet";

            OracleConnection orclCon = null;
            try
            {
                orclCon = new OracleConnection("user id=hr; password=.7N3FgQ+!.!z6qr; data source=orclpdb");
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
