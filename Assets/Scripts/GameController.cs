using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class GameController : MonoBehaviour
{
    public static bool isPuseed = false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuCanvas;
    public GameObject eventSystem;
    public static GameController instance = null;
    public TextMeshProUGUI LevelTextMeshPro;
    public TextMeshProUGUI LifeTextMeshPro;
    public TextMeshProUGUI MunicaoTextMeshPro;
    public GameObject player;

    private int level = 1;
    private int life = 100;

    private int municao = 15;
    private int municaoReserva = 30;

    AudioSource somDaPorta;

    void Start()
    {
        somDaPorta = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseMenuCanvas);
        DontDestroyOnLoad(eventSystem);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPuseed)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPuseed = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPuseed = true;
    }

    private void DestroyAll()
    {
        Destroy(gameObject);
        Destroy(pauseMenuCanvas);
        Destroy(eventSystem);
    }

    public void GoToMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        DestroyAll();
    }

    public int getLife()
    {
        return life;
    }

    public void setLife(int newLife)
    {
        life = newLife;
        LifeTextMeshPro.text = life.ToString();
        if (life <= 0)
        {
            SceneManager.LoadScene("GameOver");
            DestroyAll();
        }
    }

    public int getMunicao()
    {
        return municao;
    }

    private void updateMunicaoText()
    {
        MunicaoTextMeshPro.text = municao.ToString() + "/" + municaoReserva;

    }

    public void setMunicao(int newMunicao)
    {
        municao = newMunicao;
        updateMunicaoText();
    }

    public int getMunicaoReserva()
    {
        return municaoReserva;
    }

    public void setMunicaoReserva(int newMunicaoReserva)
    {
        municaoReserva = newMunicaoReserva;
        updateMunicaoText();
    }

    public void GoToLevel(int newLevel)
    { 
        somDaPorta.Play(0);
        level = newLevel;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        if(level == 6)
        {
            DestroyAll();
        } else
        {
            LevelTextMeshPro.text = "Level " + level;
        }

    }

    public void changeVolume(int newVolume)
    {
        Debug.Log(newVolume);
        somDaPorta.volume = newVolume;
    }
}
