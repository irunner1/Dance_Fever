using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArrowInButton : MonoBehaviour
{
    //! Переменная говорит о том, что объект попал в поле картинки
    public bool canBePressed;
    //! Код кнопки, которую нужно нажать
    public KeyCode keyToPress;
    //! Объекты эффектов, спавнятся при хорошем\плохом\отличном\идеальном попадании
    public GameObject hitEffect, goodHitEffect, perfectHitEffect, missEffect;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            // if (!canBePressed) { // fix misses
            //     Debug.Log("Miss");
            //     Debug.Log(transform.position.y);
            //     GameManager.instance.DecreaseMultiplier();
            //     Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            // }
            if (canBePressed) { 
                // Destroy(gameObject);
                gameObject.SetActive(false);
                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Normal");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodHitEffect, transform.position, goodHitEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectHitEffect, transform.position, perfectHitEffect.transform.rotation);
                }
            }
        }
    }

    ///
    /// Функция проверки попадания в объект
    ///
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    ///
    /// Функция проверки выхода из объекта
    ///
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}
