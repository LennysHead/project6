using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System;
using System.Collections.Generic;

public class BackgroundBehaviour : MonoBehaviour
{

    public GameObject player;
    Background background = new Background();
    Sprite sprite = new Sprite();
    GameObject[,] gameObjectArray = new GameObject[32, 15];
    int[,] playerPath = new int[32, 15];

    int[,] playGround = new int[32, 15];


    // Use this for initialization
    void Start()
    {
        background.generatePlayGround();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Playerehaviour>().getState() == 1)
        {
            playerPath[player.GetComponent<Playerehaviour>().getX(), player.GetComponent<Playerehaviour>().getY()] = 1;
            sprite = Resources.Load<Sprite>("BlockPlayer");
            gameObjectArray[player.GetComponent<Playerehaviour>().getX(), player.GetComponent<Playerehaviour>().getY()].GetComponent<SpriteRenderer>().sprite = sprite;
            player.GetComponent<Playerehaviour>().setState(Constants.READY);
        }
        if (player.GetComponent<Playerehaviour>().getState() == 2)
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (playerPath[i, j] == 1)
                    {
                        gameObjectArray[i, j].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BackgroundBlock");
                        player.GetComponent<Playerehaviour>().setState(Constants.READY);
                    }
                }
            }
        }
    }

    /**
     * Getter for the gameObjectArray
     */ 
    public GameObject[,] getGameObjectArray()
    {
        return gameObjectArray;
    }

    /**
     * Getter for the playGround
     */ 
    public int[,] getPlayGround()
    {
        return playGround;
    }


}
