using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Referência ao jogador
    public float smoothSpeed = 0.125f;  // Velocidade de interpolação

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Mantém a câmera na mesma altura (eixo y) e profundidade (eixo z), se necessário
        smoothedPosition.y = transform.position.y;
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}
