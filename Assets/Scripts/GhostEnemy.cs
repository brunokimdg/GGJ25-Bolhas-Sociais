using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
  
    [Header("Movement Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;

    [Header("Damage Settings")]
    public int damage = 10;

    [Header("Ground Detection")]
    [SerializeField]
    private Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    [Header("Player Reference")]
    [SerializeField]
    public Transform player;

    private Rigidbody2D rb;
    private bool isChasing = false;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Determine if the ghost should chase the player
        isChasing = distanceToPlayer <= detectionRange;

        // Handle movement
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * chaseSpeed, rb.linearVelocity.y);

        // Flip the ghost if needed
        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            Flip();
        }
    }

    private void Patrol()
    {
        // Move in the current facing direction
        float patrolDirection = facingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(patrolDirection * patrolSpeed, rb.linearVelocity.y);

        // Check for ground ahead
        if (!IsGroundAhead())
        {
            Flip();
        }
    }

    private bool IsGroundAhead()
    {
        // Check if there is ground in front of the ghost
        Vector2 checkPosition = groundCheck.position;
        return Physics2D.OverlapCircle(checkPosition, groundCheckRadius, groundLayer);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the ghost touches the player
        if (collision.CompareTag("Player"))
        {
            /*  PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }*/
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the ground check area
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
