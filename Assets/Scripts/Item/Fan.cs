using UnityEngine;

public class Fan : MonoBehaviour
{
    public float windForce = 5f; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, windForce);
            }
        }
    }
}
