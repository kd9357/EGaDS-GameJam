using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public float GameTimer = 30f;
    public float BonusTime = 10f;

    public Canvas CanvasUI;
    //public Text ScoreText;
    //public Text CapacityText;
    //public Text TimerText;

    public bool EndGame = false;

    public Player Player;

    //Game vars
    private int _itemCount = 0;
    private Text[] _texts;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _texts = CanvasUI.GetComponentsInChildren<Text>();
        _texts[_texts.Length - 1].enabled = false;
    }

    private void Update()
    {
        GameTimer -= Time.deltaTime;
        if(!EndGame)
        {
            _texts[0].text = "Trash collected: " + _itemCount;
            _texts[1].text = "Capacity: " + Player.TrashAmount() + "/" + Player.TrashCapacity;
            string message = "Time: ";

            if (GameTimer > 60f)
            {
                int minutes = (int)GameTimer / 60;
                int seconds = (int)GameTimer % 60;
                if (seconds >= 10)
                    message += minutes + ":" + seconds;
                else
                    message += minutes + ":0" + seconds; 
               
            }
            else if(GameTimer >= 10f)
                message += "0:" + ((int)GameTimer);
            else
            {
                //TODO: decimal notation smaller
                message += "0:0" + GameTimer.ToString("F2");
            }
            _texts[2].text = message;
        }
        if (GameTimer <= 0 && !EndGame)
        {
            GameOver();
        }

    }

    public void GameOver()
    {
        EndGame = true;
        for (int i = 1; i < _texts.Length; i++)
        {
            _texts[i].enabled = !_texts[i].enabled;
        }
        //TODO: Reset or kick back to title screen or something
    }

    public void UpdateScore(int trashNum)
    {
        _itemCount += trashNum;
        GameTimer += BonusTime * trashNum;
    }
}
