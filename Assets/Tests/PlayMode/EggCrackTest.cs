using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System;

public class EggCrackTest
{
    private string logPath;

    [SetUp]
    public void SetUp()
    {
        logPath = Application.persistentDataPath + "/BugReports.txt";
        if (File.Exists(logPath)) File.Delete(logPath);  
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            LogError(
                TestContext.CurrentContext.Test.Name,
                TestContext.CurrentContext.Result.Message,
                TestContext.CurrentContext.Result.StackTrace
            );
        }
    }

    private void LogError(string testName, string errorMessage, string stackTrace)
    {
        using (StreamWriter writer = new StreamWriter(logPath, true))
        {
            writer.WriteLine($"========== Bug Report ==========");
            writer.WriteLine($"Test Name: {testName}");
            writer.WriteLine($"Date & Time: {DateTime.Now}");
            writer.WriteLine($"Error Message: {errorMessage}");
            writer.WriteLine($"Stack Trace: {stackTrace}");
            writer.WriteLine($"================================\n");
        }
        Debug.LogError($"Bug reported and saved at: {logPath}");
    }

    [Test]
    public IEnumerator TestMenuButtons()
    {
        SceneManager.LoadScene("Menu");
        yield return new WaitForSeconds(2f);

        GameObject startButton = GameObject.Find("Start");
        Assert.IsNotNull(startButton, "Không tìm thấy đối tượng 'Start' trong Scene Menu.");

        MenuClick menuClick = startButton.GetComponent<MenuClick>();
        Assert.IsNotNull(menuClick, "MenuClick component is missing from 'Start' button.");

        menuClick.OnStartButton();
        yield return new WaitForSeconds(2f);

        Assert.AreEqual("Trang1", SceneManager.GetActiveScene().name, "Failed to load Scene Trang1.");
    }

    [Test]
    public IEnumerator TestEggClick()
    {
        SceneManager.LoadScene("Trang1");
        yield return new WaitForSeconds(2f);

        GameObject eggObject = GameObject.Find("Egg1"); 
        Assert.IsNotNull(eggObject, "Egg object A not found.");

        EggCrack eggCrack = eggObject.GetComponent<EggCrack>();
        Assert.IsNotNull(eggCrack, "EggCrack script is missing on Egg1.");

        int previousTapCount = eggCrack.tapCount;

        eggCrack.OnPointerClick(null); 
        yield return new WaitForSeconds(1f);

        Assert.Greater(eggCrack.tapCount, previousTapCount, "Tap count should increase when clicking the egg.");
    }
}
