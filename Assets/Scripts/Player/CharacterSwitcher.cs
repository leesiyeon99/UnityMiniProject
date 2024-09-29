using Cinemachine;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public PlayerController player1; 
    //public CinemachineVirtualCamera virtualCamera1;
    public PlayerController player2; 
    //public CinemachineVirtualCamera virtualCamera2;

    private PlayerController currentPlayer;

    [SerializeField] GameObject player1Active;
    [SerializeField] GameObject player2Active;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
        player1Active = GameObject.FindGameObjectWithTag("Player1Active");
        player2Active = GameObject.FindGameObjectWithTag("Player2Active");
        //virtualCamera1 = player1.GetComponentInChildren<CinemachineVirtualCamera>();
        //virtualCamera2 = player2.GetComponentInChildren<CinemachineVirtualCamera>();


        currentPlayer = player1;
        player1Active.SetActive(true);
        player2Active.SetActive(false);
        player1.canMove = true;
        player2.canMove = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        if (currentPlayer == player1)
        {
            currentPlayer.canMove = false;
            player1Active.SetActive(false);
            currentPlayer = player2;
            currentPlayer.canMove = true;
            player2Active.SetActive(true);
            //virtualCamera1.Priority = 8;
            //virtualCamera2.Priority = 10;
        }
        else
        {
            currentPlayer.canMove = false;
            player2Active.SetActive(false);
            currentPlayer = player1;
            currentPlayer.canMove = true;
            player1Active.SetActive(true);
            //virtualCamera1.Priority = 10;
            //virtualCamera2.Priority = 8;
        }
    }
}
