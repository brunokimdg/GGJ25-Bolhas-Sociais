using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Para TextMeshPro (se não usar TMP, remova esta linha)

public class BubbleController : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Referência ao Text no Canvas da bolha
    [SerializeField]
    private GameObject LightBackground;
    [SerializeField]
    private GameObject DarkBackground;

    void Start()
    {
        SetLightTheme();
        EnablePlatforms("GrayPlatform");
        SpawnNextBubble("DarkBubble");
        TogglePlatforms("DarkPlatform");
    }

    public void HandleBubbleCollection(string bubbleTag)
    {       
        StartCountdown();
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(bubbleTag);
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                CircleCollider2D circleCollider = platform.GetComponent<CircleCollider2D>();
                if (circleCollider != null)
                {
                    circleCollider.enabled = false;
                }
            }
        }
        if (bubbleTag == "DarkBubble")
        {
            SetDarkTheme();
            EnablePlatforms("DarkPlatform");
            SpawnNextBubble("GrayBubble");
            StartCoroutine(DisablePlatformsAfterDelay("GrayPlatform", 3f)); // Desativa as plataformas claras após 3 segundos
        }
        else if (bubbleTag == "GrayBubble")
        {
            SetLightTheme();
            SpawnNextBubble("DarkBubble");
            EnablePlatforms("GrayPlatform");
            StartCoroutine(DisablePlatformsAfterDelay("DarkPlatform", 3f)); // Desativa as plataformas sombrias após 3 segundos
        }
       
    }

    private void SetDarkTheme()
    {
        LightBackground.SetActive(false);
        DarkBackground.SetActive(true);
    }

    private void SetLightTheme()
    {
        LightBackground.SetActive(true);
        DarkBackground.SetActive(false);
    }

    private void SpawnNextBubble(string nextBubbleTag)
    {
        // Encontra a próxima bolha na cena e a ativa
        GameObject[] nextBubbles = GameObject.FindGameObjectsWithTag(nextBubbleTag);
        foreach (GameObject nextBubble in nextBubbles)
        {
            if (nextBubble != null)
            {
                // Desabilita o componente BoxCollider2D se ele existir
                CircleCollider2D circleCollider = nextBubble.GetComponent<CircleCollider2D>();
                if (circleCollider != null)
                {
                    circleCollider.enabled = true;
                }

                // Desabilita o componente SpriteRenderer se ele existir
                SpriteRenderer circleSprite = nextBubble.GetComponentInChildren<SpriteRenderer>();

                if (circleSprite != null)
                {
                    // Ativa o SpriteRenderer
                    circleSprite.enabled = true;
                    Debug.Log("Sprite habilitada!");
                }

                string previousBubbleTag = null;

                if (nextBubbleTag == "DarkBubble")
                {
                    previousBubbleTag = "GrayBubble";
                }
                else
                {
                    previousBubbleTag = "DarkBubble";
                }

                GameObject[] previousBubbles = GameObject.FindGameObjectsWithTag(previousBubbleTag);
                foreach (GameObject bubble in previousBubbles)
                {
                    //Debug.Log("Sprite in children");
                    // Desabilita o componente SpriteRenderer se ele existir
                    SpriteRenderer previousCircleSprite = bubble.GetComponentInChildren<SpriteRenderer>();

                    if (previousCircleSprite != null)
                    {
                        // Ativa o SpriteRenderer
                        previousCircleSprite.enabled = false;
                        //Debug.Log("Sprite habilitada!");
                    }
                }
            }
        }
    }

    private void EnablePlatforms(string nextPlatformTag)
    {
        GameObject[] nextPlatforms = GameObject.FindGameObjectsWithTag(nextPlatformTag);
        foreach (GameObject nextPlatform in nextPlatforms)
        { 
            if (nextPlatform != null)
            {
                BoxCollider2D boxCollider = nextPlatform.GetComponent<BoxCollider2D>();
                    boxCollider.enabled = true;


                 

            SpriteRenderer spriteRenderer = nextPlatform.GetComponent<SpriteRenderer>();
                    spriteRenderer.enabled = true;
           }
        }
    }

    private void TogglePlatforms(string tag)
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                SpriteRenderer spriteRenderer = platform.GetComponent<SpriteRenderer>();
                //if (spriteRenderer != null)
                //{
                    spriteRenderer.enabled = false;
                //}

                BoxCollider2D boxCollider = platform.GetComponent<BoxCollider2D>();
                //if (boxCollider != null)
                // {
                boxCollider.enabled = false;

                // Destroy(platform);
            }
            
            //platform.SetActive(isActive);
            //Debug.Log($"Plataforma com tag {tag} agora está {(isActive ? "ativa" : "inativa")}");
        }
    }

    private IEnumerator DisablePlatformsAfterDelay(string tag, float delay)
    {
       
        yield return new WaitForSeconds(delay);
        TogglePlatforms(tag); // Desativa as plataformas após o atraso
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        // Certifique-se de que o texto está ativo
        //if (countdownText == null) yield break;
       // countdownText.gameObject.SetActive(true);

        // Realiza a contagem regressiva
        for (int i = 3; i > 0; i--)
        {
            countdownText.SetText(i.ToString());
  //          countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f); // Espera 1 segundo
        }
        countdownText.SetText("");
        // Oculta o texto após a contagem regressiva
        //countdownText.gameObject.SetActive(false);
    }
}
