using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Form{
    List<Position> endForm = new List<Position>();
    List<Position> tmpPosition = new List<Position>();
    Position startingPos;
    int counter;
    System.Random rnd = new System.Random();
    int index;
    int end;
    int[,] playGround;

    public Form(int[,] playGround, Position startingPos)
    {
        this.playGround = playGround;
        initiate(startingPos);
        createForm();
        sort();
        for (int i = 0; i < endForm.Count; i++)
        {
            UnityEngine.Debug.Log("Element:" + i + ", X: " + endForm[i].getX() + ", Y: " + endForm[i].getY());
        }
        UnityEngine.Debug.Log(endForm.Count); 
    }

    /**
     * Method to check, if the adjacent tiles are usable paths
     */ 
    private void checkOnWalls()
    { 
        if(checkRight())
        {
            addRight();  
        }
        if (checkLeft())
        {
            addLeft();
        }
        if (checkBottom())
        {
            addBottom();
        }
        if (checkTop())
        {
            addTop();
        }
        checkLastTile();
        
    }

    /**
     * overwrite the existing playGround with the current playGround
     * define size of form
     */ 
    private void initiate(Position startingPos)
    {
        
        this.startingPos = startingPos;
        counter = 5;
        tmpPosition.Add(new Position(startingPos.getX(), startingPos.getY()));
        //tmpPosition.Add(endForm[0]);
        checkOnWalls();
    }

    /**
     * Method for creating the form
     */
    private void createForm()
    {
        while (counter != 0)
        {
            if (tmpPosition.Count == 1)
            {
                startingPos = tmpPosition[0];
            }
            else
            {
                index = rnd.Next(1, tmpPosition.Count);

                startingPos = tmpPosition[index];
            }

            checkOnWalls();
        }

    }

    /**
     * check, if the right tile is usable
     */
    private bool checkRight()
    {
        if(startingPos.getX() + 1 < 32 && playGround[startingPos.getX() + 1, startingPos.getY()] == 1)
        {
            return true;
        }else
        {
            return false;
        }
    }

    /**
     * check, if the left tile is usable
     */
    private bool checkLeft()
    {
        if(startingPos.getX() - 1 >= 0 && playGround[startingPos.getX() - 1, startingPos.getY()] == 1)
        {
            return true;
        }else
        {
            return false;
        }
    }

    /**
     * check, if the tile down under is usable
     */
    private bool checkBottom()
    {
        if (startingPos.getY() + 1 < 15 && playGround[startingPos.getX(), startingPos.getY() + 1] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * check, if the tile above is usable
     */
    private bool checkTop()
    {
        if (startingPos.getY() - 1 >= 0 && playGround[startingPos.getX(), startingPos.getY() - 1] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * Method to add the right tile to current form
     */
    private void addRight()
    {
        endForm.Add(new Position(startingPos.getX() + 1, startingPos.getY()));
        tmpPosition.Add(new Position(startingPos.getX() + 1, startingPos.getY()));
        counter--;
        playGround[startingPos.getX() + 1, startingPos.getY()] = 2;
    }

    /**
     * add the left tile to the current form
     */
    private void addLeft()
    {
        endForm.Add(new Position(startingPos.getX() - 1, startingPos.getY()));
        tmpPosition.Add(new Position(startingPos.getX() - 1, startingPos.getY()));
        counter--;
        this.playGround[startingPos.getX() - 1, startingPos.getY()] = 2;

    }

    /**
     * add bottom tile to the current form
     */
    private void addBottom()
    {
        endForm.Add(new Position(startingPos.getX(), startingPos.getY() + 1));
        tmpPosition.Add(new Position(startingPos.getX(), startingPos.getY() + 1));
        counter--;
        playGround[startingPos.getX(), startingPos.getY() + 1] = 2;
    }


    /**
     * add top tile to the current form
     */
    private void addTop()
    {
        endForm.Add(new Position(startingPos.getX(), startingPos.getY() - 1));
        tmpPosition.Add(new Position(startingPos.getX(), startingPos.getY() - 1));
        counter--;
        playGround[startingPos.getX(), startingPos.getY() - 1] = 2;
    }

    /**
     * check, if the current position is the last element in the list
     */
    private void checkLastTile()
    {
        if (tmpPosition.Count == 1)
        {
            tmpPosition.Remove(startingPos);
        }
        else
        {
            tmpPosition.RemoveAt(index);
        }

    }
    /**
     * Method to sort the elements of the form
     */
    private void sort()
    {
        initiateSort();
        sortY();
        initiateSort();
        sortX();
                
    }

    /**
     * Reset sorting components
     */ 
    private void initiateSort()
    {
        index = 0;
        end = endForm.Count;
        
    }
    
    /**
     * Sort for y variable
     */ 
    private void sortY()
    {
        Position tmp;
        while (end != 0)
        {
            index = 0;
            while (index < end)
            {
                if (index < endForm.Count - 1 && endForm[index].getY() > endForm[index + 1].getY())
                {
                    tmp = endForm[index];
                    endForm[index] = endForm[index + 1];
                    endForm[index + 1] = tmp;
                }
                index++;
            }
            end--;
        }
    }

    /**
     * Sort for x variable
     */ 
    private void sortX()
    {
        Position tmp;
        while (end != 0)
        {
            index = 0;
            while (index < end)
            {
                if (index < endForm.Count - 1 && endForm[index].getX() > endForm[index + 1].getX())
                {
                    tmp = endForm[index];
                    endForm[index] = endForm[index + 1];
                    endForm[index + 1] = tmp;
                }
                index++;
            }
            end--;
        }
    }

    public List<Position> getEndForm()
    {
        return endForm;
    }

}
