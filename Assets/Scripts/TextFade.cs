using System.Collections;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    public void FadeInAndOutText(GameObject textMessage, Color originalColor)
    {
        StartCoroutine(FadeInTextRoutine(textMessage, originalColor));
        StartCoroutine(FadeOutTextRoutine(textMessage));
    }

    private IEnumerator FadeInTextRoutine(GameObject textMessage, Color originalColor)
    {
        yield return new WaitForSecondsRealtime(0f);

        float fadeInTime = 0.1f;
        TextMeshProUGUI text = textMessage.GetComponent<TextMeshProUGUI>();
        for (float t = 0.01f; t < fadeInTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / fadeInTime));
        }
    }

    private IEnumerator FadeOutTextRoutine(GameObject textMessage)
    {
        yield return new WaitForSecondsRealtime(0.6f);

        float fadeOutTime = 100f;
        TextMeshProUGUI text = textMessage.GetComponent<TextMeshProUGUI>();
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime /2)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
        }
    }
}
