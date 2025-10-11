using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject soul;
    [SerializeField] private EnemySettingsSO data;
    [SerializeField] private AudioClip clipMovement;
    [SerializeField] private AudioClip clipHurt;
    [SerializeField] private AudioSource soundEffectAudioSource;

    private HealthSystem healthSystem;
    private EnemyMovement enemyMovement;
    private float nextTimeToReproduce;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.onMove += EnemyMovement_onMove;
        healthSystem.onDie += HealthSystem_onDie;
        healthSystem.onLifeUpdated += HealthSystem_onLifeUpdated;

        nextTimeToReproduce = Time.time;
    }

    private void OnDestroy()
    {
        enemyMovement.onMove -= EnemyMovement_onMove;
        healthSystem.onDie -= HealthSystem_onDie;
        healthSystem.onLifeUpdated -= HealthSystem_onLifeUpdated;
    }

    private void EnemyMovement_onMove()
    {
        if (nextTimeToReproduce < Time.time)
        {
            nextTimeToReproduce = Time.time + data.TimeMoveSound;
            PlaySoundEffect(clipMovement);
        }
    }

    private void HealthSystem_onDie()
    {
        StartCoroutine(nameof(Die));
    }

    private void HealthSystem_onLifeUpdated(int life, int maxLife)
    {
        StartCoroutine(TakeDamage(life, maxLife));
    }

    private IEnumerator Die()
    {

        enemyMovement.Die();

        yield return new WaitForSeconds(data.TimeStun);

        soul.transform.position = transform.position;
        soul.SetActive(true);
        gameObject.SetActive(false);
    }

    private IEnumerator TakeDamage(int life, int maxLife)
    {
        if (life < maxLife)
        {
            PlaySoundEffect(clipHurt, priority: true);
            enemyMovement.StopMovement();

            yield return new WaitForSeconds(data.TimeStun);

            enemyMovement.ResumeMovement();
        }
    }

    private void PlaySoundEffect(AudioClip audioClip, bool priority = false)
    {
        if (soundEffectAudioSource.isPlaying && !priority)
            return;

        soundEffectAudioSource.clip = audioClip;
        soundEffectAudioSource.Play();
    }
}
