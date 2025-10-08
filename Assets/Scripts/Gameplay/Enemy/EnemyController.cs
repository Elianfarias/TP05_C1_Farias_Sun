using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private HealthSystem healthSystem;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyMovement = GetComponent<EnemyMovement>();
        healthSystem.onDie += HealthSystem_onDie;
    }

    private void HealthSystem_onDie()
    {
        StartCoroutine(nameof(Die));
    }

    private IEnumerator Die()
    {
        enemyMovement.Die();

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}
