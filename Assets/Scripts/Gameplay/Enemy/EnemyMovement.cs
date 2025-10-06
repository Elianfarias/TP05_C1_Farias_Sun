using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] EnemySettingsSO enemySettings;

    public float speedIncremental = 1f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = (enemySettings.SpeedMovement * speedIncremental) * Time.fixedDeltaTime * Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitMap"))
            gameObject.SetActive(false);
    }
}
