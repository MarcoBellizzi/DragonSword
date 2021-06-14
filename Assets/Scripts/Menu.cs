using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    

    public void TornaAlGioco()
    {
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<PlayerMovement>().isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
