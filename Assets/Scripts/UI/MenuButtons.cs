using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ChangeCharacter()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
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
