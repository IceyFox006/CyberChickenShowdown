using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Rematch()
    {
        SceneManager.LoadScene("GameScreen");
    }
}
