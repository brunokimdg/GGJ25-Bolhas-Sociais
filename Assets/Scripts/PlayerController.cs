using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f; // Velocidade de movimento
    public float jumpForce = 10f; // Força do pulo

    [Header("Detecção de Solo")]
    public Transform groundCheck; // Ponto para verificar se está no chão
    public float groundCheckRadius = 0.2f; // Raio para detectar o chão
    public LayerMask groundLayer; // Camada que representa o chão

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Referências")]
    public BubbleController bubbleController;

    [SerializeField]
    private GameObject countdownCanvas;

    public Vector3 respawnPoint; // Posição atual de respawn
    public GameObject killZone; // Kill Zone para referência no Inspector

    private string respawnMode;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Define o ponto de respawn inicial como a posição atual do jogador
        respawnPoint = transform.position;
    }

    void Update()
    {
        // Movimentação horizontal
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Virar o jogador na direção do movimento
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Verificar se está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Pular
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bolha coletada com tag: " + collision.tag);

        // Detectar colisão com bolhas
        if (collision.CompareTag("DarkBubble") || collision.CompareTag("GrayBubble"))
        {
            Vector3 targetPosition = collision.transform.position;

            // Soma o vetor adicional, garantindo que Y seja +3
            Vector3 newPosition = new Vector3(
                targetPosition.x + 0,
                targetPosition.y + 3,
                targetPosition.z + 0
            );

            countdownCanvas.transform.position = newPosition;//collision.new Vector3(x, y, z);

            bubbleController.HandleBubbleCollection(collision.tag);
            //Destroy(collision.gameObject); // Remove a bolha coletada
        }

        // Verifica se o jogador toca em um checkpoint
        if (collision.CompareTag("DarkCheckpoint") || collision.CompareTag("LightCheckpoint"))
        {
            // Atualiza o ponto de respawn para o checkpoint
            respawnPoint = collision.transform.position;

            if (collision.CompareTag("DarkCheckpoint"))
            {
                respawnMode = "DarkBubble";
            }
            else if (collision.CompareTag("LightCheckpoint"))
            {
                respawnMode = "GrayBubble";
            }
        }

        // Verifica se o jogador toca em uma Kill Zone
        if (collision.CompareTag("KillZone"))
        {
            // Move o jogador para o ponto de respawn
            bubbleController.HandleBubbleCollection(respawnMode);
            transform.position = respawnPoint;
            Debug.Log("Jogador morreu! Voltando ao último checkpoint.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenhar a área de detecção do chão para debug
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
