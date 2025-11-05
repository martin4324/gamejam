using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    void Start()
    {

    }

    // Llamado al presionar el botón "Jugar"
    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Llamado al presionar el botón "Salir"
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
