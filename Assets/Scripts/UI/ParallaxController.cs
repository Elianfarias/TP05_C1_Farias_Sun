using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [Tooltip("Velocidad global del bioma (multiplica a cada layer.speed)")]
    public float baseSpeed = 0.2f;

    private readonly List<ParallaxLayer> _layers = new();
    private float _baseDistance;

    private void Awake()
    {
        GetComponentsInChildren(true, _layers);
    }

    public void SetAlpha(float a)
    {
        foreach (var l in _layers) l.SetAlpha(a);
    }

    public void Tick(float deltaTime, float sharedBaseDistance)
    {
        _baseDistance = sharedBaseDistance;
        foreach (var l in _layers)
            l.SyncWithClock(_baseDistance * baseSpeed);
    }
}
