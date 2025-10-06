using UnityEngine;

namespace Clase08
{
    public class EnemyController : MonoBehaviour
    {
        private HealthSystem healthSystem;

        private void Awake ()
        {
            healthSystem = GetComponent<HealthSystem>();
            healthSystem.onDie += HealthSystem_onDie;
        }

        private void HealthSystem_onDie()
        {
            Destroy(gameObject);
        }
    }
}