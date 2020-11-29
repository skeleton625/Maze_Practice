using System.Collections.Generic;

public class PrimMaze : Maze<char, PrimCell>
{
    public PrimMaze(int _row, int _col) :
        base(_row, _col)
    {
        // 2차원 배열(리스트)를 모두 벽 Type으로 초기화
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                Grid[i, j] = '#';
        }
    }

    public List<PrimCell> GetNeighbors(PrimCell _cell, bool _wall)
    {
        int _nrow, _ncol;
        // 상하좌우 이동 배열
        int[,] offset = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
        // 매개변수 _PrimCell 주변 이웃 리스트 정의
        List<PrimCell> _neighbors = new List<PrimCell>();

        // 주변의 이웃 PrimCell들을 찾음
        for (int i = 0; i < 4; i++)
        {
            _nrow = _cell.Row + offset[i, 0];
            _ncol = _cell.Col + offset[i, 1];

            if (IsMoveable(_nrow, _ncol))
                _neighbors.Add(new PrimCell(_nrow, _ncol, _cell));
        }

        // 찾은 이웃 PrimCell 리스트를 반환
        return _neighbors;
    }

    public override bool IsMoveable(int r, int c)
    {
        return IsValid(r, c) && Grid[r, c].Equals('#');
    }
}
