using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPhysics : MonoBehaviour
{
    //! Счетчик стрелок
    public int count_arrows = 0;
    //! Скорость бита
    public float beatTempo;
    //! Переменная хранит значение началась игра или нет
    public bool hasStarted;
    //! Объекта стрелок на сцене
    public GameObject leftArrow, rightArrow, upArrow, downArrow;
    //! Задержка между спавном объектов
    public float spawnRate = 2f;
    //! Время до спавка объектов
    float nextSpawn = 0f;
    //! Переменная хранит номер объекта для спавна
    int whatToSpawn;
    
    public GameManager gameManager;
    ///
    ///  Функция начинает игру и вызывает функцию, переводящую бит в секунды
    ///
    void Start()
    {
        int bpm = UniBpmAnalyzer.AnalyzeBpm(gameManager.targetClip);
        Debug.Log("BPM is " + bpm);
        beatTempo = bpm / 60f;
        spawnRate = beatTempo / 9f;
    }
    ///
    /// Функция обновления, при обновлении спавнятся стрелки, если игра началась
    ///
    void Update() {
        if (hasStarted) {
            SpawnArrows();
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
        // else {
        //     Debug.Log("stop");
        // }
    }
    ///
    /// Функция спавна объектов в разных точках рандомно
    ///
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
            count_arrows++;
        }
    }
}
