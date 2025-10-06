using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace Clase08
{
    public class PlayerController : MonoBehaviour
    {
        private HealthSystem healthSystem;
        private Animator animator;
        private AudioSource audioSource;
        private AudioMixer audioMixer;

        [SerializeField] private Transform firePoint;
        [SerializeField] private PlayerDataSO data;

        private void Awake ()
        {
            healthSystem = GetComponent<HealthSystem>();
            healthSystem.onDie += HealthSystem_onDie;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
                Fire();
        }

        private void Fire ()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Bullet bullet = Instantiate(data.bulletPrefab);
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.gameObject.layer = LayerMask.NameToLayer("Player");

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bullet.transform.LookAt(targetPos);
            bullet.Set(20, 30);
        }

        private void HealthSystem_onDie()
        {
            Debug.Log("Murió el Player");
        }
    }
}