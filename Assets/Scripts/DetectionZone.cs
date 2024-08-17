using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecionZone : MonoBehaviour
{
    public string targetTag = "Player";

    public List<Collider2D> detectedObjects = new List<Collider2D>();

    // Start is called before the first frame update
    void OnTriggerEnter2D( Collider2D collider)
    {
        if (collider.gameObject.tag == targetTag)
        {
            detectedObjects.Add(collider);
        }
    }
    // Update is called once per frame
    void OnTriggerExit2D( Collider2D collider)
    {
        if (collider.gameObject.tag == targetTag)
        {
            detectedObjects.Remove(collider);
        }
    }
}
