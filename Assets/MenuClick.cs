using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuClick : MonoBehaviour
{
    public GameObject resumeButton;
    public Animator transition;

    public void Start()
    {
        if (EggCrack.IsDatabaseExist())
        {
            resumeButton.gameObject.SetActive(true);
        }
        else
        {
            resumeButton.gameObject.SetActive(false);
        }
    }

    public void OnStartButton()
    {
        if(EggCrack.IsDatabaseExist())
        {

            EggCrack.ResetDatabase();
        }
        else
        {
            DatabaseManager.CreateDatabase();
        }
        SceneManager.LoadScene(1);
    }

    public void OnResumeButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
