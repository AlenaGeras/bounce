using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelLoader levelLoader;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private CameraController cameraController;
    private int initialLives = 3;

    private int currentLevel;
    private int currentLives;
    private int currentRings;
    private int maxLevel;
    public int CurrentLives => currentLives;
    public int CurrentRings => currentRings;
    public int CurrentLevel => currentLevel;
    public int MaxLevel => maxLevel;

    [SerializeField] private AudioClip gameOverAudio;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        currentLives = initialLives;
        uiManager.UpdateLivesText(currentLives);
        currentLevel = 0;
        if (PlayerPrefs.HasKey("MaxLevel"))
            maxLevel = PlayerPrefs.GetInt("MaxLevel");
    }

    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        uiManager.UpdateLivesText(currentLives);
    }

    public void InitRings(int rings)
    {
        currentRings = rings;
        uiManager.UpdateRingsCount(rings);
    }

    public void RemoveRing()
    {
        uiManager.UpdateRingsCount(--currentRings);
    }
    public void RemoveLives(int livesToRemove)
    {


        currentLives -= livesToRemove;
        SaveGame();
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            LoadLevel(currentLevel);
        }
        uiManager.UpdateLivesText(currentLives);



    }


    public void LoadLevel(int level)
    {
        if (level == levelLoader.LevelCount)
        {
            uiManager.GameWin();
            return;
        }
        levelLoader.LoadLevel(level);
        uiManager.ShowCurrentLevel(level);
        uiManager.SetBackground(level);
        currentLevel = level;
        if (currentLevel > maxLevel) maxLevel = currentLevel;
        SaveGame();
    }

    public void LoadNextLevel()
    {
        LoadLevel(++currentLevel);
    }
    public void SetBall(GameObject ball)
    {
        cameraController.SetObj(ball.transform);
    }

    public void StopGame()
    {
        levelLoader.ClearLevel();
    }

    private void GameOver()
    {
        uiManager.GameOverUI();
        currentLives = initialLives;
        SaveGame();
        gameObject.GetComponent<AudioSource>().clip = gameOverAudio;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void SaveGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CurrentLives", currentLives);
        PlayerPrefs.SetInt("MaxLevel", maxLevel);
        PlayerPrefs.Save();

    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            currentLives = PlayerPrefs.GetInt("CurrentLives");
            maxLevel = PlayerPrefs.GetInt("MaxLevel");
            uiManager.UpdateLivesText(currentLives);
            uiManager.ShowCurrentLevel(currentLevel);
        }
        else
        {
            currentLives = initialLives;
            currentLevel = 0;
            uiManager.UpdateLivesText(currentLives);
        }
        LoadLevel(currentLevel);
    }
}