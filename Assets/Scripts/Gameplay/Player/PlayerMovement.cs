using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int State = Animator.StringToHash("State");

        [Header("Player Settings")]
        [SerializeField] private PlayerDataSO data;
        [Header("Dash Settings")]
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;
        [Header("Sound clips")]
        [SerializeField] private AudioClip clipJump;
        [SerializeField] private AudioClip clipWalk;
        [Header("Particles")]
        [SerializeField] private ParticleSystem dashParticles;

        private Animator animator;
        private Rigidbody2D rb;
        private bool _isDashing = false;
        private float _lastDashTime = -Mathf.Infinity;
        private bool isJumping = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            RotateTowardsMouseScreen();

            if (!Input.GetKey(data.keyCodeLeft) && !Input.GetKey(data.keyCodeRight) && !isJumping)
                StopMovement();

            if (_isDashing)
                return;

            if (Input.GetKey(data.keyCodeJump) || Input.GetKey(KeyCode.Space))
                Jump();

            if (Input.GetKey(data.keyCodeDown))
                MoveY(new Vector2(rb.velocityX, -1));

            if (Input.GetKey(data.keyCodeLeft))
                MoveX(new Vector2(-1, rb.velocityY));

            if (Input.GetKey(data.keyCodeRight))
                MoveX(new Vector2(1, rb.velocityY));

            if (Input.GetKey(data.keyCodeDash))
                TryDash();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                isJumping = false;
        }

        private void Jump()
        {
            if (isJumping)
                return;

            AudioController.Instance.PlaySoundEffect(clipJump, priority: 1);
            isJumping = true;
            animator.SetInteger(State, (int)PlayerAnimatorEnum.Jump);
            rb.AddForce(data.jumpForce * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
        }

        private void StopMovement()
        {
            animator.SetInteger(State, (int)PlayerAnimatorEnum.Idle);
            rb.velocity = new Vector2(0, rb.velocityY);
        }

        private void MoveX(Vector2 axis)
        {
            if(!isJumping)
                animator.SetInteger(State, (int)PlayerAnimatorEnum.Run);

            AudioController.Instance.PlaySoundEffect(clipWalk);
            Vector2 movementSpeed = new(data.speed * Time.fixedDeltaTime * axis.x, rb.velocityY);

            if (_isDashing)
                rb.velocity = axis * data.dashSpeed;
            else
                rb.velocity = movementSpeed;
        }

        private void MoveY(Vector2 axis)
        {
            Vector2 movementSpeed = new(rb.velocityX, data.speed * Time.fixedDeltaTime * axis.y);

            if (_isDashing)
                rb.velocity = axis * data.dashSpeed;
            else
                rb.velocity = movementSpeed;
        }

        private void TryDash()
        {
            if (Time.time < _lastDashTime + dashCooldown)
                return;
            if (_isDashing)
                return;
            if (rb.velocity == Vector2.zero)
                return;

            StartCoroutine(DashRoutine(rb.velocity));
        }

        private IEnumerator DashRoutine(Vector2 velocity)
        {
            _isDashing = true;
            _lastDashTime = Time.time;

            dashParticles.Play();
            Vector2 dashDir = velocity;
            dashDir.x = dashDir.x * data.dashSpeed;
            rb.velocity = dashDir;

            yield return new WaitForSeconds(dashDuration);

            _isDashing = false;
            rb.velocity = Vector2.zero;
        }

        private void RotateTowardsMouseScreen()
        {
            Vector3 mousePos = Input.mousePosition;
            float playerScreenX = Camera.main.WorldToScreenPoint(transform.position).x;
            float sign = mousePos.x > playerScreenX ? 1f : -1f;
            transform.localScale = new Vector3(sign, 1f, 1f);
        }
    }
}