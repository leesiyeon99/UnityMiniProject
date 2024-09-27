using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;
    public GameObject wallPrefab;
    public GameObject groundPrefab;
    public GameObject lRedWaterPrefab;
    public GameObject redWaterPrefab;
    public GameObject rRedWaterPrefab;
    public GameObject lBlueWaterPrefab;
    public GameObject blueWaterPrefab;
    public GameObject rBlueWaterPrefab;
    public GameObject lGreenWaterPrefab;
    public GameObject greenWaterPrefab;
    public GameObject rGreenWaterPrefab;
    public GameObject dHalfGround;
    public GameObject uHalfGround;
    public GameObject player1;
    public GameObject player2;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        string[] lines = csvFile.text.Split(new char[] { '\n' });

        for (int y = 0; y < lines.Length; y++)
        {
            string[] tiles = lines[y].Split(',');

            for (int x = 0; x < tiles.Length; x++)
            {
                GameObject tilePrefab = null;

                switch (tiles[x].Trim())
                {
                    case "wall":
                        tilePrefab = wallPrefab;
                        break;
                    case "ground":
                        tilePrefab = groundPrefab;
                        break;
                    case "Lredwater":
                        tilePrefab = lRedWaterPrefab;
                        break;
                    case "redwater":
                        tilePrefab = redWaterPrefab;
                        break;
                    case "Rredwater":
                        tilePrefab = rRedWaterPrefab;
                        break;
                    case "Lbluewater":
                        tilePrefab = lBlueWaterPrefab;
                        break;
                    case "bluewater":
                        tilePrefab = blueWaterPrefab;
                        break;
                    case "Rbluewater":
                        tilePrefab = rBlueWaterPrefab;
                        break;
                    case "Lgreenwater":
                        tilePrefab = lGreenWaterPrefab;
                        break;
                    case "greenwater":
                        tilePrefab = greenWaterPrefab;
                        break;
                    case "Rgreenwater":
                        tilePrefab = rGreenWaterPrefab;
                        break;
                    case "player1":
                        tilePrefab = player1;
                        break;
                    case "player2":
                        tilePrefab = player2;
                        break;
                    case "dhalfground":
                        tilePrefab = dHalfGround;
                        break;
                    case "uhalfground":
                        tilePrefab = uHalfGround;
                        break;
                }

                if (tilePrefab != null)
                {
                    GameObject tileObject = Instantiate(tilePrefab, new Vector3(x, -y, 0), Quaternion.identity);
                    tileObject.transform.parent = this.transform;
                }
            }
        }
    }
}
