using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<int, int> onLifeUpdated;
    public event Action onDie;

    private int life = 100;
    [SerializeField] private int maxLife = 100;

    private void Start ()
    {
        life = maxLife;
        onLifeUpdated?.Invoke(life, maxLife);
    }

    public void DoDamage (int damage)
    {
        if (damage < 0)
        {
            Debug.Log("Se cura en la funcion de daño");
            return;
        }

        life -= damage;

        if (life < 0)
        {
            life = 0;
            onDie?.Invoke();
        }
        else
        {
            onLifeUpdated?.Invoke(life, maxLife);
        }

        Debug.Log("DoDamage", gameObject);
    }

    public void Heal (int plus)
    {
        if (plus < 0)
        {
            Debug.Log("Se daña en la funcion de cura");
            return;
        }

        life += plus;

        if (life > maxLife)
            life = maxLife;

        Debug.Log("Heal");
        onLifeUpdated?.Invoke(life, maxLife);
    }
}