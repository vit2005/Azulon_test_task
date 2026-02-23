using System;
using System.Collections;
using UnityEngine;

public abstract class AnimatedScreen : MonoBehaviour, IScreen
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.3f;

    private Coroutine _fadeCoroutine;

    public void Show()
    {
        gameObject.SetActive(true);
        StartFade(1f);
    }

    public void Hide()
    {
        StartFade(0f, onComplete: () => gameObject.SetActive(false));
    }

    private void StartFade(float targetAlpha, Action onComplete = null)
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha, onComplete));
    }

    private IEnumerator FadeRoutine(float targetAlpha, Action onComplete)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = targetAlpha == 1f;
        canvasGroup.blocksRaycasts = targetAlpha == 1f;

        onComplete?.Invoke();
    }
}