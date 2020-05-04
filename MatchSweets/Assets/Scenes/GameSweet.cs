using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSweet : MonoBehaviour
{

    // 甜品的x坐标
    private int x;
    public int X
    {
        get
        {
            return x;
        }

        set
        {
            if (CanMove())
            {
                x = value;
            }
        }
    }

    //甜品的y坐标
    private int y;
    public int Y
    {
        get
        {
            return y;
        }

        set
        {
            if (CanMove())
            {
                y = value;
            }
        }
    }

    // 甜品的种类
    private GameManager.SweetsType type;
    public GameManager.SweetsType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    // 甜品的移动状态
    private MovedSweet moveComponent;
    private MovedSweet MoveComponent
    {
        get
        {
            return moveComponent;
        }
        set
        {
            moveComponent = value;
        }
    }

    // 甜品是否可以移动
    public bool CanMove()
    {
        return moveComponent != null;
    }

    [HideInInspector]
    public GameManager gameManager;

    void Awake()
    {
        moveComponent = GetComponent<MovedSweet>();
    }

    // 糖果的基础脚本进行初始化
    public void Init(int _x, int _y, GameManager _gameManager, GameManager.SweetsType _type)
    {
        x = _x;
        y = _y;
        gameManager = _gameManager;
        type = _type;
    }
}
