using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject camadaSombria; // Pai do cen�rio sombrio
    public GameObject camadaAlegre; // Pai do cen�rio alegre

    void Start()
    {
        // Inicializa com um cen�rio vis�vel
        SetScenario(camadaSombria, false);
        SetScenario(camadaAlegre, true);
    }

    public void ChangeScenario(string scenarioName)
    {
        switch (scenarioName)
        {
            case "Sombrio":
                SetScenario(camadaSombria, true);
                SetScenario(camadaAlegre, false);
                break;

            case "Alegre":
                SetScenario(camadaSombria, false);
                SetScenario(camadaAlegre, true);
                break;
        }
    }

    private void SetScenario(GameObject scenario, bool isActive)
    {
        scenario.SetActive(isActive);
    }
}
