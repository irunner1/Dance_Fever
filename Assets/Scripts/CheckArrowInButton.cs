using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArrowInButton : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed) { 
                // gameObject.SetActive(false);
                Destroy(gameObject);
                // GameManager.instance.NoteHit();
                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log(transform.position.y);
                    // Debug.Log("Normal");
                    // GameManager.instance.NormalHit();
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    // Debug.Log("Good");
                    // Debug.Log(transform.position.y);
                    GameManager.instance.GoodHit();
                }
                else
                {
                    // Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                }
            }
            else { // fix misses
                Debug.Log("Miss");
                Debug.Log(transform.position.y);
                GameManager.instance.DecreaseMultiplier();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
        }
    }
}
