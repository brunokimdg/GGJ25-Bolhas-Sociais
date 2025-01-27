using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Refer�ncias")]
    public Transform player; // O transform do jogador que a c�mera deve seguir

    [Header("Configura��es de Movimenta��o")]
    public Vector3 offset = new Vector3(0, 1, -10); // Deslocamento da c�mera em rela��o ao jogador
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o do movimento da c�mera

    [Header("Limites da C�mera")]
    public bool useBounds = false; // Ativar/desativar limites
    public Vector2 minBounds; // Limites m�nimos (x, y) da c�mera
    public Vector2 maxBounds; // Limites m�ximos (x, y) da c�mera

    void LateUpdate()
    {
        if (player == null) return;

        // Calcula a posi��o desejada da c�mera
        Vector3 desiredPosition = player.position + offset;

        // Aplica suaviza��o ao movimento da c�mera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica os limites se ativados
        if (useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        }

        // Atualiza a posi��o da c�mera
        transform.position = smoothedPosition;
    }
}