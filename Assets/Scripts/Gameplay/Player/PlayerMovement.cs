using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int State = Animator.StringToHash("State");

        [Header("Player Settings")]
        [SerializeField] private PlayerDataSO data;
        [Header("Dash Settings")]
        [SerializeField] private float dashSpeed = 10f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;


        private PlayerAnimatorEnum playerAnimatorEnum;
        private Animator animator;
        private Rigidbody2D rb;
        private bool _isDashing = false;
        private float _lastDashTime = -Mathf.Infinity;
        private Vector2 moveInput = Vector2.zero;
        private bool isJumping = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            RotateTowardsMouseScreen();

            if (_isDashing)
                return;
            
            if (Input.GetKey(data.keyCodeJump))
                Jump();

            if (Input.GetKey(data.keyCodeLeft))
                Move(new Vector2(-1, rb.velocityY));


            if (Input.GetKey(data.keyCodeRight))
                Move(new Vector2(1, rb.velocityY));

            if (Input.GetKey(data.keyCodeDash))
                TryDash();

            if (!Input.GetKey(data.keyCodeLeft) && !Input.GetKey(data.keyCodeRight) && !isJumping)
                StopMovement();
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

            isJumping = true;
            animator.SetInteger(State, (int)PlayerAnimatorEnum.Jump);
            rb.AddForce(data.jumpForce * Time.fixedDeltaTime * Vector2.up, ForceMode2D.Impulse);
        }

        private void StopMovement()
        {
            animator.SetInteger(State, (int)PlayerAnimatorEnum.Idle);
            rb.velocity = new Vector2(0, rb.velocityY);
        }

        private void Move(Vector2 axis)
        {
            animator.SetInteger(State, (int)PlayerAnimatorEnum.Run);

            if (_isDashing)
                rb.velocity = axis * dashSpeed;
            else
                rb.velocity = axis;
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

            Vector2 dashDir = velocity;
            dashDir.x = dashDir.x * dashSpeed;
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