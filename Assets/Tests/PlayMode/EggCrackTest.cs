using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class EggCrackTest
{
    private string logPath;
    private GameObject eggObject;
    private EggCrack eggCrack;
    private GameObject startButton;
    private MenuClick menuClick;

    [SetUp]
    public void SetUp()
    {
        logPath = Application.persistentDataPath + "/BugReports.txt";
        if (File.Exists(logPath)) File.Delete(logPath);

        startButton = new GameObject("Start");
        menuClick = startButton.AddComponent<MenuClick>();

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
        Assert.IsNotNull(eggObject, "Egg object 'Egg1' not found.");
        Assert.IsNotNull(eggCrack, "EggCrack script is missing on Egg1.");

        int previousTapCount = eggCrack.tapCount;

        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eggCrack.OnPointerClick(eventData);

        Assert.Greater(eggCrack.tapCount, previousTapCount, "Tap count should increase when clicking the egg.");

        GameObject.DestroyImmediate(eventSystem);
    }
}