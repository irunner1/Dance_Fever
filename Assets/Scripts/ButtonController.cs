using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //! Получает картинку со сцены
    private SpriteRenderer spriteRenderer;
    //! Картинка для обычной кнопки
    public Sprite defaultImage;
    //! Картинка для нажатой кнопки
    public Sprite pressedImage;
    //! Какую кнопку нужно нажать для картинки
    public KeyCode keyToPress;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            spriteRenderer.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            spriteRenderer.sprite = defaultImage;
        }
    }
}
