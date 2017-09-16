using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Canvas CanvasUI;

    public bool EndGame = false;

    //private Player player;

    //Game vars
    private int _itemCount = 0;

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

    private void Update()
    {

    }

    public void GameOver()
    {
        EndGame = true;
    }

    public void IncrementScore()
    {
        _itemCount++;
    }
}
