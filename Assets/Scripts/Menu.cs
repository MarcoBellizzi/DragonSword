using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script da attaccare al pannello principale del HUD.
 *
 * Contiene i metodi necessari per interagire con i menu
 */
public class Menu : MonoBehaviour
{

    // Carica la scena del videogioco
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    // ritorna al videogioco dopo aver messo pausa
    public void TornaAlGioco()
    {
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<PlayerMovement>().isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // torna al menu principale dopo aver messo pausa
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // chude l'applicazione
    public void QuitGame()
    {
        Application.Quit();
    }
}
