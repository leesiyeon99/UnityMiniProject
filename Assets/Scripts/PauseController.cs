using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pause;
    public void Pause()
    {
        pause.SetActive(true);
    }
}
