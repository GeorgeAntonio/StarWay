using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && other.CompareTag("Player"))
        {
            GameController gameController = FindObjectOfType<GameController>();
            gameController.GoToLevel(sceneBuildIndex);
        }

        if (other.CompareTag("Jolene"))
        {
            Destroy(other.gameObject);
        }
    }
}