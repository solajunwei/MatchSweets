using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // 创建一个单例
    public static GameManager _instance;
    public static GameManager _Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    public enum SweetsType
    {
        EMPTY,
        NORMAL,
        BARRIER,
        ROW_CLEAR,
        RAINBOWCANDY,
        COUNT, //标记类型
    }

    // 设置甜品的预制体的字典， 我们可以通过甜品的种类来得到对应的甜品游戏物体
    public Dictionary<SweetsType, GameObject> sweetPrefabDict;

    // 能够在Unity的UI界面显示出控件
    [System.Serializable]
    public struct SweetPrefab
    {
        public SweetsType type;
        public GameObject prefab;
    }
    public SweetPrefab[] sweetPrefabs;

    // 行
    public int yRow;

    // 列
    public int xCol;

    // 网格模板
    public GameObject GridPrefabs;

    //甜品的二维数组
    private GameSweet[,] sweets;
    
    // 在最开始的时候调用
    void Awake()
    {
        _instance = this;
    }


	// 开始的时候进行初始化
	void Start () {
        // 字典实例化
        sweetPrefabDict = new Dictionary<SweetsType, GameObject>();
        for(int i = 0; i < sweetPrefabs.Length; i++)
        {
            // 检验字典中是否已经存在该甜品，不存在则直接添加进去
            if(!sweetPrefabDict.ContainsKey(sweetPrefabs[i].type))
            {
                sweetPrefabDict.Add(sweetPrefabs[i].type, sweetPrefabs[i].prefab);
            }
        }


        // 初始化背景方块
        for (int x = 0; x < xCol; x++)
        {
            for(int y = 0; y < yRow; y++)
            {
                GameObject chocolate = Instantiate(GridPrefabs, CorrectPosition(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);
            }
        }

        // 初始化甜品二维数组
        sweets = new GameSweet[xCol, yRow];

        // 初始化甜品
        for(int x = 0; x < xCol; x++)
        {
            for(int y = 0; y < yRow; y++)
            {
                GameObject newSweet = Instantiate(sweetPrefabDict[SweetsType.NORMAL], CorrectPosition(x, y), Quaternion.identity);
                newSweet.transform.SetParent(transform);
                
                // 获取控件中的脚本，并对其进行初始化
                sweets[x, y] = newSweet.GetComponent<GameSweet>();
                sweets[x, y].Init(x, y, this, SweetsType.NORMAL);
            }
        }
	}

    // 设置背景方块位置
    public Vector3 CorrectPosition(int x, int y)
    {
        // x 为背景坐标减去列数的一半 加上 位置
        // y 为背景坐标减去行数的一半 加上 位置
        return new Vector3(transform.position.x - xCol / 2 + x, transform.position.y + yRow / 2 - y, 0);
    }
	
}
