using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy")]
public class EnemySettingsSO : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private float speedMovement;
    [SerializeField] private int damage;

    public string EnemyName { get { return enemyName; } }

    public void SetEnemyName(string enemyName)
    {
        this.enemyName = enemyName;
    }

    public float SpeedMovement { get { return speedMovement; } }

    public void SetSpeedMovement (float speedMovement)
    {
        this.speedMovement = speedMovement;
    }

    public int Damage { get { return damage; } }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
