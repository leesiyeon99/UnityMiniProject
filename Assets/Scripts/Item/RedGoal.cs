using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGoal : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            animator.Play("DoorOpen");
            GameManager.Instance.SetPlayer1Goal(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            animator.Play("DoorClose");
            GameManager.Instance.SetPlayer1Goal(false);
        }
    }
}
