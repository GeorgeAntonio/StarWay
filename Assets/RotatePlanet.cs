using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    public float rotationSpeed = 10f;  // Velocidade de rotação

    void Update()
    {
        // Rotaciona o planeta em torno do seu eixo Y a cada quadro
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
