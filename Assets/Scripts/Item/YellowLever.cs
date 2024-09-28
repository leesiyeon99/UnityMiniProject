using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLever : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> yPlatforms;

    private void Start()
    {
        GameObject[] platformObjects = GameObject.FindGameObjectsWithTag("yPlatform");
        yPlatforms.AddRange(platformObjects);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2")) && CanActivate())
        {
            animator.Play("LeverOn");
            foreach (var platformObject in yPlatforms)
            {
                Platform platform = platformObject.GetComponent<Platform>();
                if (platform != null)
                {
                    platform.Rise();
                }
            }
            StartCoroutine(LeverCoroutine());
        }
    }

    private bool CanActivate()
    {
        foreach (var platformObject in yPlatforms)
        {
            Platform platform = platformObject.GetComponent<Platform>();
            if (platform != null && (platform.IsRising || platform.IsLowering))
                return false;
        }
        return true;
    }

    IEnumerator LeverCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        animator.Play("LeverIdle");
    }
}
