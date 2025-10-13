using UnityEngine;

public class ParallaxFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void LateUpdate()
    {
        Vector3 posFollow = new(player.transform.position.x, transform.position.y);
        transform.position = posFollow;
    }
}
