using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class Background{

    int[,] playGround = new int[32, 15];
    GameObject[,] gameObjectArray = new GameObject[32, 15];
    List<Position> walls = new List<Position>();
    System.Random rnd = new System.Random();
    Sprite sprite = new Sprite();

    public Background()
    {
        
    }

    public void generatePlayGround()
    {

        buildPath(playGround);
        drawBackground(playGround);
        findForms(playGround);
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
        currentPos.setPos(0, 0);
        prepareStartingPont(currentPos);
        
        generateMaze(currentPos, index);

        return playGround;
    }

    /**
     * Check if adjacent tiles are marked as walls and within the arrays
     */
    public void checkOnWalls(Position currentPos)
    {
        if (currentPos.getX() + 1 < 31 && playGround[currentPos.getX() + 1, currentPos.getY()] != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX() + 1, currentPos.getY()));
        }
        if (currentPos.getX() - 1 >= 0 && playGround[currentPos.getX() - 1, currentPos.getY()] != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX() - 1, currentPos.getY()));
        }
        if (currentPos.getY() + 1 < 14 && playGround[currentPos.getX(), currentPos.getY() + 1] != Constants.MAZE)
        {
            walls.Add(new Position(currentPos.getX(), currentPos.getY() + 1));
        }
        if (currentPos.getY() - 1 >= 0 && playGround[currentPos.getX(), currentPos.getY() - 1] != Constants.MAZE)
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
        if (currentPos.getX() == 32 || currentPos.getY() == 14)
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

        if (currentPos.getX()+1 < 32 && playGround[currentPos.getX() + 1, currentPos.getY()] == 1)
        {
            counter++;
        }
        if (currentPos.getX()-1 >= 0 && playGround[currentPos.getX() - 1, currentPos.getY()] == 1)
        {
            counter++;
        }
        if (currentPos.getY()+1 < 14 && playGround[currentPos.getX(), currentPos.getY() + 1] == 1)
        {
            counter++;
        }
        if (currentPos.getY()-1 >=0 && playGround[currentPos.getX(), currentPos.getY() - 1] == 1)
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
     * Method to draw the Labyrinth in which the players are moving
     */
    private void drawBackground(int[,] playGround)
    {
        for (int j = 0; j < 15; j++)
        {
            for (int i = 0; i < 32; i++)
            {
                if (checkIfPath(i, j))
                {
                    createPath(i, j);
                }
            }
        }
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

    /**
     * Method to check, if the current position is part of the maze, or a wall
     */ 
    private Boolean checkIfPath(int i, int j)
    {
        if(playGround[i,j] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * Method to set the particular position as walkable part of the maze
     */
    private void createPath(int i, int j)
    {
        sprite = Resources.Load<Sprite>("BackgroundBlock");

        gameObjectArray[i, j] = new GameObject("Path");
        gameObjectArray[i, j].transform.position = new Vector3(-7.176f + (i * 0.49f), 3.607f + (-j * 0.49f));
        gameObjectArray[i, j].AddComponent<SpriteRenderer>();
        gameObjectArray[i, j].GetComponent<SpriteRenderer>().sprite = sprite;
        gameObjectArray[i, j].GetComponent<SpriteRenderer>().sortingLayerName = "Background";
    }

    private void findForms(int[, ] playGround)
    {
        for(int i = 0; i < 32; i++)
        {
            for(int j = 0; j< 15; j++)
            {
                if(i == 0 && j == 0 && playGround[0,0] == 1)
                {
                    Form form = new Form(playGround, new Position(0,0));
                }
            }
        }

    }

    public int[,] getPlayground()
    {
        return playGround;
    }
    public GameObject[,] getGameObjectArray()
    {
        return gameObjectArray;
    }

}
