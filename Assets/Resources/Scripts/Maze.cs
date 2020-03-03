using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// '#' : -1, ' ' : 0, 'S' : 1, 'E' : 2, 'V' : 3
public class Maze
{
    public enum CELL_T{ WALL, FLOOR };

    private int Rows, Cols;
    private int[,] Grid;
    private int[,] offset;

    public Maze(int _row, int _col)
    {
        // 행, 열 길이를 홀수로 정의
        Rows = (_row % 2) == 1 ? _row : _row - 1;
        Cols = (_col % 2) == 1 ? _col : _col - 1;
        Grid = new int[Rows, Cols];

        // 2차원 배열(리스트)를 모두 벽 Type으로 초기화
        for(int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                Grid[i, j] = -1;
        }

        // 상하좌우 배열 이동 값 초기화
        offset = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
    }

    public List<Cell> GetNeighbors(Cell _cell, CELL_T _type)
    {
        int _nrow, _ncol;
        // 매개변수 _cell 주변 이웃 리스트 정의
        List<Cell> _neighbors = new List<Cell>();

        // 주변의 이웃 Cell들을 찾음
        for(int i = 0; i < 4; i++)
        {
            _nrow = _cell.Row + offset[i, 0];
            _ncol = _cell.Col + offset[i, 1];

            bool validWall = (_type.Equals(CELL_T.WALL) && IsWall(_nrow, _ncol));
            if (validWall)
                _neighbors.Add(new Cell(_nrow, _ncol, _cell));
        }

        // 찾은 이웃 Cell 리스트를 반환
        return _neighbors;
    }

    public int[,] GetMazeMap()
    {
        return Grid;
    }

    public ref int At(int r, int c)
    {
        return ref Grid[r, c];
    }

    public bool IsValid(int r, int c)
    {
        return r >= 0 && r < Rows && c >= 0 && c < Cols;
    }

    public bool IsWall(int r, int c)
    {
        return IsValid(r, c) && Grid[r, c].Equals(-1);
    }
}
