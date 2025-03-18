using UnityEngine;

public class EggCrack : MonoBehaviour
{

    public Animator eggAnimator;
    public string letter;
    public GameObject letterImage;
    public int tapCount = 0;
    public int maxTap = 3;

    public void OnMouseDown()
    {
        tapCount++;
        if (tapCount <= maxTap)
        {
            eggAnimator.SetTrigger("Crack");
        }

        if (tapCount >= maxTap)
        {
            ShowLetter();
        }
    }

    public void ShowLetter()
    {
        letterImage.SetActive(true);
        gameObject.SetActive(false);
    }
}
