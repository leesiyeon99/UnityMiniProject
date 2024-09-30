using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") && this.gameObject.CompareTag("RedGoal"))
        {
            animator.Play("DoorOpen");
        }
        if (collision.gameObject.CompareTag("Player2") && this.gameObject.CompareTag("BlueGoal"))
        {
            animator.Play("DoorOpen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") && this.gameObject.CompareTag("RedGoal"))
        {
            animator.Play("DoorClose");
        }
        if (collision.gameObject.CompareTag("Player2") && this.gameObject.CompareTag("BlueGoal"))
        {
            animator.Play("DoorClose");
        }
    }
}
