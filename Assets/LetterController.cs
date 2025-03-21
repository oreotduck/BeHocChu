using UnityEngine;
using UnityEngine.EventSystems;

public class NewMonoBehaviourScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject upperCaseLetter;
    public GameObject lowerCaseLetter;
    public GameObject wordImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject == upperCaseLetter)
        {
            lowerCaseLetter.SetActive(true);
            upperCaseLetter.SetActive(false);
        }
        else if (gameObject == lowerCaseLetter)
        {
            wordImage.SetActive(true);
            lowerCaseLetter.SetActive(false);
        }
        else if (gameObject == wordImage)
        {
            upperCaseLetter.SetActive(true);
            wordImage.SetActive(false);
        }
    }


}
