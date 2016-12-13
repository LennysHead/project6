using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Playerehaviour : MonoBehaviour {

    List<Position> expectedForm = new List<Position>();
    List<string> moving = new List<string>();
    public int score = 0;
    int posX;
    int posY;
    int[,] playGround = new int[31, 14];
    int state = 0;
    public GameObject background;
    // Use this for initialization
    void Start () {
        playGround = background.GetComponent<BackgroundBehaviour>().getPlayGround();
        transform.position = new Vector3(0.18f, 0.172f);
        
        posX = 15;
        posY = 7;
        }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
          if(playGround[posX, posY-1] >= 1)
            {
                UnityEngine.Debug.Log("W");
                var speed = transform.position.y + 0.49f;
                transform.position = new Vector3(transform.position.x, speed);
                posY--;
            }
          

            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
           if(playGround[posX, posY + 1] >= 1)
            {
                UnityEngine.Debug.Log("S");
                var speed = transform.position.y - 0.49f;
                transform.position = new Vector3(transform.position.x, speed);
                posY++;
            } 
 
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
           if(playGround[posX-1, posY] >= 1)
            {
                UnityEngine.Debug.Log("A");
                var speed = transform.position.x - 0.49f;
                transform.position = new Vector3(speed, transform.position.y);
                posX--;
            }
                           
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(playGround[posX + 1, posY] >= 1)
            {
                UnityEngine.Debug.Log("D");
                var speed = transform.position.x + 0.49f;
                transform.position = new Vector3(speed, transform.position.y);
                posX++;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            state = Constants.ISACTIVE;
            addForm();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            state = Constants.ISDEACTIVATED;
        }

    }

    private void addForm()
    {
        Boolean check = false;
        if(expectedForm.Count == 0)
        {
            expectedForm.Add(new Position(posX, posY));
        }else
        {
            for(int i = 0; i < expectedForm.Count; i++){
                if (expectedForm[i].getX() == posX && expectedForm[i].getY() == posY)
                {
                    check = true;
                }
            }
            if (!check)
            {
                expectedForm.Add(new Position(posX, posY));
            }
        }
        
    }

    public int getState()
    {
        return state;
    }

    public Vector3 getPos()
    {
        return (new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    public int getX()
    {
        return posX;
    }

    public int getY()
    {
        return posY;
    }

    public void setState(int state)
    {
        this.state = state;
    }

    public int getScore()
    {
        return score;
    }
}
