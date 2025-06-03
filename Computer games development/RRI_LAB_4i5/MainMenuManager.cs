using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start Game clicked");
        // Zamijenit cemo ovo sa stvarnim ucitavanjem scene
        SceneManager.LoadScene("LoadingScene");
    }

    public void OpenOptions()
    {
        Debug.Log("Options clicked");
        // Ovdje mozes otvoriti podmeni ili panel
    }

    public void QuitGame()
    {
        Debug.Log("Quit clicked");
        Application.Quit();
    }
}
