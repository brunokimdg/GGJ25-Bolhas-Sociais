using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform player; // O jogador
    public float parallaxFactor; // Fator de paralaxe para cada layer

    private Vector3 previousPlayerPosition;

    void Start()
    {
        // Armazena a posi��o inicial do jogador
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        // Calcula o deslocamento do jogador
        Vector3 deltaPosition = player.position - previousPlayerPosition;

        // Move a layer na propor��o do fator de paralaxe
        transform.position += new Vector3(deltaPosition.x * parallaxFactor, deltaPosition.y * parallaxFactor, 0);

        // Atualiza a posi��o anterior
        previousPlayerPosition = player.position;
    }
}
