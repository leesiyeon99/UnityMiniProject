using System.Collections;
using UnityEngine;

public class RedLever : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Platform platform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") && !platform.IsRising && !platform.IsLowering)
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
