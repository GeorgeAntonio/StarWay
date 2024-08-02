using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public float levelTime = 24f; // Tempo total do level em segundos
    private float countdown;
    public Text timerText;
    public GameObject winUI;
    public GameObject loseUI;
    public string[] levelScenes; // Array contendo o nome das scenes dos níveis

    void Start()
    {
        countdown = levelTime;
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        UpdateTimerUI();

        if (countdown <= 0)
        {
            CheckLevelCompletion();
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
            WinLevel();
        }
        else
        {
            // Ainda há inimigos restantes
            LoseLevel();
        }
    }

    void WinLevel()
    {
        winUI.SetActive(true);
        Invoke("LoadRandomLevel", 3f); // Aguarda 3 segundos antes de carregar o próximo nível
    }

    void LoseLevel()
    {
        loseUI.SetActive(true);
        Invoke("LoadRandomLevel", 3f); // Aguarda 3 segundos antes de carregar o próximo nível
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
}
