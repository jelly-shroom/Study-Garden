using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance { get; private set; }
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private Vector3 popUpPosition = new Vector3(0, 0, 0);
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 2f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps it alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures there is only one instance
        }
    }

    public void ShowPopup(string message)
    {
        GameObject instantiatedPopup = Instantiate(popUpPrefab, popUpPosition, Quaternion.identity);

        TextMeshProUGUI textComponent = instantiatedPopup.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = message;

        StartCoroutine(FadePopup(instantiatedPopup));
    }

    private IEnumerator FadePopup(GameObject popup)
    {
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("Popup prefab is missing a CanvasGroup component.");
            yield break;
        }

        // Fade
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f));
        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f));

        // Destroy the popup after fading out
        Destroy(popup);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null; // Wait until the next frame
        }

        // Ensure the final alpha is set exactly
        canvasGroup.alpha = endAlpha;
    }
}
