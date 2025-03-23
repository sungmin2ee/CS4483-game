/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public Image cutsceneImage; // UI Image to display the cutscenes
    public Sprite[] cutsceneSprites; // Array of cutscene images
    private string[] cutsceneNarratives = {
        "Once upon a time, there was a peaceful village named Lania.",
        "I lived happily with the villagers here.",
        "One day, an ominous change appeared in the sky.",
        "Demons crawled out from every corner.",
        "Lania suffered a devastating blow.",
        "Now, I set out on my journey of revenge!"
        }; // Array of narratives (text for each scene)
    public TextMeshProUGUI narrativeText; // UI Text for narrative
    public CanvasGroup canvasGroup; 
    private int currentIndex = 0;
    public float fadeDuration = 0.5f; // duration of fade effect

    void Start()
    {
        if (cutsceneSprites.Length > 0)
        {
            cutsceneImage.sprite = cutsceneSprites[currentIndex];
            narrativeText.text = cutsceneNarratives[currentIndex]; 
            canvasGroup.alpha = 1; 
            
        }
        else
        {
            Debug.LogError("CutsceneSprites array is empty! Assign sprites in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click for the next image
        {
            NextCutscene();
        }
    }

    void NextCutscene()
    {
        if (cutsceneSprites.Length == 0) return;

        currentIndex++;

        if (currentIndex < cutsceneSprites.Length)
        {
            StartCoroutine(FadeToNextCutscene());
        }
        else
        {
            StartCoroutine(FadeOutAndEnd());
        }
    }

    IEnumerator FadeToNextCutscene()
    {
        yield return StartCoroutine(FadeOut());
        cutsceneImage.sprite = cutsceneSprites[currentIndex];
        narrativeText.text = cutsceneNarratives[currentIndex]; 
        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FadeOutAndEnd()
    {
        yield return StartCoroutine(FadeOut());
        EndCutscene();
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene ended. Load next scene or transition here.");
        SceneManager.LoadScene("StartingPage");
    }
}
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    [Header("UI References")]
    public Image cutsceneImage; // UI Image to display the cutscenes
    public TextMeshProUGUI narrativeText; // UI Text for narrative
    public CanvasGroup canvasGroup; // For fade effect

    [Header("Cutscene Content")]
    public Sprite[] cutsceneSprites; // Array of cutscene images
    [TextArea(2, 4)]
    public string[] cutsceneNarratives; // Array of narratives (text for each scene)

    [Header("Cutscene Settings")]
    public string nextSceneName = "StartingPage"; // Scene to load after cutscene ends
    public float fadeDuration = 0.5f; // Duration of fade effect

    private int currentIndex = 0;

    void Start()
    {
        if (cutsceneSprites.Length == 0 || cutsceneNarratives.Length == 0)
        {
            Debug.LogError("Cutscene content missing!");
            return;
        }

        currentIndex = 0;

        // Display first image and text
        cutsceneImage.sprite = cutsceneSprites[currentIndex];
        narrativeText.text = GetNarrativeSafe(currentIndex);
        canvasGroup.alpha = 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click to continue
        {
            NextCutscene();
        }
    }

    void NextCutscene()
    {
        if (cutsceneSprites.Length == 0) return;

        currentIndex++;

        if (currentIndex < cutsceneSprites.Length)
        {
            StartCoroutine(FadeToNextCutscene());
        }
        else
        {
            StartCoroutine(FadeOutAndEnd());
        }
    }

    IEnumerator FadeToNextCutscene()
    {
        yield return StartCoroutine(FadeOut());
        cutsceneImage.sprite = cutsceneSprites[currentIndex];

        narrativeText.text = GetNarrativeSafe(currentIndex);

        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FadeOutAndEnd()
    {
        yield return StartCoroutine(FadeOut());
        EndCutscene();
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene ended. Loading scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    string GetNarrativeSafe(int index)
    {
        if (index >= 0 && index < cutsceneNarratives.Length)
            return cutsceneNarratives[index];
        else
            return cutsceneNarratives[cutsceneNarratives.Length - 1];

    }
}
