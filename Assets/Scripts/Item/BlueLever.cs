using System.Collections;
using UnityEngine;

public class BlueLever : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Platform platform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2") && !platform.IsRising && !platform.IsLowering)
        {
            animator.Play("LeverOn");
            platform.Rise();
            StartCoroutine(LeverCoroutine());
        }
    }

    IEnumerator LeverCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        animator.Play("LeverIdle");
    }
}
