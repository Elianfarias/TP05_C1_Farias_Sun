using System.Collections;
using UnityEngine;

public class BiomeParallaxOrchestrator : MonoBehaviour
{
    public static BiomeParallaxOrchestrator Instance { get; private set; }
    [Header("Biomas")]
    [SerializeField] private ParallaxController biomeA;
    [SerializeField] private ParallaxController biomeB;
    [SerializeField] private ParallaxController biomeC;

    [Header("Reloj global")]
    public float worldSpeed = 1f;
    private float _sharedBaseDistance;

    [Header("Fade")]
    [SerializeField, Min(0.05f)] private float fadeDuration = 2f;
    [SerializeField] private AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool _fading;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        biomeA.SetAlpha(1f);
        biomeB.SetAlpha(0f);
        biomeC.SetAlpha(0f);
    }

    private void Update()
    {
        _sharedBaseDistance += worldSpeed * Time.deltaTime;

        biomeA.Tick(Time.deltaTime, _sharedBaseDistance);
        biomeB.Tick(Time.deltaTime, _sharedBaseDistance);
        biomeC.Tick(Time.deltaTime, _sharedBaseDistance);
    }

    public void SwitchToBiomeB()
    {
        if (!_fading) StartCoroutine(Fade(biomeA, biomeB));
    }

    public void SwitchToBiomeA()
    {
        if (!_fading) StartCoroutine(Fade(biomeB, biomeA));
    }

    public void SwitchToBiomeC()
    {
        if (!_fading) StartCoroutine(Fade(biomeB, biomeC));
    }

    private IEnumerator Fade(ParallaxController from, ParallaxController to)
    {
        _fading = true;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float k = ease.Evaluate(Mathf.Clamp01(t / fadeDuration));
            from.SetAlpha(1f - k);
            to.SetAlpha(k);
            yield return null;
        }
        from.SetAlpha(0f);
        to.SetAlpha(1f);
        _fading = false;
    }
}