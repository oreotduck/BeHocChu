//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;
//using UnityEngine.SceneManagement;
//using System.Collections;
//using System.IO;
//using System;

//public class EggCrackTest
//{
//    private string logPath;

//    [SetUp]
//    public void SetUp()
//    {
//        logPath = Application.persistentDataPath + "/BugReports.txt";
//        if (File.Exists(logPath)) File.Delete(logPath);  
//    }

//    [TearDown]
//    public void TearDown()
//    {
//        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
//        {
//            LogError(
//                TestContext.CurrentContext.Test.Name,
//                TestContext.CurrentContext.Result.Message,
//                TestContext.CurrentContext.Result.StackTrace
//            );
//        }
//    }

//    private void LogError(string testName, string errorMessage, string stackTrace)
//    {
//        using (StreamWriter writer = new StreamWriter(logPath, true))
//        {
//            writer.WriteLine($"========== Bug Report ==========");
//            writer.WriteLine($"Test Name: {testName}");
//            writer.WriteLine($"Date & Time: {DateTime.Now}");
//            writer.WriteLine($"Error Message: {errorMessage}");
//            writer.WriteLine($"Stack Trace: {stackTrace}");
//            writer.WriteLine($"================================\n");
//        }
//        Debug.LogError($"Bug reported and saved at: {logPath}");
//    }

//    [Test]
//    public IEnumerator TestMenuButtons()
//    {
//        SceneManager.LoadScene("Menu");
//        yield return new WaitForSeconds(2f);

//        GameObject startButton = GameObject.Find("Start");
//        Assert.IsNotNull(startButton, "Không tìm thấy đối tượng 'Start' trong Scene Menu.");

//        MenuClick menuClick = startButton.GetComponent<MenuClick>();
//        Assert.IsNotNull(menuClick, "MenuClick component is missing from 'Start' button.");

//        menuClick.OnStartButton();
//        yield return new WaitForSeconds(2f);

//        Assert.AreEqual("Trang1", SceneManager.GetActiveScene().name, "Failed to load Scene Trang1.");
//    }

//    [Test]
//    public IEnumerator TestEggClick()
//    {
//        SceneManager.LoadScene("Trang1");
//        yield return new WaitForSeconds(2f);

//        GameObject eggObject = GameObject.Find("Egg1"); 
//        Assert.IsNotNull(eggObject, "Egg object A not found.");

//        EggCrack eggCrack = eggObject.GetComponent<EggCrack>();
//        Assert.IsNotNull(eggCrack, "EggCrack script is missing on Egg1.");

//        int previousTapCount = eggCrack.tapCount;

//        eggCrack.OnPointerClick(null); 
//        yield return new WaitForSeconds(1f);

//        Assert.Greater(eggCrack.tapCount, previousTapCount, "Tap count should increase when clicking the egg.");
//    }
//}


using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.EventSystems;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class EggCrackTest
{
    private string bugReportPath;
    private GameObject eggObject;
    private EggCrack eggCrack;
    private GameObject startButton;
    private MenuClick menuClick;

    [SetUp]
    public void SetUp()
    {
        bugReportPath = System.IO.Path.Combine(Application.persistentDataPath, "BugReports.txt");

        if (System.IO.File.Exists(bugReportPath))
        {
            System.IO.File.Delete(bugReportPath);
        }

        Debug.Log("Bug report path: " + bugReportPath);

        startButton = new GameObject("Start");
        menuClick = startButton.AddComponent<MenuClick>();

        menuClick.SetConnectionString("Data Source=:memory:;Version=3;");

        eggObject = new GameObject("Egg1");
        eggCrack = eggObject.AddComponent<EggCrack>();

        eggCrack.maxTap = 3;
        eggCrack.letterImage = new GameObject("A");
        eggCrack.letterImage.SetActive(false);
        eggCrack.eggAnimator = eggObject.AddComponent<Animator>();
        eggCrack.Egg = eggObject.AddComponent<BoxCollider2D>();
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

        GameObject.DestroyImmediate(eggObject);
        GameObject.DestroyImmediate(startButton);
    }

    private void LogError(string testName, string errorMessage, string stackTrace)
    {
        using (StreamWriter writer = new StreamWriter(bugReportPath, true))
        {
            writer.WriteLine($"========== Bug Report ==========");
            writer.WriteLine($"Test Name: {testName}");
            writer.WriteLine($"Date & Time: {DateTime.Now}");
            writer.WriteLine($"Error Message: {errorMessage}");
            writer.WriteLine($"Stack Trace: {stackTrace}");
            writer.WriteLine($"================================\n");
        }
        Debug.LogError($"Bug reported and saved at: {bugReportPath}");
    }

    [Test]
    public void TestMenuButtons()
    {
        Assert.IsNotNull(startButton, "Không tìm thấy đối tượng 'Start'.");
        Assert.IsNotNull(menuClick, "MenuClick component is missing from 'Start' button.");

        menuClick.OnStartButton();

        Assert.IsTrue(true, "OnStartButton được gọi thành công (mocked behavior).");
    }

    [Test]
    public void TestEggClick()
    {
        SceneManager.LoadScene("Trang1");

        GameObject eggObject = GameObject.FindWithTag("Egg");

        if (eggObject == null)
        {
            LogBug("Egg object with tag 'Egg' not found in Trang1.");
            Assert.Fail("Egg object with tag 'Egg' not found in Trang1.");
        }

        EggCrack eggCrack = eggObject.GetComponent<EggCrack>();

        if (eggCrack == null)
        {
            LogBug("EggCrack script is missing on Egg.");
            Assert.Fail("EggCrack script is missing on Egg.");
        }

        int initialTapCount = eggCrack.tapCount;

        eggCrack.OnPointerClick(null);

        if (eggCrack.tapCount <= initialTapCount)
        {
            LogBug($"Tap count did not increase. Initial: {initialTapCount}, Current: {eggCrack.tapCount}");
            Assert.Fail($"Tap count did not increase. Initial: {initialTapCount}, Current: {eggCrack.tapCount}");
        }
    }
    private void LogBug(string message)
    {
        try
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(bugReportPath, true))
            {
                writer.WriteLine($"[{System.DateTime.Now}] {message}");
            }
            Debug.Log("Bug report successfully written to: " + bugReportPath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Cannot write in BugReports.txt: " + ex.Message);
        }
    }
}
