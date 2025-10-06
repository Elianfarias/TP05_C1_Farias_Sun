using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO data;
        [Header("Dash Settings")]
        [SerializeField] private float dashSpeed = 10f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;

        private Rigidbody2D rb;
        private bool _isDashing = false;
        private float _lastDashTime = -Mathf.Infinity;
        private Vector2 moveInput = Vector2.zero;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            RotateTowardsMouseScreen();

            if (_isDashing)
                return;

            if (Input.GetKey(data.keyCodeLeft))
                Move(Vector2.left);

            if (Input.GetKey(data.keyCodeRight))
                Move(Vector2.right);

            if (!Input.GetKey(data.keyCodeLeft) && !Input.GetKey(data.keyCodeRight))
                StopMovement();
        }

        void StopMovement()
        {
            rb.velocity = new Vector2(0, 0);
        }

        void Move(Vector2 axis)
        {
            if (_isDashing)
                rb.velocity = axis * dashSpeed;
            else
                rb.velocity = axis;
        }

        void TryDash()
        {
            if (Time.time < _lastDashTime + dashCooldown)
                return;
            if (_isDashing)
                return;
            if (rb.velocity == Vector2.zero)
                return;

            StartCoroutine(DashRoutine(rb.velocity));
        }

        IEnumerator DashRoutine(Vector2 velocity)
        {
            _isDashing = true;
            _lastDashTime = Time.time;

            Vector2 dashDir = velocity;

            rb.velocity = dashDir * dashSpeed;

            yield return new WaitForSeconds(dashDuration);

            _isDashing = false;
            rb.velocity = Vector2.zero;
        }

        void RotateTowardsMouseScreen()
        {
            Vector3 mousePos = Input.mousePosition;;
            float playerScreenX = Camera.main.WorldToScreenPoint(transform.position).x;
            float sign = mousePos.x > playerScreenX ? 0.4f : -0.4f;
            transform.localScale = new Vector3(sign, 0.4f, 1f);
        }
    }
}