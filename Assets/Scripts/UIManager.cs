using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesTextLabel;
    [SerializeField] private GameObject ringPrefab;

    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject gameWinMenu;

    [SerializeField] private Button buttonPrefab;

    [SerializeField] private Sprite[] levelImages;

    [SerializeField] private Image bckgroundImage;

    [SerializeField] private TextMeshProUGUI currentLevelText;

    private List<GameObject> ringObjects = new List<GameObject>();
        private List<Button> levelbuttons = new List<Button>();

    private List<GameObject> menus;

    void Start()
    {
        GameObject[] foundMenus = GameObject.FindGameObjectsWithTag("Menu");
        menus = new List<GameObject>(foundMenus);
        SetActiveMenu(startMenu);
        GenerateLevelButtons();

    }
    public void UpdateLivesText(int lives)
    {
        livesTextLabel.text = "X" + lives.ToString();
    }

    void GenerateLevelButtons()
    {
        foreach (Button obj in levelbuttons)
        {
            if (obj != null)
                Destroy(obj.gameObject);
        }
        levelbuttons.Clear();
        
        int i = 1;
        foreach (Sprite spr in levelImages)
        {
            if( (PlayerPrefs.HasKey("MaxLevel") && PlayerPrefs.GetInt("MaxLevel") < i-1) || 
            (!PlayerPrefs.HasKey("MaxLevel") && i!=1 ) ) break;

            Button newButton = Instantiate(buttonPrefab, buttonPrefab.transform.parent);
            newButton.GetComponent<Image>().sprite = spr;
            newButton.gameObject.SetActive(true);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i;
            int levelIndex = i - 1;

            newButton.onClick.AddListener(() => LoadLevel(levelIndex));
            levelbuttons.Add(newButton); 
            i++;
        }
    }
    public void UpdateRingsCount(int rings)
    {
        foreach (GameObject obj in ringObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        ringObjects.Clear();

        for (int i = 0; i < rings; i++)
        {
            GameObject newRing = Instantiate(ringPrefab, Vector2.zero, Quaternion.identity, ringPrefab.transform.parent);
            ringObjects.Add(newRing);
            newRing.SetActive(true);

        }

    }

    public void ShowCurrentLevel(int level)
    {
        currentLevelText.text = level.ToString();

    }
    public void LoadLevel(int level)
    {
        GameManager.Instance.LoadLevel(level);
        SetActiveMenu(gameMenu);
    }

    public void SetActiveMenu(GameObject activeMenu)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(menu == activeMenu);
        }
        GenerateLevelButtons();
    }

    public void OpenLevelMenu()
    {
        GameManager.Instance.StopGame();
        SetActiveMenu(levelMenu);
    }

    public void LoadCurrLevel()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.CurrentLevel);
    }

    public void GameOverUI()
    {
        SetActiveMenu(gameOverMenu);
    }

    public void GameWin()
    {
        SetActiveMenu(gameWinMenu);
    }

    public void SetBackground(int level)
    {
        bckgroundImage.sprite = levelImages[level];
    }

    public void ResumeGame()
    {
        GameManager.Instance.LoadGame();
        SetActiveMenu(gameMenu);
    }
}