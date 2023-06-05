//using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

public class DBConnection
{
    private DBConnection()
    {
    }

    public string Server { get; set; }
    public string DatabaseName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    //public MySqlConnection Connection { get; set; }

    private static DBConnection _instance = null;
    public static DBConnection Instance()
    {
        //if (_instance == null)
        //    _instance = new DBConnection();
        return _instance;
    }

    public bool IsConnect()
    {
        //if (Connection == null)
        //{
        //    if (String.IsNullOrEmpty(DatabaseName))
        //        return false;
        //    string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
        //    Connection = new MySqlConnection(connstring);
        //    Connection.Open();
        //}

        return true;
    }

    public static void MySqlNewScore()
    {
        //var dbCon = Instance();
        //dbCon.Server = "127.0.0.1";
        //dbCon.DatabaseName = "test";
        //dbCon.UserName = "root";
        //dbCon.Password = "";
        //if (dbCon.IsConnect())
        //{
        //    //suppose col0 and col1 are defined as VARCHAR in the DB
        //    string query = $"INSERT INTO `users` (`name`) VALUES ('{StarCollector.starCount}')";
        //    var cmd = new MySqlCommand(query, dbCon.Connection);
        //    var reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        string someStringFromColumnZero = reader.GetString(0);
        //        string someStringFromColumnOne = reader.GetString(1);
        //        Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
        //    }
        //    dbCon.Close();
        //}
    }

    public void Close()
    {
        //Connection.Close();
    }

}
