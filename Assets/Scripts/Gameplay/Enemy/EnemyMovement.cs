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
    private bool isAttacking = false;
    private bool isDie = false;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isDie)
        {
            if (toPatrolA)
                MoveToPatrol(patrolA);
            else
                MoveToPatrol(patrolB);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem)
            && collision.gameObject.layer == LayerMask.NameToLayer("Player")
            && !isDie)
            StartCoroutine(Attack(healthSystem));
    }

    public void Die()
    {
        isDie = true;
        rb.velocity = Vector2.zero;
    }

    private IEnumerator Attack(HealthSystem healthSystem)
    {
        isAttacking = true;
        healthSystem.DoDamage(enemySettings.Damage);
        animator.SetInteger(State, (int)PlayerAnimatorEnum.Attack);

        yield return new WaitForSeconds(1f);

        isAttacking = false;
        animator.SetInteger(State, (int)PlayerAnimatorEnum.Idle);
    }

    private void MoveToPatrol(GameObject patrol)
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

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
