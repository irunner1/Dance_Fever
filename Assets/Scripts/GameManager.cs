using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //! Объект аниматора, отвечает за начало и конец проигрывания анимации
    [SerializeField] private Animator dancerAnimationControllerProtagonist;
    [SerializeField] private Animator dancerAnimationControllerAntagonist;

    //! Объект текстого поля, которое выводится при начале игры
    [SerializeField] private TMP_Text startGameHint;
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

    //! Общее количество стрелок
    public float totalNotes;
    // Получить компонент из arrowphysics
    //! Количество нормальных попаданий
    public float normalHits;
    //! Количество хороших попаданий
    public float goodHits;
    //! Количество идеальных попаданий
    public float perfectHits;
    //! Количество пропущенных стрелок
    public float missedHits;

    //! Переменная отвечает за паузу в игре
    public bool pause;
    //! Объект экрана результатов
    public GameObject resultsScreen;
    //! Объект экрана настроек
    public GameObject settingsScreen;
    //! Текстовые поля на экране результатов
    public TMP_Text percentHitText, normalsText, goodsText, perfectsText, missedText, rankText, finalScoreText;
    
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        multText.text = "Multiplier: x1";
        currMultiplier = 1;
        startGameHint.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                startGameHint.gameObject.SetActive(false);
                beatScroller.hasStarted = true;
                dancerAnimationControllerProtagonist.SetBool("startAnimation", true);
                dancerAnimationControllerAntagonist.SetBool("startAnimation", true);
                song.Play();
            }
        }
        else
        {
            if (song.isPlaying && settingsScreen.activeInHierarchy) {
                song.Pause();
                pause = true;
                beatScroller.hasStarted = false;
                Debug.Log("tick");
            }
            if (pause && !settingsScreen.activeInHierarchy) {
                song.Play();
                pause = false;
                beatScroller.hasStarted = true;
                Debug.Log("tack");
            }
            if (!song.isPlaying && !resultsScreen.activeInHierarchy && pause == false)
            {
                startPlaying = false;
                dancerAnimationControllerProtagonist.SetBool("startAnimation", false);
                dancerAnimationControllerAntagonist.SetBool("startAnimation", false);
                beatScroller.hasStarted = false;
                
                resultsScreen.SetActive(true);
                normalsText.text = "" + normalHits;
                goodsText.text = "" + goodHits;
                perfectsText.text = "" + perfectHits;
                missedText.text = "" + missedHits;

                float totalHits = normalHits + goodHits + perfectHits;
                float percentHit = (totalHits / totalNotes) * 100f;
                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";
                if (percentHit > 40) {
                    rankVal = "D";
                    if (percentHit > 55) {
                        rankVal = "C";
                        if (percentHit > 70) {
                            rankVal = "B";
                            if (percentHit > 85) {
                                rankVal = "A";
                                if (percentHit > 55) {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = "" + rankVal;
                finalScoreText.text = currScore.ToString();
            }
        }
        totalNotes = goodHits + normalHits + perfectHits + missedHits;
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
        missedHits++;
    }

    ///
    /// Функция обычного попадания
    ///
    public void NormalHit() 
    {
        currScore += scorePerNote * currMultiplier;
        NoteHit();
        normalHits++;
    }
    ///
    /// Функция хорошего попадания
    /// добавляет множитель к очкам и дает больше очков
    ///

    public void GoodHit()
    {
        currScore += scorePerGoodNote * currMultiplier;
        NoteHit();
        goodHits++;
    }

    ///
    /// Функция идеального попадания
    /// добавляет множитель к очкам и дает максимальное колчичество очков
    ///
    public void PerfectHit()
    {
        currScore += scorePerPerfectNote * currMultiplier;
        NoteHit();
        perfectHits++;
    }
}
