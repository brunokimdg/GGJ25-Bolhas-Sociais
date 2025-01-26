using UnityEngine;

public class TimerPlatformController : MonoBehaviour
{
    public float timeToReachBubble = 5f;
    private bool timerStarted = false;
    private float timer = 0f;

    private void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (timer > timeToReachBubble)
            {
                Debug.Log("Failed to reach the gray bubble in time!");
                // Lógica adicional aqui.
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timerStarted = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timerStarted = false;
            timer = 0f;
        }
    }
}
