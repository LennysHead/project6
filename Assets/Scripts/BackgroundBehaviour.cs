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
        playGround = background.getPlayground();
        gameObjectArray = background.getGameObjectArray();
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkPath())
        {
            case 1: markPath();
                    break;
            case 2: resetPath();
                    break;
            default: break;
        }
    }

    /**
     * Mark the walked tiles as visited
     */ 
    private void markPath()
    {
        playerPath[player.GetComponent<Playerehaviour>().getX(), player.GetComponent<Playerehaviour>().getY()] = 1;
        sprite = Resources.Load<Sprite>("BlockPlayer");
        gameObjectArray[player.GetComponent<Playerehaviour>().getX(), player.GetComponent<Playerehaviour>().getY()].GetComponent<SpriteRenderer>().sprite = sprite;
    }
    /**
     * And reset the marked path
     */
    private void resetPath()
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
    /**
     * check the state of the Player
     */
    private int checkPath()
    {
        return player.GetComponent<Playerehaviour>().getState();
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
