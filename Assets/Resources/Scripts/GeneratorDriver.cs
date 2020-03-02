using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDriver : MonoBehaviour
{
    [SerializeField]
    private GameObject MazeField;
    [SerializeField]
    private GameObject Block;
    [SerializeField]
    private int Row, Col;
    private MazeGenerator Generator;

    private GameObject CurrentField;
    public static GeneratorDriver instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        CurrentField = Instantiate(MazeField, Vector3.zero, Quaternion.identity);
        Generator = new MazeGenerator(Row, Col);
    }

    public void ChooseGeneratingMaze(int _type)
    {
        RemoveCurrentField();
        Maze _maze = new Maze(Row, Col);

        // 원하는 미로 알고리즘으로 미로 설계도 생성
        switch(_type)
        {
            case 0:
                Generator.Prims(_maze, Row, Col);
                break;
            default:
                break;
        }

        GenerateMaze(_maze.GetMazeMap(), Row, Col);
    }

    private void RemoveCurrentField()
    {
        if(CurrentField)
        {
            Destroy(CurrentField);
            CurrentField = Instantiate(MazeField, Vector3.zero, Quaternion.identity);
        }
    }

    private void GenerateMaze(char[,] _grid, int _row, int _col)
    {
        float _x = 1f, _z = 1f;
        float _lastX = 2 * _col+3, _lastZ = 2 * _row +3;
        GameObject _clone1, _clone2;

        // 사이드 벽 채우기
        for (int i = 0; i < _row; i++)
        {
            _clone1 = Instantiate(Block, CurrentField.transform);
            _clone2 = Instantiate(Block, CurrentField.transform);
            _clone1.name = "SideWall";
            _clone2.name = "SideWall";
            _clone1.transform.position = new Vector3(1, 1, _z);
            _clone2.transform.position = new Vector3(_lastX, 1, _z);
            _z += 2f;
        }

        for (int i = 0; i < _col; i++)
        {
            _clone1 = Instantiate(Block, CurrentField.transform);
            _clone2 = Instantiate(Block, CurrentField.transform);
            _clone1.name = "SideWall";
            _clone2.name = "SideWall";
            _clone1.transform.position = new Vector3(_x, 1, 1);
            _clone2.transform.position = new Vector3(_x, 1, _lastZ);
            _x += 2f;
        }
        
        // 빈 벽 채우기
        _clone1 = Instantiate(Block, CurrentField.transform);
        _clone2 = Instantiate(Block, CurrentField.transform);
        _clone1.name = "SideWall";
        _clone2.name = "SideWall";
        _clone1.transform.position = new Vector3(1, 1, _lastZ-2f);
        _clone2.transform.position = new Vector3(_lastX-2f, 1, 1);
        _x = _z = 3f;

        // 생성된 미로 출력
        for (int i = 0; i < _row; i++)
        {
            for(int j = 0; j < _col; j++)
            {
                if(_grid[i,j] == '#')
                {
                    _clone1 = Instantiate(Block, CurrentField.transform);
                    _clone1.transform.position = new Vector3(_x, 1, _z);
                }
                _x += 2f;
            }
            _x = 3;
            _z += 2f;
        }
    }
}
