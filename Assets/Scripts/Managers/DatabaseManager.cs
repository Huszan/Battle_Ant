
using System;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public static class DatabaseManager
{
    private static string dbName = "URI=file:mainDB.db";

    public static List<User> GetUsers()
    {
        List<User> listOfUsers = new List<User>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = 
                    "SELECT * FROM User " +
                    "INNER JOIN Setting ON User.setting_id = Setting.id;";

                using(IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string username = (string)reader["name"];

                        User currentUser = new User(
                            username,
                            GetSetting(username));

                        listOfUsers.Add(currentUser);
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        return listOfUsers;
    }
    public static string GetUsername(int id)
    {
        List<int> listOfUsers = new List<int>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT * FROM User;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (id == (int)reader["id"])
                            return (string)reader["name"];
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        return null;
    }
    public static void AddUser(string username)
    {
        if (UserExists(username))
        {
            Debug.LogWarning("User with this username already exists");
            return;
        }  
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            
            using (var command = connection.CreateCommand())
            {
                command.CommandText =

                    "BEGIN TRANSACTION;" +

                    "INSERT OR ROLLBACK INTO Setting DEFAULT VALUES; " +

                    "INSERT OR ROLLBACK INTO User(name, setting_id) " +
                    "VALUES('" + username + "', last_insert_rowid()); " +

                    "COMMIT TRANSACTION;";

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public static void UpdateUser(User user)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT id, setting_id FROM User WHERE name = '" + user.name + "';";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        using (var updateCommand = connection.CreateCommand())
                        {
                            updateCommand.CommandText =
                                "UPDATE User " +
                                "SET name = '" + user.name + "' " +
                                "WHERE id = '" + reader["id"] + "';";
                            updateCommand.ExecuteNonQuery();
                        }
                        using (var updateCommand = connection.CreateCommand())
                        {
                            Setting setting = user.setting;
                            updateCommand.CommandText =
                                "UPDATE Setting " +
                                "SET screen_width = '" + setting.screenWidth + "', " +
                                "screen_height = '" + setting.screenHeight + "', " +
                                "fullscreen = '" + Convert.ToInt32(setting.fullscreen) + "', " +
                                "refresh_rate = '" + setting.refreshRate + "', " +
                                "volume_master = '" + setting.volume.master + "', " +
                                "volume_sfx = '" + setting.volume.sfx + "', " +
                                "volume_music = '" + setting.volume.music + "' " +
                                "WHERE id = '" + reader["setting_id"] + "';";
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            connection.Close();
        }
    }
    public static void DeleteUser(string username)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT id,setting_id FROM User WHERE name = '" + username + "';";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        using (var deleteCommand = connection.CreateCommand())
                        {
                            deleteCommand.CommandText =
                                "DELETE FROM User WHERE id = '" + reader.GetInt32(0) + "';" +
                                "DELETE FROM Setting WHERE id = '" + reader.GetInt32(1) + "';";
                            deleteCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }
    }
    private static bool UserExists(string username)
    {
        bool exist = false;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT name FROM User;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["name"].ToString() == username)
                            exist = true;
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        return exist;
    }

    public static Setting GetSetting(string username)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT name, volume_master, volume_sfx, volume_music, screen_width, screen_height, fullscreen, refresh_rate FROM User " +
                    "INNER JOIN Setting ON User.setting_id = Setting.id;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (username == (string)reader["name"])
                        {
                            Setting setting = new Setting();

                            setting.volume = new Volume(
                                reader.GetFloat(1),
                                reader.GetFloat(2),
                                reader.GetFloat(3));
                            setting.SetResolution(
                                reader.GetInt32(4),
                                reader.GetInt32(5),
                                reader.GetBoolean(6),
                                reader.GetInt32(7));

                            return setting;
                        }
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        return null;
    }

}
