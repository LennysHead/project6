using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class Background{

    int[,] playGround = new int[32, 15];
    List<Position> walls = new List<Position>();
    System.Random rnd = new System.Random();

    public Background()
    {
        
    }

    public int[,] generatePlayGround()
    {
        return buildPath(playGround);
    }

    /*
    * Randomisiertes Bauen des Weges
    * Es wird zunächst nach oben gegangen
    * Anschließend wird rekursiv jedes gesetzte Objekt in andere Richtungen gemalt
    */
    private int[,] buildPath(int[,] playGround)
    {
        int index = 0;
        Position currentPos = locateStartingPoint();

        prepareStartingPont(currentPos);

        generateMaze(currentPos, index);

        return playGround;
    }

    /**
     * Check if adjacent tiles are marked as walls and within the arrays
     */
    public void checkOnWalls(Position currentPos)
    {

        if (currentPos.getX() + 1 < 31 && currentPos.getX() + 1 != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX() + 1, currentPos.getY()));
        }
        if (currentPos.getX() - 1 >= 0 && currentPos.getX() - 1 != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX() - 1, currentPos.getY()));
        }
        if (currentPos.getY() + 1 < 14 && currentPos.getY() + 1 != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX(), currentPos.getY() + 1));
        }
        if (currentPos.getY() - 1 >= 0 && currentPos.getY() - 1 != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX(), currentPos.getY() - 1));
        }
    }

    /**
     * Generate the maze, in which the players are going to play
     */
    private void generateMaze(Position currentPos, int index)
    {
        while (walls.Count > 0)
        {
            index = findIndex(index);

            currentPos.setPos(walls[index].getX(), walls[index].getY());

            if (checkEndOfPlayGround(currentPos))
            {
                walls.RemoveAt(index);
            }
            else
            {
                if (adjacentPath(currentPos) == 1)
                {
                    prepareNextPoint(currentPos, index);
                }
                else
                {
                    lastElement(index);

                }
            }

        }
    }
    /**
     * Method to find a Starting Point within the array
     */
    private Position locateStartingPoint()
    {
        return new Position(rnd.Next(1, 30), rnd.Next(1, 13));
    }

    /**
     * Prepare the startingPoint and add the walls to the list 
     */
    private void prepareStartingPont(Position currentPos)
    {
        playGround[currentPos.getX(), currentPos.getY()] = 1;
        walls.Add(currentPos);
        checkOnWalls(currentPos);
    }

    /**
     * Check, if there are more is 1 element remaining in the list
     * if not, take a random object from the list
     */
    private int findIndex(int index)
    {
        if (walls.Count == 1)
        {
            index = 0;
        }
        else
        {
            index = rnd.Next(walls.Count);
        }
        return index;
    }

    /**
     * Check, if the currentPos is at the end of the array
     */
    private Boolean checkEndOfPlayGround(Position currentPos)
    {
        if (currentPos.getX() == 32 || currentPos.getX() == 0 || currentPos.getY() == 14 || currentPos.getY() == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * Check, if adjacent tiles have already been marked as part of the maze
     */
    private int adjacentPath(Position currentPos)
    {
        int counter = 0;
        if (playGround[currentPos.getX() + 1, currentPos.getY()] == 1)
        {
            counter++;
        }
        if (playGround[currentPos.getX() - 1, currentPos.getY()] == 1)
        {
            counter++;
        }
        if (playGround[currentPos.getX(), currentPos.getY() + 1] == 1)
        {
            counter++;
        }
        if (playGround[currentPos.getX(), currentPos.getY() - 1] == 1)
        {
            counter++;
        }
        return counter;
    }

    /**
     * set the current position as part of the maze and add adjacent walls to the list
     * then remove the current position from the list
     */
    private void prepareNextPoint(Position currentPos, int index)
    {
        playGround[currentPos.getX(), currentPos.getY()] = 1;
        checkOnWalls(currentPos);
        walls.RemoveAt(index);
    }

    /**
     * If the list only contains one element, delete the last element
     * otherwise delete the element which was worked with
     */
    private void lastElement(int index)
    {
        if (index == walls.Count)
        {
            Position tmp = walls[walls.Count - 1];
            walls.Remove(tmp);
        }
        else
        {
            walls.RemoveAt(index);
        }
    }

}
