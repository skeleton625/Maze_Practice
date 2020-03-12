﻿using System.Collections.Generic;
public abstract class Maze<T, U>
{
    protected int[,] offset;
    protected int Rows, Cols;
    protected T[,] Grid;

    public Maze(int _row, int _col)
    {
        Rows = (_row % 2) == 1 ? _row : _row - 1;
        Cols = (_col % 2) == 1 ? _col : _col - 1;
        // 행, 열 길이를 홀수로 정의
        Grid = new T[Rows, Cols];
        offset = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
    }

    public bool IsValid(int r, int c)
    {
        return r >= 0 && r < Rows && c >= 0 && c < Cols;
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
    public abstract List<U> GetNeighbors(U _cell, bool _wall);
}