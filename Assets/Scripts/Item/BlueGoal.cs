using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGoal : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            animator.Play("DoorOpen");
            GameManager.Instance.SetPlayer2Goal(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            animator.Play("DoorClose");
            GameManager.Instance.SetPlayer2Goal(false);
        }
    }
}
