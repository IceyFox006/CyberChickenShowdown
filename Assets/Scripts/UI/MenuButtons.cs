using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ChangeCharacter()
    {
        Debug.Log("Not implemented yet.");
        //Go to choose fighter scene
    }
    public void Rematch()
    {
        SceneManager.LoadScene("GameScreen");
    }
    public void MainMenu()
    {
        Debug.Log("Not implemented yet.");
        //Go to title scene
    }
}
