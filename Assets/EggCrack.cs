//using System.Data;
//using System.Data.SQLite;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class EggCrack : MonoBehaviour, IPointerClickHandler
//{
//    public Animator eggAnimator;
//    public BoxCollider2D Egg;
//    public string letter;
//    public GameObject letterImage;
//    public int tapCount = 0;
//    public int maxTap = 3;
//    private bool isCracked = false;

//    private string dbPath;

//    private void Start()
//    {
//        eggAnimator = GetComponent<Animator>();
//        Egg = GetComponent<BoxCollider2D>();
//        tapCount = 0;

//        dbPath = "URI=file:" + Application.persistentDataPath + "/EggLearning.db";

//        if (!DatabaseManager.IsDatabaseExist())
//        {
//            DatabaseManager.CreateDatabase();
//        }

//        int crackedState = GetProgress(letter);
//        if (crackedState >= maxTap)
//        {
//            ShowLetter();
//            isCracked = true;
//        }
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (isCracked || !CanBreakEgg()) return;

//        Debug.Log("Egg Click!");
//        tapCount++;
//        eggAnimator.SetInteger("TapCount", tapCount);

//        if (tapCount >= maxTap)
//        {
//            isCracked = true;
//            SaveProgress(letter, tapCount);
//            ShowLetter();
//        }
//    }

//    private void ShowLetter()
//    {
//        letterImage.SetActive(true);
//        gameObject.SetActive(false);
//    }

//    private void SaveProgress(string letter, int isCracked)
//    {
//        using (var connection = new SQLiteConnection(dbPath))
//        {
//            connection.Open();
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "UPDATE EggStates SET IsCracked = @isCracked WHERE Letter = @letter";
//                command.Parameters.AddWithValue("@isCracked", isCracked);
//                command.Parameters.AddWithValue("@letter", letter);
//                command.ExecuteNonQuery();
//            }
//        }
//    }

//    private int GetProgress(string letter)
//    {
//        using (var connection = new SQLiteConnection(dbPath))
//        {
//            connection.Open();
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "SELECT IsCracked FROM EggStates WHERE Letter = @letter";
//                command.Parameters.AddWithValue("@letter", letter);
//                var result = command.ExecuteScalar();
//                return result != null ? int.Parse(result.ToString()) : 0;
//            }
//        }
//    }

//    private bool CanBreakEgg()
//    {
//        using (var connection = new SQLiteConnection(dbPath))
//        {
//            connection.Open();
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "SELECT COUNT(*) FROM EggStates WHERE Letter < @letter AND IsCracked < 3";
//                command.Parameters.AddWithValue("@letter", letter);
//                var result = command.ExecuteScalar();
//                int count = result != null ? int.Parse(result.ToString()) : 0;
//                return count == 0;
//            }
//        }
//    }


//    public static bool IsDatabaseExist()
//    {
//        return System.IO.File.Exists(Application.persistentDataPath + "/EggLearning.db");
//    }

//    public static void ResetDatabase()
//    {
//        using (var connection = new SQLiteConnection("URI=file:" + Application.persistentDataPath + "/EggLearning.db"))
//        {
//            connection.Open();
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "UPDATE EggStates SET IsCracked = 0;";
//                command.ExecuteNonQuery();
//            }
//        }
//    }

//    public static bool IsProgressSaved()
//    {
//        using (var connection = new SQLiteConnection("URI=file:" + Application.persistentDataPath + "/EggLearning.db"))
//        {
//            connection.Open();
//            using (var command = connection.CreateCommand())
//            {
//                command.CommandText = "SELECT COUNT(*) FROM EggStates WHERE IsCracked > 0;";
//                var result = command.ExecuteScalar();
//                int count = result != null ? int.Parse(result.ToString()) : 0;
//                return count > 0;
//            }
//        }
//    }

//}


using System.Data;
using System.Data.SQLite;
using UnityEngine;
using UnityEngine.EventSystems;

public class EggCrack : MonoBehaviour, IPointerClickHandler
{
    public Animator eggAnimator;
    public BoxCollider2D Egg;
    public string letter;
    public GameObject letterImage;
    public int tapCount = 0;
    public int maxTap = 3;
    private bool isCracked = false;

    private string dbPath;

    private void Start()
    {
        eggAnimator = GetComponent<Animator>();
        Egg = GetComponent<BoxCollider2D>();
        tapCount = 0;

        dbPath = "URI=file:" + Application.persistentDataPath + "/EggLearning.db";

        if (!DatabaseManager.IsDatabaseExist())
        {
            DatabaseManager.CreateDatabase();
        }

        int crackedState = GetProgress(letter);
        if (crackedState >= maxTap)
        {
            ShowLetter();
            isCracked = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCracked || !CanBreakEgg()) return;

        Debug.Log("Egg Click!");
        tapCount++;
        eggAnimator.SetInteger("TapCount", tapCount);

        if (tapCount >= maxTap)
        {
            isCracked = true;
            SaveProgress(letter, tapCount);
            ShowLetter();
        }
    }

    private void ShowLetter()
    {
        letterImage.SetActive(true);
        gameObject.SetActive(false);
    }

    private void SaveProgress(string letter, int isCracked)
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE EggStates SET IsCracked = @isCracked WHERE Letter = @letter";
                command.Parameters.AddWithValue("@isCracked", isCracked);
                command.Parameters.AddWithValue("@letter", letter);
                command.ExecuteNonQuery();
            }
        }
    }

    private int GetProgress(string letter)
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT IsCracked FROM EggStates WHERE Letter = @letter";
                command.Parameters.AddWithValue("@letter", letter);
                var result = command.ExecuteScalar();
                return result != null ? int.Parse(result.ToString()) : 0;
            }
        }
    }

    private bool CanBreakEgg()
    {
        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM EggStates WHERE Letter < @letter AND IsCracked < 3";
                command.Parameters.AddWithValue("@letter", letter);
                var result = command.ExecuteScalar();
                int count = result != null ? int.Parse(result.ToString()) : 0;
                return count == 0;
            }
        }
    }


    public static bool IsDatabaseExist()
    {
        return System.IO.File.Exists(Application.persistentDataPath + "/EggLearning.db");
    }

    public static void ResetDatabase()
    {
        using (var connection = new SQLiteConnection("URI=file:" + Application.persistentDataPath + "/EggLearning.db"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE EggStates SET IsCracked = 0;";
                command.ExecuteNonQuery();
            }
        }
    }

    public static bool IsProgressSaved()
    {
        using (var connection = new SQLiteConnection("URI=file:" + Application.persistentDataPath + "/EggLearning.db"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM EggStates WHERE IsCracked > 0;";
                var result = command.ExecuteScalar();
                int count = result != null ? int.Parse(result.ToString()) : 0;
                return count > 0;
            }
        }
    }

}

