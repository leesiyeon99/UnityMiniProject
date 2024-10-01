using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;

    [Header("Grounds")]
    public GameObject wall; // ��
    public GameObject ground; // �ٴ�
    public GameObject underGround; // �Ʒ� �ٴ�
    public GameObject dHalfGround; // �Ʒ� �� �ٴ�
    public GameObject uHalfGround; // �� �� �ٴ�
    public GameObject mHalfGround; // ��� �� �ٴ�

    [Header("WaterGrounds")]
    public GameObject lRedWater; // ���� ���� �� 
    public GameObject redWater; // ���� �� 
    public GameObject rRedWater; // ������ ���� �� 
    public GameObject lBlueWater; // ���� �Ķ� ��
    public GameObject blueWater; // �Ķ� ��
    public GameObject rBlueWater; // ������ �Ķ� �� 
    public GameObject lGreenWater; // ���� �ʷ� �� 
    public GameObject greenWater; // �ʷ� ��
    public GameObject rGreenWater; // ������ �ʷ� �� 

    [Header("HalfWaterGrounds")]
    public GameObject hlRedWater; // ���� ���� �� 
    public GameObject hredWater; // ���� �� 
    public GameObject hrRedWater; // ������ ���� �� 
    public GameObject hlBlueWater; // ���� �Ķ� ��
    public GameObject hblueWater; // �Ķ� ��
    public GameObject hrBlueWater; // ������ �Ķ� �� 
    public GameObject hlGreenWater; // ���� �ʷ� �� 
    public GameObject hgreenWater; // �ʷ� ��
    public GameObject hrGreenWater; // ������ �ʷ� �� 

    [Header("Players")]
    public GameObject player1; // �÷��̾�1
    public GameObject player2; // �÷��̾�2

    [Header("Items")]
    public GameObject blever; // ��� ����
    public GameObject rlever; // ���� ����
    public GameObject ylever; // ��� ����
    public GameObject yPlatform; // ������ ��ȣ�ۿ��ϴ� ����ٴ�
    public GameObject bPlatform; // ������ ��ȣ�ۿ��ϴ� �Ķ��ٴ�
    public GameObject rPlatform; // ������ ��ȣ�ۿ��ϴ� �����ٴ�
    public GameObject downSwitch; //������ �Ʒ��� �����̴� �ٴ� ����ġ
    public GameObject downPlatform; //������ �Ʒ��� �����̴� �ٴ�
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
