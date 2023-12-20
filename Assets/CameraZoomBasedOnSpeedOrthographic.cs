using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomBasedOnSpeedOrthographic : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Rigidbody2D playerRigidbody;
    public float minOrthographicSize = 5f;   // Tamanho ortográfico mínimo
    public float maxOrthographicSize = 10f;  // Tamanho ortográfico máximo baseado na velocidade
    public float maxSpeed = 10f;             // Velocidade máxima esperada do player
    public float zoomSpeed = 1f;             // Quão rápido a câmera se ajusta ao tamanho alvo

    void Update()
    {
        if (virtualCamera != null && playerRigidbody != null)
        {
            float speed = playerRigidbody.velocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);
            float targetOrthographicSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, normalizedSpeed);

            // Interpolar suavemente para o tamanho alvo
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);
        }
    }
}
