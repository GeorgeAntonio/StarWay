using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectile;

    public Transform spawnLocation;

    public Quaternion spawnRotation;

    public float SpawnTime = 0.5f;

    private float timeSinceSpawned = 0.5f;

    private AudioSource arrowSound;


    public DetecionZone detectionZone;
    // Start is called before the first frame update
    void Start()
    {
        arrowSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(detectionZone.detectedObjects.Count > 0)
        {
            timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned >= SpawnTime)
            {
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timeSinceSpawned = 0;
                arrowSound.Play();
            }
        } else {
            timeSinceSpawned = 0.5f;
        }
    
    }
}
