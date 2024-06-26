using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject[] menuButtons;
    public float videoDuration = 10.0f;
    public float playbackSpeed = 1.0f;
    public float fadeDuration = 1.0f;

    void Start()
    {
        foreach (GameObject button in menuButtons)
        {
            CanvasGroup canvasGroup = button.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            button.SetActive(false);
        }

        videoPlayer.playbackSpeed = playbackSpeed;
        videoPlayer.Play();

        StartCoroutine(WaitForVideoEnd());
    }

    IEnumerator WaitForVideoEnd()
    {
        yield return new WaitForSeconds(videoDuration / playbackSpeed);

        // Oprește videoclipul
        videoPlayer.Stop();
        foreach (GameObject button in menuButtons)
        {
            button.SetActive(true);
            StartCoroutine(FadeIn(button));
        }
    }

    IEnumerator FadeIn(GameObject button)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / fadeDuration;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * rate)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1.0f, t);
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SandBox");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
