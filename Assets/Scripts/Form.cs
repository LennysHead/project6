using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Form{
    List<Position> test = new List<Position>();
    List<Position> tmpPosition = new List<Position>();
    int counter;
    System.Random rnd = new System.Random();
    int index;
    int[,] playGround;

    public Form(int[,] playGround, Position startingPos)
    {
        
        this.playGround = playGround;
        counter = 5;

        test.Add(new Position(startingPos.getX(), startingPos.getY()));
        tmpPosition.Add(test[0]);
        checkOnWalls(startingPos);

        while (counter != 0)
        {
            index = rnd.Next(1, tmpPosition.Count);
            checkOnWalls(startingPos);
        }

    }

    private void checkOnWalls(Position startingPos)
    {
        //TODO Add IsVisited zur Überprüfung
        if(startingPos.getX() + 1 < 32 && playGround[startingPos.getX() + 1, startingPos.getY()] == 1)
        {
            tmpPosition.Add(new Position(startingPos.getX() + 1, startingPos.getY()));
            counter--;
        }
        if (startingPos.getX() - 1 >= 0 && playGround[startingPos.getX() - 1, startingPos.getY()] == 1)
        {
            tmpPosition.Add(new Position(startingPos.getX() - 1, startingPos.getY()));
            counter--;
        }
        if (startingPos.getY() + 1 < 15 && playGround[startingPos.getX(), startingPos.getY() + 1] == 1)
        {
            tmpPosition.Add(new Position(startingPos.getX(), startingPos.getY() + 1));
            counter--;
        }
        if (startingPos.getY() - 1 >= 0 && playGround[startingPos.getX(), startingPos.getY() - 1] == 1)
        {
            tmpPosition.Add(new Position(startingPos.getX(), startingPos.getY() - 1));
            counter--;
        }
    }
	
}
