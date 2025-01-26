using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    //[Header("Plataformas que devem aparecer ao coletar uma bolha")]
    //public List<GameObject> platformsToEnable;

    public void HandleBubbleCollection(string bubbleTag)
    {
        Debug.Log("Bolha coletada: " + bubbleTag);

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
        //Debug.Log("Aplicando tema sombrio");
        // Lógica para mudar o cenário para o tema sombrio
    }

    private void SetLightTheme()
    {
        //Debug.Log("Aplicando tema alegre");
        // Lógica para mudar o cenário para o tema alegre
    }

    /*
     //código original criado pelo ChatGPT 
     private void EnablePlatforms()
     {
        foreach (GameObject platform in platformsToEnable)
        {
            if (platform != null)
            {
                platform.SetActive(true);
                Debug.Log($"Plataforma {platform.name} ativada.");
            }
        }
     }
     */
    private void SpawnNextBubble(string nextBubbleTag)
    {
        // Encontra a próxima bolha na cena e a ativa
        GameObject nextBubble = GameObject.FindGameObjectWithTag(nextBubbleTag);
        if (nextBubble != null)
        {
            nextBubble.SetActive(true);
        }
    }

    private void EnablePlatforms(string nextPlatformTag)
    {
        // Encontra a próxima bolha na cena e a ativa
        GameObject nextBubble = GameObject.FindGameObjectWithTag(nextPlatformTag);
        if (nextBubble != null)
        {
            nextBubble.SetActive(true);
        }
    }

    private void TogglePlatforms(string tag, bool isActive)
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(isActive);
            //Debug.Log($"Plataforma com tag {tag} agora está {(isActive ? "ativa" : "inativa")}");
        }
    }

    private IEnumerator DisablePlatformsAfterDelay(string tag, float delay)
    {
        yield return new WaitForSeconds(delay);
        TogglePlatforms(tag, false); // Desativa as plataformas após o atraso
    }
}
