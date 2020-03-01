using System.Collections;
using System.Collections.Generic;

public class Maze
{
    public enum CELL_T{ WALL, FLOOR };

    private int Rows, Cols;
    private Cell Start, End;
    private char[,] Grid;
    private int[,] offset;

    public Maze(int _row, int _col)
    {
        // 행, 열 길이를 홀수로 정의
        Rows = (_row % 2) == 1 ? _row : _row - 1;
        Cols = (_col % 2) == 1 ? _col : _col - 1;
        Grid = new char[Rows, Cols];

        // 2차원 배열(리스트)를 모두 벽 Type으로 초기화
        for(int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
                Grid[i, j] = '#';
        }

        Start = End = null;
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
            _nrow = _cell.Row;
            _ncol = _cell.Col;

            bool validFloor = (_type.Equals(CELL_T.FLOOR) && IsPathable(_nrow, _ncol));
            bool validWall = (_type.Equals(CELL_T.WALL) && IsWall(_nrow, _ncol));
            if (validFloor || validWall)
                _neighbors.Add(new Cell(_nrow, _ncol, _cell));
        }

        // 찾은 이웃 Cell 리스트를 반환
        return _neighbors;
    }

    public ref char At(int r, int c)
    {
        return ref Grid[r, c];
    }

    public bool IsPathable(int r, int c)
    {
        return (IsValid(r, c) && !(Grid[r, c].Equals('#') || Grid[r, c].Equals('.')));
    }

    public bool IsValid(int r, int c)
    {
        return r >= 0 && r < Rows && c >= 0 && c < Cols;
    }

    public bool IsWall(int r, int c)
    {
        return IsValid(r, c) && Grid[r, c].Equals('#');
    }
}
