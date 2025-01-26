using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;

    private void Update()
    {
        Vector2 direction = player.position - transform.position;

        if (Vector2.Dot(transform.right, direction) > 0) // O inimigo está olhando para o jogador.
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
