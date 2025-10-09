using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onDie += HealthSystem_onDie;
        healthSystem.onLifeUpdated += HealthSystem_onLifeUpdated;
    }

    private void OnDestroy()
    {
        healthSystem.onDie -= HealthSystem_onDie;
        healthSystem.onLifeUpdated -= HealthSystem_onLifeUpdated;
    }

    private void HealthSystem_onLifeUpdated(int arg1, int arg2)
    {

    }

    private void HealthSystem_onDie()
    {
        GameStateManager.Instance.SetGameState(GameState.GAME_OVER);
    }
}