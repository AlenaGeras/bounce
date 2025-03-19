using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject waterPrefab;

    [SerializeField] private GameObject ringPrefab;

    [SerializeField] private GameObject spikePrefab;

    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private GameObject trapsquarePrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject bubblePrefab;

    [SerializeField] private TextAsset[] levelFiles;
    [SerializeField] private float tileSizeMultiplier = 1f;
    [SerializeField] private float levelVerticalOffset = 0f;

    private List<GameObject> levelObjects = new List<GameObject>();

    [SerializeField] private GameObject playerPrefab;
    private float calculatedTileSize = 1f;
    private float calculatedTileSizeY = 1f;
    private int levelCount;
    public int LevelCount => levelCount;

    void Start()
    {
        levelCount = levelObjects.Count;
    }

    void CalculateTileSize()
    {
        SpriteRenderer brickRenderer = brickPrefab.GetComponentInChildren<SpriteRenderer>();


        calculatedTileSize = brickRenderer.bounds.size.x * tileSizeMultiplier;
        calculatedTileSizeY = brickRenderer.bounds.size.y * tileSizeMultiplier;
    }


    public void LoadLevel(int levelIndex)
    {

        CalculateTileSize();

        if (levelIndex < 0 || levelIndex >= levelFiles.Length)
        {
            return;
        }

        TextAsset levelTextAsset = levelFiles[levelIndex];
        if (levelTextAsset == null)
        {
            return;
        }

        ClearLevel();

        string levelText = levelTextAsset.text;
        levelText = levelText.Replace(" ", "");
        levelText = levelText.Replace("\t", "");
        string[] levelLines = levelText.Split('\n');
        int ringsCount = 0;
        Vector3 playerStartPosition = Vector3.zero;
        int k = 0; int l = 0;
        for (int y = 0; y < levelLines.Length; y++)
        {
            string line = levelLines[y].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            for (int x = 0; x < line.Length; x++)
            {
                char symbol = line[x];
                Vector3 tilePosition = new Vector3((x + l * line.Length - l) * calculatedTileSize, ((-y + k) * calculatedTileSizeY) + levelVerticalOffset, 0);

                switch (symbol)
                {
                    case '#':
                        levelObjects.Add(Instantiate(brickPrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'W':
                        levelObjects.Add(Instantiate(waterPrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'R':
                        levelObjects.Add(Instantiate(ringPrefab, tilePosition, Quaternion.identity, transform));
                        ringsCount++;
                        break;
                    case 'S':
                        levelObjects.Add(Instantiate(spikePrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'B':
                        levelObjects.Add(Instantiate(bubblePrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'T':
                        levelObjects.Add(Instantiate(trapPrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'Q':
                        levelObjects.Add(Instantiate(trapsquarePrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'O':
                        levelObjects.Add(Instantiate(portalPrefab, tilePosition, Quaternion.identity, transform));
                        break;
                    case 'p':
                        GameObject wall = Instantiate(brickPrefab, tilePosition, Quaternion.identity, transform);
                        levelObjects.Add(wall);
                        wall.GetComponent<TrapsquareAnimation>().enabled = true;
                        break;
                    case 'V':
                        wall = Instantiate(brickPrefab, tilePosition, Quaternion.identity, transform);
                        levelObjects.Add(wall);
                        wall.GetComponent<TrapsquareAnimation>().enabled = true;
                        wall.GetComponent<TrapsquareAnimation>().SetParametrs(1, 3);
                        break;
                    case 'P':
                        GameObject player = Instantiate(playerPrefab, tilePosition, Quaternion.identity, transform.parent);
                        player.gameObject.tag = "Player";
                        levelObjects.Add(player);
                        GameManager.Instance.SetBall(player);
                        break;
                    case '+':
                        {
                            k = y + 1;
                            l++;
                            break;
                        }
                }
            }
        }

        GameManager.Instance.InitRings(ringsCount);

        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(true);
        }

    }

    public void ClearLevel()
    {
        foreach (GameObject obj in levelObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        levelObjects.Clear();
    }
}