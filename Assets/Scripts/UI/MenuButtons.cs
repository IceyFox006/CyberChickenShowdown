using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
    }
    public void LoadGameScreen()
    {
        SceneManager.LoadScene("GameScreen");
    }
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
