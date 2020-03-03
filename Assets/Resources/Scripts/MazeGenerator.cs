using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    private int Rows, Cols;
    private Maze PreMaze;
    public MazeGenerator(int r, int c)
    {
        Rows = (r % 2) == 1 ? r : r - 1;
        Cols = (c % 2) == 1 ? c : c - 1;
    }

    // '#' : -1, ' ' : 0, 'S' : 1, 'E' : 2, 'V' : 3
    public void Prims(Maze _maze)
    {
        PreMaze = _maze;
        // 시작 Cell 정의 및 _maze 시작부 초기화
        Cell start = new Cell(0, 0, null);
        PreMaze.At(start.Row, start.Col) = 1;

        // 시작 Cell의 이웃 Cell 정의 및 리스트에 추가
        List<Cell> _frontier = new List<Cell>();
        _frontier.Add(new Cell(1, 0, start));
        _frontier.Add(new Cell(0, 1, start));

        Cell _child, _gc;

        while (_frontier.Count > 0)
        {
            // 리스트 내 랜덤 좌표를 자식으로 정의 및 _frontier 리스트에서 제거
            int rand_p = Random.Range(0, _frontier.Count);
            _child = _frontier[rand_p];
            _frontier.RemoveAt(rand_p);

            // 선택된 자식의 자식 Cell을 가져와 좌표를 _r, _c 변수로 정의
            _gc = _child.GetChild();
            int _r = _gc.Row;
            int _c = _gc.Col;

            // 선택된 자식 Cell의 좌표가 벽인지, 방문한 적 없을 경우
            if (PreMaze.IsValid(_r, _c) && PreMaze.At(_r, _c).Equals(-1))
            {
                PreMaze.At(_child.Row, _child.Col) = 0;
                PreMaze.At(_r, _c) = 2;

                List<Cell> _neighbors = PreMaze.GetNeighbors(_gc, Maze.CELL_T.WALL);
                foreach (Cell _cell in _neighbors)
                    _frontier.Add(_cell);

                PreMaze.At(_r, _c) = 0;
            }
        }

        PreMaze.At(Rows - 1, Cols - 1) = 2;
    }
}
