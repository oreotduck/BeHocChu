using System.Data;
using System.Data.SQLite;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private static string dbPath = "URI=file:" + Application.persistentDataPath + "/EggLearning.db";

    public static void CreateDatabase()
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS EggStates (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Letter TEXT UNIQUE,
                    IsCracked INTEGER
                );";
                command.ExecuteNonQuery();
            }

            string[] letters = { "A", "A1", "A2", "B", "C", "D", "D1", "E", "E1", "G", "H", "I", "K", "L", "M", "N", "O", "O1", "O2", "P", "Q", "R", "S", "T", "U", "U1", "V", "X", "Y" };

            foreach (var letter in letters)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT OR IGNORE INTO EggStates (Letter, IsCracked) VALUES (@letter, 0)";
                    command.Parameters.AddWithValue("@letter", letter);
                    command.ExecuteNonQuery();
                }
            }

            Debug.Log("URI=file:" + Application.persistentDataPath + "/EggLearning.db");
        }
    }

    public static bool IsDatabaseExist()
    {
        return System.IO.File.Exists(Application.persistentDataPath + "/EggLearning.db");
    }

    public static void ResetDatabase()
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE EggStates SET IsCracked = 0;";
                command.ExecuteNonQuery();
            }
        }
    }
}
