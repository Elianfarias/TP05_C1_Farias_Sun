using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private static readonly int State = Animator.StringToHash("State");

    [Header("Enemy Settings")]
    [SerializeField] EnemySettingsSO enemySettings;
    [Header("Patrol Positions")]
    [SerializeField] private GameObject patrolA;
    [SerializeField] private GameObject patrolB;

    private bool toPatrolA = true;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (toPatrolA)
            MoveToPatrol(patrolA);
        else
            MoveToPatrol(patrolB);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem) && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(nameof(Attack));
            healthSystem.DoDamage(enemySettings.Damage);
        }
    }

    private IEnumerator Attack()
    {
        animator.SetInteger(State, (int)PlayerAnimatorEnum.Attack);
        yield return new WaitForSeconds(1f);
        animator.SetInteger(State, (int)PlayerAnimatorEnum.Idle);
    }

    private void MoveToPatrol(GameObject patrol)
    {
        Vector2 direction = (patrol.transform.position - transform.position).normalized;
        rb.velocity = (enemySettings.SpeedMovement) * Time.fixedDeltaTime * direction;
        float distance = Vector2.Distance(transform.position, patrol.transform.position);

        //Rotation
        float sign = patrol.transform.position.x > transform.position.x ? 0 : 180;
        transform.rotation = new Quaternion(0, sign, 0, 0);

        if (distance < 0.1f)
        {
            rb.velocity = Vector2.zero;
            toPatrolA = !toPatrolA;
        }
    }
}
