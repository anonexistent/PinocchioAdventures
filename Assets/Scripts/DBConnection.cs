using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBConnection : MonoBehaviour
{
    [Header("Database Properties")]
    public string Host = "localhost";
    public string User = "root";
    public string Password = "root";
    public string Database = "test";

    //public string Server { get; set; }
    //public string DatabaseName { get; set; }
    //public string UserName { get; set; }
    //public string Password { get; set; }

    //public MySqlConnection Connection { get; set; }

    //private static DBConnection _instance = null;
    //public static DBConnection Instance()
    //{
    //    if (_instance == null)
    //        _instance = new DBConnection();
    //    return _instance;
    //}

    //public bool IsConnect()
    //{
    //    if (Connection == null)
    //    {
    //        if (String.IsNullOrEmpty(DatabaseName))
    //            return false;
    //        string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
    //        Connection = new MySqlConnection(connstring);
    //        Connection.Open();
    //    }

    //    return true;
    //}

    //public void Close()
    //{
    //    Connection.Close();
    //}

    public MySqlConnection Connection()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = Host;
        builder.UserID = User;
        builder.Password = Password;
        builder.Database = Database;

        MySqlConnection connection = new(builder.ToString());

        try
        {
            using (connection)
            {
                connection.Open();
                print("MySQL - Opened Connection");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        return connection;
    }
}
