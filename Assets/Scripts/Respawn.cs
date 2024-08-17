using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Arraste o GameObject "RespawnPoint" aqui no Inspector.
    private Vector3 initialPosition;

    void Start()
    {
        // Salva a posição inicial do jogador
        initialPosition = transform.position;
    }

    public void Respawn()
    {
        // Reposiciona o jogador no ponto de respawn
        transform.position = respawnPoint.position;
        // Se necessário, você pode redefinir outros aspectos do jogador, como saúde, animação, etc.
    }

    public void Die()
    {
        // Aqui você pode adicionar lógica para quando o jogador "morrer".
        // Por exemplo, desabilitar o controle do jogador, tocar uma animação de morte, etc.

        // Chamando o método de respawn após a morte
        Respawn();
    }
}
