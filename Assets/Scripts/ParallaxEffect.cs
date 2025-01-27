using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform[] layers; // Array de camadas (camadas de fundo)
    public float parallaxFactor = 0.5f; // Fator de paralaxe, ajust�vel no Inspector
    public float smoothing = 1f; // Suaviza��o do movimento, ajust�vel no Inspector

    private Vector3 previousCameraPosition; // Posi��o da c�mera anterior

    void Start()
    {
        // Inicializa a posi��o da c�mera anterior
        previousCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        // Calcula o deslocamento da c�mera
        float deltaX = Camera.main.transform.position.x - previousCameraPosition.x;

        // Atualiza a posi��o de cada camada
        for (int i = 0; i < layers.Length; i++)
        {
            // Calcula o movimento da camada baseado no fator de paralaxe
            float parallax = deltaX * (i * parallaxFactor + 1); // Ajuste do movimento conforme a camada

            // Suaviza a posi��o da camada
            Vector3 targetPosition = new Vector3(layers[i].position.x + parallax, layers[i].position.y, layers[i].position.z);
            layers[i].position = Vector3.Lerp(layers[i].position, targetPosition, smoothing * Time.deltaTime);
        }

        // Atualiza a posi��o da c�mera anterior para a atual
        previousCameraPosition = Camera.main.transform.position;
    }
}
