using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;

    [Header("Grounds")]
    public GameObject wallPrefab; // 벽
    public GameObject groundPrefab; // 바닥
    public GameObject underGroundPrefab; // 아래 바닥
    public GameObject dHalfGround; // 아래 반 바닥
    public GameObject uHalfGround; // 위 반 바닥
    public GameObject mHalfGround; // 가운데 반 바닥

    [Header("WaterGrounds")]
    public GameObject lRedWaterPrefab; // 왼쪽 빨간 물 
    public GameObject redWaterPrefab; // 빨간 물 
    public GameObject rRedWaterPrefab; // 오른쪽 빨간 물 
    public GameObject lBlueWaterPrefab; // 왼쪽 파란 물
    public GameObject blueWaterPrefab; // 파란 물
    public GameObject rBlueWaterPrefab; // 오른쪽 파란 물 
    public GameObject lGreenWaterPrefab; // 왼쪽 초록 물 
    public GameObject greenWaterPrefab; // 초록 물
    public GameObject rGreenWaterPrefab; // 오른쪽 초록 물 

    [Header("Players")]
    public GameObject player1; // 플레이어1
    public GameObject player2; // 플레이어2

    [Header("Items")]
    public GameObject blever; // 블루 레버
    public GameObject rlever; // 빨간 레버
    public GameObject plever; // 보라색 레버
    public GameObject platform; // 레버와 상호작용하는 바닥

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
