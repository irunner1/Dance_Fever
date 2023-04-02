using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPhysics : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;

    // Рандомный спавн объектов
    public GameObject leftArrow, rightArrow, upArrow, downArrow;
    public float spawnRate = 2f;
    float nextSpawn = 0f;
    int whatToSpawn;
    
    public GameManager gameManager;

    void Start()
    {
        int bpm = UniBpmAnalyzer.AnalyzeBpm(gameManager.targetClip);
        Debug.Log("BPM is " + bpm);
        beatTempo = bpm / 60f;
        spawnRate = beatTempo / 8;
    }

    void Update()
    {
        if (hasStarted)
        {
            SpawnArrows();
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    private void SpawnArrows()
    {
        if (Time.time > nextSpawn) 
        {
            whatToSpawn = Random.Range(1, 5);
            switch (whatToSpawn)
            {
                case 1: 
                    Instantiate(leftArrow, new Vector3(-1.72f, 8f, 0f), leftArrow.transform.rotation, transform); 
                    break;
                case 2: 
                    Instantiate(rightArrow, new Vector3(2.6f, 8f, 0f), rightArrow.transform.rotation, transform); 
                    break;
                case 3: 
                    Instantiate(upArrow, new Vector3(-0.3f, 8f, 0f), upArrow.transform.rotation, transform); 
                    break;
                case 4: 
                    Instantiate(downArrow, new Vector3(1.12f, 8f, 0f), downArrow.transform.rotation, transform); 
                    break;
                default:
                    break;
            }
            nextSpawn = Time.time + spawnRate;
        }
    }
}
