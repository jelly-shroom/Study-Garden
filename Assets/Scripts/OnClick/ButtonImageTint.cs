using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonImageTint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private Image itemImage;
    public Color pressedColor = Color.gray;
    private Color originalColor;
    public float fadeDuration = 0.1f;

    private Coroutine colorChangeCoroutine;

    void Start()
    {
        button = GetComponent<Button>();

        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img != button.image) // Make sure it's not the button's own Image component
            {
                itemImage = img;
                break;
            }
        }

        originalColor = itemImage.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Stop any previous coroutine to prevent overlapping transitions
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }

        colorChangeCoroutine = StartCoroutine(FadeToColor(pressedColor));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Stop any previous coroutine to prevent overlapping transitions
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }

        colorChangeCoroutine = StartCoroutine(FadeToColor(originalColor));
    }

    private void OnDisable()
    {
        // Ensure that the color resets if the button is disabled
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }

        itemImage.color = originalColor;
    }

    IEnumerator FadeToColor(Color targetColor)
    {
        float elapsedTime = 0f;
        Color startingColor = itemImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            itemImage.color = Color.Lerp(startingColor, targetColor, elapsedTime / fadeDuration);
            yield return null; // Wait until the next frame
        }

        // Ensure the final color is exactly the target color
        itemImage.color = targetColor;
    }
}
