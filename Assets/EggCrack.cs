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

    public void Start()
    {
        eggAnimator = GetComponent<Animator>();
        Egg = GetComponent<BoxCollider2D>();
        tapCount = 0;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCracked) return;

        Debug.Log("Egg Click!");
        tapCount++;
        eggAnimator.SetInteger("TapCount",tapCount);

        if (tapCount >= maxTap)
        {
            isCracked=true;
            ShowLetter();
        }
    }

    private void ShowLetter()
    {
        letterImage.SetActive(true);
        gameObject.SetActive(false);
    }
}
