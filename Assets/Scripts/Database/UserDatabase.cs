using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class UserDatabase : MonoBehaviour
{

    private string dbName = "URI=file:Users.db";

    void Start()
    {
        CreateDB();
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = 
                    "CREATE TABLE IF NOT EXISTS users (name VARCHAR(20));";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddUser(User user)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO users (name) VALUES ('" + user.name + "');";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void DeleteUser(User user)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "DELETE FROM users WHERE name = '"+user.name+"';";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public List<User> GetUsers()
    {
        List<User> users = new List<User>();
        
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT * FROM users;";
               
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User((string)reader["name"]));
                    }  
                }
            }
        }
        return users;
    }

}
