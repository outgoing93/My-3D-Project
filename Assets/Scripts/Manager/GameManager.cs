using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("HP 설정")]
    [SerializeField] private int HP = 100;
    [SerializeField] private int maxHP = 100;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("HP 감소 설정")]
    [SerializeField] private float hpDecreaseInterval = 1f;
    [SerializeField] private int hpDecreaseAmount = 1;

    private int score = 0;
    private int highScore = 0;
    private float hpTimer = 0f;
    private float scoreTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        UpdateUI();
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameOverPanel.activeSelf) return;

        // HP 감소
        hpTimer += Time.deltaTime;
        if (hpTimer >= hpDecreaseInterval)
        {
            DecreaseHP(hpDecreaseAmount);
            hpTimer = 0f;
        }

        // 점수 증가 (1초당 1점)
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 1f)
        {
            AddScore(1);
            scoreTimer = 0f;
        }
    }

    public void IncreaseHP(int amount)
    {
        HP += amount;
        if (HP > maxHP) HP = maxHP;
        UpdateUI();
    }

    public void DecreaseHP(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            HP = 0;
            GameOver();
        }
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
            highScore = score;
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = "HP: " + HP;
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
