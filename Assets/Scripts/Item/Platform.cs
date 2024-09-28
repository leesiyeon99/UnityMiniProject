using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject platform;
    public float riseSpeed = 2f;
    public float riseHeight;

    private Vector3 initialPosition;
    private bool isRising = false;
    private bool isLowering = false;


    private void Start()
    {
        initialPosition = platform.transform.position;
    }

    public bool IsRising => isRising;
    public bool IsLowering => isLowering;

    private void Update()
    {
        if (isRising)
        {
            platform.transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            if (platform.transform.position.y >= initialPosition.y + riseHeight)
            {
                isRising = false;
                StartCoroutine(WaitAndLower());
            }
        }
    }

    public void Rise()
    {
        isRising = true;
    }

    private IEnumerator WaitAndLower()
    {
        yield return new WaitForSeconds(1f);

        isLowering = true;
        while (platform.transform.position.y > initialPosition.y)
        {
            platform.transform.position -= Vector3.up * riseSpeed * Time.deltaTime;
            yield return null;
        }

        platform.transform.position = initialPosition;
        isLowering = false;
    }
}
