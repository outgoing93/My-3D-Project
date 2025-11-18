using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("HP 설정")]
    [SerializeField] private int HP = 100;
    [SerializeField] private int maxHP = 100;

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

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        UIManager.Instance?.HideGameOver();
        UIManager.Instance?.UpdateHP(HP);
        UIManager.Instance?.UpdateScore(score, highScore);
        
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return; // 게임 오버 시 멈춤

        hpTimer += Time.deltaTime;
        if (hpTimer >= hpDecreaseInterval)
        {
            DecreaseHP(hpDecreaseAmount);
            hpTimer = 0f;
        }

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 1f)
        {
            AddScore(1);
            scoreTimer = 0f;
        }
    }

    public void IncreaseHP(int amount)
    {
        HP = Mathf.Min(HP + amount, maxHP);
        UIManager.Instance?.UpdateHP(HP);
    }

    public void DecreaseHP(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            HP = 0;
            GameOver();
        }
        UIManager.Instance?.UpdateHP(HP);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
            highScore = score;

        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

        UIManager.Instance?.UpdateScore(score, highScore);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        UIManager.Instance?.ShowGameOver();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
