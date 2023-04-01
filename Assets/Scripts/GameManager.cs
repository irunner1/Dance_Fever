using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource song;
    public bool startPlaying;
    public ArrowPhysics beatScroller;
    public static GameManager instance;

    //Score
    public int currScore = 0;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    //Multiplier
    public int currMultiplier;
    public int multiplierTracker;
    public int[] multiplierHolder;

    //UI Elements
    public TMP_Text scoreText;
    public TMP_Text multText;

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
    }
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
    public void DecreaseMultiplier() 
    {
        multiplierTracker = 0;
        currMultiplier = 1;
        multText.text = "Multiplier: x" + currMultiplier;
    }
    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        increseMultiplier();
        scoreText.text = "Score: " + currScore;
        multText.text = "Multiplier: x" + currMultiplier;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed note");
        currMultiplier = 1;
        multiplierTracker = 0;
        multText.text = "Multiplier: x" + currMultiplier;
    }

    public void NormalHit() 
    {
        currScore += scorePerNote * currMultiplier;
        NoteHit();
    }
    public void GoodHit()
    {
        currScore += scorePerGoodNote * currMultiplier;
        NoteHit();
    }
    public void PerfectHit()
    {
        currScore += scorePerPerfectNote * currMultiplier;
        NoteHit();
    }
}
