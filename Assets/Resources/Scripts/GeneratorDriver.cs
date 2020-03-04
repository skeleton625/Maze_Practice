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

    private GameObject CurrentField;
    public static GeneratorDriver instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrentField = Instantiate(MazeField, Vector3.zero, Quaternion.identity);
    }

    public void ChooseGeneratingMaze(int _type)
    {
        // 현재 생성된 미로를 제거
        RemoveCurrentField();
        MazeGenerator Generator = null;

        // 원하는 미로 알고리즘으로 미로 설계도 생성
        switch(_type)
        {
            case 0:
                PrimMaze _prim = new PrimMaze(Row, Col, '#');
                Generator = new Prims(Row, Col, Block, CurrentField.transform, _prim);
                break;
            case 1:
                break;
            default:
                _prim = new PrimMaze(0, 0, '#');
                Generator = new Prims(0, 0, Block, CurrentField.transform, _prim);
                break;
        }
        Generator.AlgorithmStart();
        Generator.GenerateMaze();
        // 주변 미로 벽 출력
        GenerateSideBlock(Row, Col);
    }

    private void RemoveCurrentField()
    {
        if(CurrentField)
        {
            Destroy(CurrentField);
            CurrentField = Instantiate(MazeField, Vector3.zero, Quaternion.identity);
        }
    }

    private void GenerateSideBlock(int _row, int _col)
    {
        float _x = 1f, _z = 1f;
        float _lastX = 2 * _col + 3, _lastZ = 2 * _row + 3;
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
        _clone1.transform.position = new Vector3(1, 1, _lastZ - 2f);
        _clone2.transform.position = new Vector3(_lastX - 2f, 1, 1);
    }
}
