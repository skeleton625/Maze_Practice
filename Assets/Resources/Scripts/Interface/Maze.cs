using System.Collections.Generic;
public abstract class Maze<T, U>
{
    protected int rows, cols;
    protected T[,] Grid;

    public int Rows { get => rows; }
    public int Cols { get => cols; }

    public Maze(int _row, int _col)
    {
        rows = (_row % 2) == 1 ? _row : _row - 1;
        cols = (_col % 2) == 1 ? _col : _col - 1;
        // 행, 열 길이를 홀수로 정의
        Grid = new T[rows, cols];
    }

    public bool IsValid(int r, int c)
    {
        return r >= 0 && r < rows && c >= 0 && c < cols;
    }

    public ref T[,] GetMazeMap()
    {
        return ref Grid;
    }

    public ref T At(int r, int c)
    {
        return ref Grid[r, c];
    }

    public abstract bool IsMoveable(int r, int c);
}