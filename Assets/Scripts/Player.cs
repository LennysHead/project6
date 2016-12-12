using UnityEngine;
using System.Collections;

public class Player{

    int order = 1;
   
    string name;
    int[,] playGround = new int[32, 15];
    int state;
    int posX;
    int posY;
    

    public Player(string name)
    {
        this.name = name;
        posX = 15;
        posY = 7;
        state = 0;

    }

    public void setMovingConditions()
    {
        if (name.Equals("Player_0"))
        {
            UnityEngine.Debug.Log("Test");
        }
    }
        
}
