using UnityEngine;
using System.Collections;
using System;

public class Position{

    int x;
    int y;
    Boolean isVisited;

	public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
        isVisited = true;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void setPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void setVisited(Boolean b)
    {
        this.isVisited = b;
    }

    public Boolean getVisited()
    {
        return isVisited;
    }
}
