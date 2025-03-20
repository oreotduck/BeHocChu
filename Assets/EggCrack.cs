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
    public GameObject RightEgg;

    public void Start()
    {
        eggAnimator = GetComponent<Animator>();
        Egg = GetComponent<BoxCollider2D>();
        tapCount = 0;
        EnableRigthEggClick(false, RightEgg);
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
            Debug.Log("Show letter");
            ShowLetter();
            EnableRigthEggClick(isCracked, RightEgg);
        }
    }

    private void ShowLetter()
    {
        letterImage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void EnableRigthEggClick(bool isCracked, GameObject obj)
    {
        if (obj.TryGetComponent<CanvasGroup>(out CanvasGroup cg)) cg.blocksRaycasts = isCracked;
        if (obj.TryGetComponent<Collider2D>(out Collider2D col2D)) col2D.enabled = isCracked;

        foreach (var handler in obj.GetComponents<MonoBehaviour>())
        {
            if (handler is IPointerClickHandler || handler is IPointerDownHandler || handler is IPointerUpHandler)
            {
                handler.enabled = isCracked;
            }
        }
    }
}
