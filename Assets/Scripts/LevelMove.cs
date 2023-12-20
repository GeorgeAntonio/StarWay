using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Verifica se é a nave (com a tag "Player") que colidiu com o planeta
        if (other.CompareTag("Player"))
        {
            // Carrega a cena correspondente ao planeta
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
