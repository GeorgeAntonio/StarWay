using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public float levelTime = 24f; // Tempo total do level em segundos
    private float countdown;
    public Text timerText;
    public string[] levelScenes; // Array contendo o nome das scenes dos níveis
    private Player player;  // Referência ao jogador

    void Start()
    {
        countdown = levelTime;

        // Encontrar o jogador na cena
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        UpdateTimerUI();

        if (countdown <= 0)
        {
            // Se o tempo acabar, transição para a cena do espaço
            LoadSpaceScene();
        }
        else
        {
            CheckLevelCompletion();
        }

        // Verifica a vida do jogador usando o método GetCurrentHealth
        if (player != null && player.GetCurrentHealth() <= 0)
        {
            PlayerDied();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Max(0, Mathf.Ceil(countdown)).ToString();
    }

    void CheckLevelCompletion()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            // Todos os inimigos foram destruídos
            LoadRandomLevel();
        }
    }

    public void PlayerDied()
    {
        Debug.Log("PlayerDied called. Transitioning to space scene.");
        LoadSpaceScene();
    }

    void LoadRandomLevel()
    {
        if (levelScenes.Length > 0)
        {
            int randomIndex = Random.Range(0, levelScenes.Length);
            SceneManager.LoadScene(levelScenes[randomIndex]);
        }
        else
        {
            Debug.LogWarning("Nenhuma scene de nível configurada.");
        }
    }

    void LoadSpaceScene()
    {
        SceneManager.LoadScene("Space");
    }
}
