using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;

    [Header("Grounds")]
    public GameObject wall; // 벽
    public GameObject ground; // 바닥
    public GameObject underGround; // 아래 바닥
    public GameObject dHalfGround; // 아래 반 바닥
    public GameObject uHalfGround; // 위 반 바닥
    public GameObject mHalfGround; // 가운데 반 바닥

    [Header("WaterGrounds")]
    public GameObject lRedWater; // 왼쪽 빨간 물 
    public GameObject redWater; // 빨간 물 
    public GameObject rRedWater; // 오른쪽 빨간 물 
    public GameObject lBlueWater; // 왼쪽 파란 물
    public GameObject blueWater; // 파란 물
    public GameObject rBlueWater; // 오른쪽 파란 물 
    public GameObject lGreenWater; // 왼쪽 초록 물 
    public GameObject greenWater; // 초록 물
    public GameObject rGreenWater; // 오른쪽 초록 물 

    [Header("HalfWaterGrounds")]
    public GameObject hlRedWater; // 왼쪽 빨간 물 
    public GameObject hredWater; // 빨간 물 
    public GameObject hrRedWater; // 오른쪽 빨간 물 
    public GameObject hlBlueWater; // 왼쪽 파란 물
    public GameObject hblueWater; // 파란 물
    public GameObject hrBlueWater; // 오른쪽 파란 물 
    public GameObject hlGreenWater; // 왼쪽 초록 물 
    public GameObject hgreenWater; // 초록 물
    public GameObject hrGreenWater; // 오른쪽 초록 물 

    [Header("Players")]
    public GameObject player1; // 플레이어1
    public GameObject player2; // 플레이어2

    [Header("Items")]
    public GameObject blever; // 블루 레버
    public GameObject rlever; // 빨간 레버
    public GameObject ylever; // 노란 레버
    public GameObject yPlatform; // 레버와 상호작용하는 노란바닥
    public GameObject bPlatform; // 레버와 상호작용하는 파란바닥
    public GameObject rPlatform; // 레버와 상호작용하는 빨간바닥
    public GameObject downSwitch; //누르면 아래로 움직이는 바닥 스위치
    public GameObject downPlatform; //누르면 아래로 움직이는 바닥
    public GameObject box;
    public GameObject chain;

    [Header("Gems")]
    public GameObject blueGem;
    public GameObject redGem;

    [Header("Goals")]
    public GameObject redGoal;
    public GameObject blueGoal;

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
            { "wall", wall },
            { "ground", ground },
            { "uground", underGround },
            { "dhalfground", dHalfGround },
            { "uhalfground", uHalfGround },
            { "mhalfground", mHalfGround },
            { "Lredwater", lRedWater },
            { "redwater", redWater },
            { "Rredwater", rRedWater },
            { "Lbluewater", lBlueWater },
            { "bluewater", blueWater },
            { "Rbluewater", rBlueWater },
            { "Lgreenwater", lGreenWater },
            { "greenwater", greenWater },
            { "Rgreenwater", rGreenWater },
            { "player1", player1 },
            { "player2", player2 },
            { "blever", blever },
            { "rlever", rlever },
            { "ylever", ylever },
            { "yPlatform", yPlatform },
            { "rPlatform", rPlatform },
            { "bPlatform", bPlatform },
            { "blueGem", blueGem },
            { "redGem", redGem },
            { "downSwitch", downSwitch },
            { "downPlatform", downPlatform },
            { "box", box },
            { "redGoal", redGoal },
            { "blueGoal", blueGoal },
            { "hlRedWater", hlRedWater },
            { "hredWater", hredWater },
            { "hrRedWater", hrRedWater },
            { "hlBlueWater", hlBlueWater },
            { "hblueWater", hblueWater },
            { "hrBlueWater", hrBlueWater },
            { "hlGreenWater", hlGreenWater },
            { "hgreenWater", hgreenWater },
            { "hrGreenWater", hrGreenWater },
            { "chain", chain },
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
