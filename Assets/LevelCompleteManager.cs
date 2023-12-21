using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Use "using TMPro;" se estiver usando TextMeshPro
using TMPro;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject panel;                  // Referência ao painel que contém o texto
    public TextMeshProUGUI statusText;       // Referência ao componente TextMeshProUGUI onde o status será exibido
    public string spaceSceneName;            // Nome da sua cena do espaço
    // Nome da sua cena do espaço

    private float habitabilityPercentage;

    private void Start()
    {
        // Calcula a habitabilidade no início baseado na quantidade de inimigos e bosses
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        int bossCount = GameObject.FindGameObjectsWithTag("Boss").Length;
        habitabilityPercentage = 100f - (enemyCount * 10f + bossCount * 30f);
        habitabilityPercentage = Mathf.Clamp(habitabilityPercentage, 0f, 100f); // Garante que está entre 0 e 100
        panel.SetActive(false); // Esconde o painel inicialmente
    }

    private void Update()
    {
        // Verifica se não existem mais inimigos ou bosses na cena
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0)
        {
            ShowCompletionStatus();
        }
    }

    private void ShowCompletionStatus()
    {
        panel.SetActive(true);  // Ativa o painel
        string recommendation = habitabilityPercentage >= 90f ? "Planeta recomendado" : "Planeta não recomendado";
        Debug.Log("ShowCompletionStatus called.");
        // Define o texto com base na porcentagem de habitabilidade
        statusText.text = $"Habitabilidade do Planeta é: {habitabilityPercentage:F1}%\n{recommendation}";

        // Se não recomendado, agendar retorno para a cena do espaço após 10 segundos
        if (habitabilityPercentage < 90f)
        {
            Invoke("ReturnToSpaceScene", 10f);
        }

        // Desativa o script para que o texto não seja atualizado novamente
        this.enabled = false;
    }
public void PlayerDied()
{
    Debug.Log("PlayerDied called. Habitability is 0%.");
    habitabilityPercentage = 0f;  // Define a habitabilidade para 0%
    ShowCompletionStatus();
}

    private void ReturnToSpaceScene()
    {
        SceneManager.LoadScene(spaceSceneName);  // Carrega a cena do espaço
    }
}
