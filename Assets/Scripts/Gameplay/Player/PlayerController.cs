using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HealthSystem healthSystem;
    [Header("Sound clips")]
    [SerializeField] private AudioClip clipHurt;
    [SerializeField] private AudioClip clipDie;

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

    private void HealthSystem_onLifeUpdated(int life, int maxLife)
    {
        if(life < maxLife)
            AudioController.Instance.PlaySoundEffect(clipHurt, priority: true);
    }

    private void HealthSystem_onDie()
    {
        AudioController.Instance.PlaySoundEffect(clipDie, priority: true);
        GameStateManager.Instance.SetGameState(GameState.GAME_OVER);
    }
}