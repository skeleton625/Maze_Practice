using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prims : MonoBehaviour, MazeGenerator
{
    private int Rows, Cols;
    private GameObject Block;
    private Transform CurrentField;
    private PrimMaze Pmaze;

    public Prims(int r, int c, GameObject _block, Transform _field, PrimMaze _maze)
    {
        Rows = (r % 2) == 1 ? r : r - 1;
        Cols = (c % 2) == 1 ? c : c - 1;
        Pmaze = _maze;
        Block = _block;
        CurrentField = _field;
    }

    public void AlgorithmStart()
    {
        // 시작 PrimCell 정의 및 Pmaze 시작부 초기화
        PrimCell start = new PrimCell(0, 0, null);
        Pmaze.At(start.Row, start.Col) = 'S';

        // 시작 PrimCell의 이웃 PrimCell 정의 및 리스트에 추가
        List<PrimCell> _frontier = new List<PrimCell>();
        _frontier.Add(new PrimCell(1, 0, start));
        _frontier.Add(new PrimCell(0, 1, start));

        PrimCell _child, _gc;

        while (_frontier.Count > 0)
        {
            // 리스트 내 랜덤 좌표를 자식으로 정의 및 _frontier 리스트에서 제거
            int rand_p = Random.Range(0, _frontier.Count);
            _child = _frontier[rand_p];
            _frontier.RemoveAt(rand_p);

            // 선택된 자식의 자식 PrimCell을 가져와 좌표를 _r, _c 변수로 정의
            _gc = _child.GetChild();
            int _r = _gc.Row;
            int _c = _gc.Col;

            // 선택된 자식 PrimCell의 좌표가 벽인지, 방문한 적 없을 경우
            if (Pmaze.IsMoveable(_r, _c))
            {
                Pmaze.At(_child.Row, _child.Col) = ' ';
                Pmaze.At(_r, _c) = 'E';

                List<PrimCell> _neighbors = Pmaze.GetNeighbors(_gc, true);
                foreach (PrimCell _cell in _neighbors)
                    _frontier.Add(_cell);

                Pmaze.At(_r, _c) = ' ';
            }
        }

        Pmaze.At(Rows - 1, Cols - 1) = 'E';
    }

    public void GenerateMaze()
    {
        float _x = 3f, _z = 3f;
        GameObject _clone;
        char[,] _grid = Pmaze.GetMazeMap();

        // 생성된 미로 출력
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                if (_grid[i, j].Equals('#'))
                {
                    _clone = Instantiate(Block, CurrentField);
                    _clone.transform.position = new Vector3(_x, 1, _z);
                }
                _x += 2f;
            }
            _x = 3;
            _z += 2f;
        }
    }
}
