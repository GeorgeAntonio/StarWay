using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Sprite[] backgrounds; // Array de Sprites para o background
    public float minChangeTime = 2f; // Tempo mínimo para trocar de imagem
    public float maxChangeTime = 5f; // Tempo máximo para trocar de imagem

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeBackground());
    }

    IEnumerator ChangeBackground()
    {
        while (true)
        {
            // Espera por um tempo aleatório
            yield return new WaitForSeconds(Random.Range(minChangeTime, maxChangeTime));

            // Escolhe uma imagem aleatória e a define como background
            Sprite newBackground = backgrounds[Random.Range(0, backgrounds.Length)];
            spriteRenderer.sprite = newBackground;
        }
    }
}
