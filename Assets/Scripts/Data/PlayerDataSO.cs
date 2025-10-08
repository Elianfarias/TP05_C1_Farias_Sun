using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerDataSO : ScriptableObject
{
    public KeyCode keyCodeJump = KeyCode.Space;
    public KeyCode keyCodeLeft = KeyCode.A;
    public KeyCode keyCodeRight = KeyCode.D;
    public KeyCode keyCodeDash = KeyCode.LeftShift;
    public Bullet bulletPrefab;
    public int speed;
    public int jumpForce;
    public string playerName;
    public float volumeMusic;
    public float volumeSFX;
    public int chargerSize = 4;
    public float extraReloadDelay = 2f;
}