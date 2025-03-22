using NUnit.Framework;
using System.IO;
using UnityEngine;
using System;

public class DatabaseManagerTest
{
    private string dbPath;
    private string logPath;

    [SetUp]
    public void SetUp()
    {
        dbPath = Application.persistentDataPath + "/EggLearningTest.db";
        logPath = Application.persistentDataPath + "/BugReport.txt";

        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }

        if (File.Exists(logPath))
        {
            File.Delete(logPath);
        }

        DatabaseManager.CreateDatabase();
    }

    [Test]
    public void TestResetDatabase()
    {
        try
        {
            DatabaseManager.ResetDatabase();
            var isProgressSaved = EggCrack.IsProgressSaved();
            Assert.IsFalse(isProgressSaved, "Database should be reset but still contains progress.");
        }
        catch (Exception ex)
        {
            LogBugReport("TestResetDatabase", ex.Message);
            throw;
        }
    }

    private void LogBugReport(string testName, string errorMessage)
    {
        using (StreamWriter sw = File.AppendText(logPath))
        {
            sw.WriteLine("======= BUG REPORT =======");
            sw.WriteLine("Test Name: " + testName);
            sw.WriteLine("Error Message: " + errorMessage);
            sw.WriteLine("Date & Time: " + DateTime.Now.ToString());
            sw.WriteLine("==========================");
            sw.WriteLine();
        }

        Debug.LogError($"Bug Report generated for {testName}. See BugReport.txt for details.");
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }
    }
}
