using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //! Песня, которая играет на фоне
    public AudioSource song;
    //! Клип пеесни, которая играет на фоне
    public AudioClip targetClip;

    //! Индикатор начала игры
    public bool startPlaying;
    //! Указатель на другой объект сцены 
    public ArrowPhysics beatScroller;
    //! Дублирует self
    public static GameManager instance;

    //Score
    //! Текущий показатель очков
    public int currScore = 0;
    //! Количество очков за попадание
    public int scorePerNote = 100;
    //! Количество очков за хорошее попадание
    public int scorePerGoodNote = 125;
    //! Количество очков за идеальное попадание
    public int scorePerPerfectNote = 150;

    //Multiplier
    //! Текущий показатель множения очков
    public int currMultiplier;
    //! Счетчик множителя очков
    public int multiplierTracker;
    //! Сколько очков дается за действия
    public int[] multiplierHolder;

    //UI Elements
    //! Элемент текста для показателя очков TextMeshPro
    public TMP_Text scoreText;
    //! Элемент текста для множителя TextMeshPro
    public TMP_Text multText;

    // //! 
    // public float totalNotes;
    // //!
    // public float normalHits;
    // //!
    // public float goodHits;
    // //!
    // public float perfectHits;
    // //!
    // public float missedHits;
    
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        multText.text = "Multiplier: x1";
        currMultiplier = 1;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;
                song.Play();
            }
        }
        else
        {
            if (!song.isPlaying)
            {
                startPlaying = false;
                //End Game
            }
        }
    }
    ///
    /// Функция увеличивает множитель
    ///
    public void increseMultiplier() 
    {
        if (currMultiplier - 1 < multiplierHolder.Length) 
        {
            multiplierTracker++;
            if (multiplierHolder[currMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currMultiplier++;
            }
        }
    }
    ///
    /// Функция уменьшает множитель
    ///
    public void DecreaseMultiplier() 
    {
        multiplierTracker = 0;
        currMultiplier = 1;
        multText.text = "Multiplier: x" + currMultiplier;
    }
    ///
    /// Функция попадания в удар
    ///
    public void NoteHit()
    {
        // Debug.Log("Hit On Time");
        increseMultiplier();
        scoreText.text = "Score: " + currScore;
        multText.text = "Multiplier: x" + currMultiplier;
    }
    ///
    /// Функция увеличивает множитель
    ///
    public void NoteMissed()
    {
        Debug.Log("Missed note");
        currMultiplier = 1;
        multiplierTracker = 0;
        multText.text = "Multiplier: x" + currMultiplier;
    }
    ///
    /// Функция обычного попадания
    ///
    public void NormalHit() 
    {
        currScore += scorePerNote * currMultiplier;
        NoteHit();
    }
    ///
    /// Функция хорошего попадания
    /// добавляет множитель к очкам и дает больше очков
    ///
    public void GoodHit()
    {
        currScore += scorePerGoodNote * currMultiplier;
        NoteHit();
    }
    ///
    /// Функция идеального попадания
    /// добавляет множитель к очкам и дает максимальное колчичество очков
    ///
    public void PerfectHit()
    {
        currScore += scorePerPerfectNote * currMultiplier;
        NoteHit();
    }
}
