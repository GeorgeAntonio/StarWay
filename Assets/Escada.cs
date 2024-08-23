using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escada : MonoBehaviour
{

    [SerializeField]
    private string nomeProximaFase;

    private void OnTriggerEnter2D(Collider2D collision){
        ProximaFase();
    }

    private void ProximaFase(){
        SceneManager.LoadScene(this.nomeProximaFase);
    }
}