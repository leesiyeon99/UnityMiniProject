using UnityEngine;
using UnityEngine.UI;

public class ClearImageUI : MonoBehaviour
{
    private Image clearImage;

    private void Start()
    {
        clearImage = GetComponent<Image>();
        clearImage.gameObject.SetActive(false);
        GameManager.Instance.OnGameClear.AddListener(Show);
    }

    public void Show()
    {
        clearImage.gameObject.SetActive(true);
    }

}
