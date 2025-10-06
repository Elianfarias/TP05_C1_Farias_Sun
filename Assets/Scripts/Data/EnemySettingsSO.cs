using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy")]
public class EnemySettingsSO : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private float speedMovement;

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
}
