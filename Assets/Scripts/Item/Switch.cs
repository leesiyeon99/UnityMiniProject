using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveDistance = 13f;
    [SerializeField] float moveSpeed = 0.5f;

    private bool isPlayerOnSwitch = false;

    [SerializeField] List<GameObject> downPlatforms;
    private List<Vector3> originalPositions = new List<Vector3>();

    private static int activeSwitchCount = 0;

    private void Start()
    {
        GameObject[] platformObjects = GameObject.FindGameObjectsWithTag("DownPlatform");
        downPlatforms.AddRange(platformObjects);

        foreach (GameObject platform in downPlatforms)
        {
            originalPositions.Add(platform.transform.position);
        }
    }

    private void Update()
    {
        for (int i = 0; i < downPlatforms.Count; i++)
        {
            GameObject downPlatform = downPlatforms[i];
            Vector3 targetPosition = originalPositions[i] - new Vector3(0, moveDistance, 0);

            if (activeSwitchCount > 0) 
            {
                downPlatform.transform.position = Vector3.Lerp(downPlatform.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                downPlatform.transform.position = Vector3.Lerp(downPlatform.transform.position, originalPositions[i], moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2") || collision.CompareTag("Box"))
        {
            if (activeSwitchCount == 0) 
            {
                activeSwitchCount++;
                isPlayerOnSwitch = true;
                animator.Play("DownSwitch");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2") || collision.CompareTag("Box"))
        {
            if (activeSwitchCount > 0) 
            {
                activeSwitchCount--;
                isPlayerOnSwitch = false;
                animator.Play("SwitchIdle");
            }
        }
    }
}
