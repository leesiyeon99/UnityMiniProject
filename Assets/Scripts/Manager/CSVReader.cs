using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;

    [Header("Grounds")]
    public GameObject wallPrefab; // ��
    public GameObject groundPrefab; // �ٴ�
    public GameObject underGroundPrefab; // �Ʒ� �ٴ�
    public GameObject dHalfGround; // �Ʒ� �� �ٴ�
    public GameObject uHalfGround; // �� �� �ٴ�
    public GameObject mHalfGround; // ��� �� �ٴ�

    [Header("WaterGrounds")]
    public GameObject lRedWaterPrefab; // ���� ���� �� 
    public GameObject redWaterPrefab; // ���� �� 
    public GameObject rRedWaterPrefab; // ������ ���� �� 
    public GameObject lBlueWaterPrefab; // ���� �Ķ� ��
    public GameObject blueWaterPrefab; // �Ķ� ��
    public GameObject rBlueWaterPrefab; // ������ �Ķ� �� 
    public GameObject lGreenWaterPrefab; // ���� �ʷ� �� 
    public GameObject greenWaterPrefab; // �ʷ� ��
    public GameObject rGreenWaterPrefab; // ������ �ʷ� �� 

    [Header("Players")]
    public GameObject player1; // �÷��̾�1
    public GameObject player2; // �÷��̾�2

    [Header("Items")]
    public GameObject blever; // ��� ����
    public GameObject rlever; // ���� ����
    public GameObject plever; // ����� ����
    public GameObject platform; // ������ ��ȣ�ۿ��ϴ� �ٴ�

    private Dictionary<string, GameObject> tileDictionary;

    private void Start()
    {
        Init(); 
        GenerateMap(); 
    }

    private void Init()
    {
        tileDictionary = new Dictionary<string, GameObject>
        {
            { "wall", wallPrefab },
            { "ground", groundPrefab },
            { "uground", underGroundPrefab },
            { "dhalfground", dHalfGround },
            { "uhalfground", uHalfGround },
            { "mhalfground", mHalfGround },
            { "Lredwater", lRedWaterPrefab },
            { "redwater", redWaterPrefab },
            { "Rredwater", rRedWaterPrefab },
            { "Lbluewater", lBlueWaterPrefab },
            { "bluewater", blueWaterPrefab },
            { "Rbluewater", rBlueWaterPrefab },
            { "Lgreenwater", lGreenWaterPrefab },
            { "greenwater", greenWaterPrefab },
            { "Rgreenwater", rGreenWaterPrefab },
            { "player1", player1 },
            { "player2", player2 },
            { "blever", blever },
            { "rlever", rlever },
            { "plever", plever },
            { "platform", platform },
        };
    }

    private void GenerateMap()
    {
        string[] lines = csvFile.text.Split(new char[] { '\n' });

        for (int y = 0; y < lines.Length; y++)
        {
            CreateTiles(lines[y], -y);
        }
    }

    private void CreateTiles(string line, float y)
    {
        string[] tiles = line.Split(',');

        for (int x = 0; x < tiles.Length; x++)
        {
            string tileType = tiles[x].Trim();
            if (tileDictionary.TryGetValue(tileType, out GameObject tilePrefab)) 
            {
                Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);
            }
        }
    }
}
