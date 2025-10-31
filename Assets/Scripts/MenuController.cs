using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [Header("Transición de Fade (opcional)")]
    public Image fadeImage;          // Asigna una Image negra que cubra toda la pantalla
    public float fadeDuration = 0.6f; // Duración del fade en segundos

    void Start()
    {
        // Si tienes una imagen negra inicial, la desvanecemos al iniciar el menú
        if (fadeImage != null)
            StartCoroutine(FadeIn());
    }

    // Llamado al presionar el botón "Jugar"
    public void PlayGame(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    // Carga la escena con efecto de desvanecimiento
    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeOut());
        }

        SceneManager.LoadScene(sceneName);
    }

    // Efecto de aparición del menú (negro -> visible)
    private IEnumerator FadeIn()
    {
        float t = fadeDuration;
        Color c = fadeImage.color;
        c.a = 1;
        fadeImage.color = c;

        while (t > 0)
        {
            t -= Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0;
        fadeImage.color = c;
    }

    // Efecto de desaparición (visible -> negro)
    private IEnumerator FadeOut()
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1;
        fadeImage.color = c;
    }
}
