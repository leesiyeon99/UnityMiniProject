using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject platformObject;
    public float riseSpeed = 2f;
    public float riseHeight;

    private Vector3 initialPosition;
    private bool isRising = false;
    private bool isLowering = false;

    private void Start()
    {
        initialPosition = platformObject.transform.position;
    }

    public bool IsRising => isRising;
    public bool IsLowering => isLowering;

    private void Update()
    {
        if (isRising)
        {
            platformObject.transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            if (platformObject.transform.position.y >= riseHeight)
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
        while (platformObject.transform.position.y > initialPosition.y)
        {
            platformObject.transform.position -= Vector3.up * riseSpeed * Time.deltaTime;
            yield return null;
        }

        platformObject.transform.position = initialPosition;
        isLowering = false;
    }
}
