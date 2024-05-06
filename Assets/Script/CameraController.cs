using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject.transform;
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng Player!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    } 
}
