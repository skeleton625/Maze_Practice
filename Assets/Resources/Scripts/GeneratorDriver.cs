using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDriver : MonoBehaviour
{
    [SerializeField]
    private GameObject MazeField, FinishPoint;
    [SerializeField]
    private GameObject Block, SlimBlock;
    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private bool PlayerMode;

    private int Row, Col;
    private GameObject CurrentField;
    private UIController UControl;
    public static GeneratorDriver instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UControl = UIController.instance;
        CurrentField = Instantiate(MazeField, Vector3.zero, Quaternion.identity);
    }

    public void ChooseGeneratingMaze(int _type)
    {
        Row = UControl.GetInputFieldRow();
        Col = UControl.GetInputFieldCol();
        Row = (Row % 2) == 1 ? Row : Row - 1;
        Col = (Col % 2) == 1 ? Col : Col - 1;
        // 현재 생성된 미로를 제거
        RemoveCurrentField();
        MazeGenerator Generator = null;

        // 원하는 미로 알고리즘으로 미로 설계도 생성
        switch(_type)
        {
            case 0:
                PrimMaze _prim = new PrimMaze(Row, Col);
                Generator = new Prims(Block, CurrentField.transform, _prim);
                break;
            case 1:
                HKMaze _huntandkill = new HKMaze(Row, Col);
                Generator = new HuntandKill(SlimBlock, CurrentField.transform, _huntandkill);
                break;
            default:
                _prim = new PrimMaze(0, 0);
                Generator = new Prims(Block, CurrentField.transform, _prim);
                break;
        }
        Generator.AlgorithmStart();
        Generator.GenerateMaze();
        // 주변 미로 벽 출력
        GenerateSideBlock(Row, Col);
        if(PlayerMode)
        {
            MainCamera.SetActive(false);
            Player.SetActive(true);
            StartCoroutine(UControl.StartTimerCoroutine());
        }

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
        float _x = 2f, _z = 2f;
        float _lastX = 4 * _col + 6, _lastZ = 4 * _row + 6;
        GameObject _clone1, _clone2;

        // 사이드 벽 채우기
        for (int i = 0; i < _row; i++)
        {
            _clone1 = Instantiate(Block, CurrentField.transform);
            _clone2 = Instantiate(Block, CurrentField.transform);
            _clone1.name = "SideWall";
            _clone2.name = "SideWall";
            _clone1.transform.position = new Vector3(2, 2, _z);
            _clone2.transform.position = new Vector3(_lastX, 2, _z);
            _z += 4f;
        }

        for (int i = 0; i < _col; i++)
        {
            _clone1 = Instantiate(Block, CurrentField.transform);
            _clone2 = Instantiate(Block, CurrentField.transform);
            _clone1.name = "SideWall";
            _clone2.name = "SideWall";
            _clone1.transform.position = new Vector3(_x, 2, 2);
            _clone2.transform.position = new Vector3(_x, 2, _lastZ);
            _x += 4f;
        }

        // 빈 벽 채우기
        _clone1 = Instantiate(Block, CurrentField.transform);
        _clone2 = Instantiate(Block, CurrentField.transform);
        _clone1.name = "SideWall";
        _clone2.name = "SideWall";
        _clone1.transform.position = new Vector3(2, 2, _lastZ - 4f);
        _clone2.transform.position = new Vector3(_lastX - 4f, 2, 2);
        _clone1 = Instantiate(FinishPoint, CurrentField.transform);
        _clone1.name = "FinishPoint";
        _clone1.transform.position = new Vector3(_lastX, 1.5f, _lastZ);
    }
}
