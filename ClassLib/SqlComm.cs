/* Code obtained from http://stackoverflow.com/questions/9806166/how-to-create-sql-connection-with-c-sharp-code-behind-access-the-sql-server-the 
 * Accessed 24/04/2012
 * Modified my Stewart Collins 26/04/2012
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Security.Cryptography;
using System.Text;


public static class SqlComm
{
	// this is a shortcut for your connection string
	static string DatabaseConnectionString = " Server=tcp:rtfdhczvzu.database.windows.net; Database=averagenegativedb; UID=mtsolakis@rtfdhczvzu; Password=AverageNegative1; Trusted_Connection=False;Encrypt=True;";

	// this is for just executing sql command with no value to return
	public static void SqlExecute(string sql)
	{
		using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
		{
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
		}
	}

	// with this you will be able to return a value
	public static object SqlReturn(string sql)
	{
		using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand(sql, conn);
			object result = (object)cmd.ExecuteScalar();
			return result;
		}
	}

	// with this you can retrieve an entire table or part of it
	public static DataTable SqlDataTable(string sql)
	{
		using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
		{
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Connection.Open();
			DataTable TempTable = new DataTable();
			TempTable.Load(cmd.ExecuteReader());
			return TempTable;
		}
	}   

	// sooner or later you will probably use stored procedures. 
	// you can use this in order to execute a stored procedure with 1 parameter
	// it will work for returning a value or just executing with no returns
	public static object SqlStoredProcedure1Param(string StoredProcedure, string PrmName1, object Param1)
	{
		using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
		{
			SqlCommand cmd = new SqlCommand(StoredProcedure, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter(PrmName1, Param1.ToString()));
			cmd.Connection.Open();
			object obj = new object();
			obj = cmd.ExecuteScalar();
			return obj;
		}
	}

	//Hashing and salt functions obtained from http://forums.asp.net/t/1272055.aspx?Can+I+create+a+password+salt+ 24/04/2015

	//Generates at random password salt
	public static string CreateSalt()
	{
		RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		byte[] buff = new byte[32];
		rng.GetBytes(buff);
		return Convert.ToBase64String(buff);
	}

	//Hashes a password string
	public static string Enc(string d2e)
	{
		UnicodeEncoding uEncode = new UnicodeEncoding();
		byte[] bytD2e = uEncode.GetBytes(d2e);
		SHA256Managed sha = new SHA256Managed();
		byte[] hash = sha.ComputeHash(bytD2e);
		return Convert.ToBase64String(hash);
	}


	//Adds the input to the sql string if needed
	public static string AddIfNotNull(string sql, string input){
		if (string.IsNullOrEmpty (input)) {
			sql = sql + "null";
		} else {
			sql = sql + "'" + input + "'";
		}return sql;
	}
}