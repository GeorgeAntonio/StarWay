using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string[] levelScenes; // Array contendo o nome das scenes dos níveis

    public void Play()
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

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
