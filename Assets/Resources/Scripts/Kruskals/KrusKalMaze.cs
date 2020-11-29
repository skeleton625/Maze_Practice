using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KruskalMaze : Maze<KrusCell, KrusCell>
{
    public KruskalMaze(int _row, int _col)
    : base(_row, _col)
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Grid[i, j] = new KrusCell(i, j);
                if (IsValid(i, j + 1)) Grid[i, j].RightCell = Grid[i, j + 1];
                if (IsValid(i + 1, j)) Grid[i, j].LowerCell = Grid[i + 1, j];
            }
        }
    }

    public override bool IsMoveable(int r, int c)
    {
        throw new System.NotImplementedException();
    }
}
