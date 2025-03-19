using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClick : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    public void StartGame()
    {
        Debug.Log("Start Game!");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Close Game!");
        Application.Quit();
    }  
    
}
