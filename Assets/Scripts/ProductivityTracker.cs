using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ProductivityTracker : MonoBehaviour
{
    private const float MAX_PRODUCTIVITY = 100f;
    public float productivity = MAX_PRODUCTIVITY;
    private Image productivityBar;
    public SpawnManager spawnManager;
    public GameObject titleScreen;
    public bool isGameActive;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winnerText;
     public TextMeshProUGUI scoreText;
    public Button restartButton;
    private int difficultyLevel;
    public float productivityRate;
    private int targetsRemaining;
    private int score;
    public bool scrumMasterSpawned;
    public int scrumMasterHits;
    // Start is called before the first frame update
    void Start()
    {
        productivityBar = GetComponent<Image>();
        productivityBar.fillAmount = 0;
        score = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetsRemaining = GameObject.FindGameObjectsWithTag("Computer").Length + GameObject.FindGameObjectsWithTag("Worker").Length + GameObject.FindGameObjectsWithTag("ScrumMaster").Length;
        if (isGameActive) {
            productivityRate = Time.deltaTime / (50 - targetsRemaining);
            trackProductivity();
        }
    }

    void increaseProductivity() {
        productivityBar.fillAmount += productivityRate;
    }

    void trackProductivity() {
        if (targetsRemaining > 0) {
            increaseProductivity();
        } 
        if (productivityBar.fillAmount >= 1) {
            gameOver("loser");
        } else if (targetsRemaining == 0) {
            gameOver("winner");
        }
        if ((GameObject.FindGameObjectsWithTag("Worker").Length <= spawnManager.workers.Length/2) && isGameActive && !scrumMasterSpawned) {
            spawnManager.SpawnScrumMaster();
            scrumMasterSpawned = true;
        }
    }

    void gameOver(string result) {
        if (result == "winner") {
            winnerText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
            UpdateScore((100 - Mathf.RoundToInt(productivityBar.fillAmount * 10)) * (difficultyLevel * 50));
        } else {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
        }
    }

    public void StartGame(int difficulty) {
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        spawnManager.SpawnWorker(difficulty);
        scrumMasterSpawned = false;
        UpdateScore(0);
        difficultyLevel = difficulty;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int ScoreToAdd) {
        score += ScoreToAdd + difficultyLevel + Mathf.RoundToInt(productivityBar.fillAmount * 10);
        scoreText.text = "Score: " + score;
    }
}
