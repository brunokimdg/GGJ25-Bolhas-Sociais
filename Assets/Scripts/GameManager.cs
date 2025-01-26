using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform playerStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = playerStart.position;
    }
}
