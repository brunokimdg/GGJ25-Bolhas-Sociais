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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Bolha coletada com tag: " + collision.tag);

        // Detectar colisão com bolhas
        if (collision.CompareTag("DarkBubble") || collision.CompareTag("GrayBubble"))
        {
            bubbleController.HandleBubbleCollection(collision.tag);
            Destroy(collision.gameObject); // Remove a bolha coletada
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
