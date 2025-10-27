using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    public void SetActive(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void SetUnactive(GameObject gameObject)
    {
        Debug.Log(";");
        gameObject.SetActive(false);
    }
    public void EnableImage(Image image)
    {
        image.enabled = true;
    }
    public void DisableImage(Image image)
    {
        image.enabled = false;
    }
}
