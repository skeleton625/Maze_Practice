using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKMaze : Maze<HKCell, HKCell>
{
    public HKMaze(int _row, int _col)
        : base(_row, _col)
    {
        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
                Grid[i, j] = new HKCell(i, j);
        }
    }

    public override List<HKCell> GetNeighbors(HKCell _cell, bool _wall)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsMoveable(int r, int c)
    {
        int _nr, _nc;
        for(int i = 0; i < 4; i++)
        {
            _nr = r + offset[i, 0];
            _nc = c + offset[i, 1];
            if (IsValid(_nr, _nc) && !Grid[_nr, _nc].Visited)
                return true;
        }
        return false;
    }

    public void DestroyAdjacentWall(int r, int c)
    {
        int _nr, _nc;
        for(int i = 0; i < 4; i ++)
        {
            _nr = r + offset[i, 0];
            _nc = c + offset[i, 1];
            if(IsValid(_nr, _nc) && Grid[_nr, _nc].Visited)
            {
                Grid[r, c][i] = false;
                Grid[_nr, _nc][4 + i] = false;
                break;
            }
        }
    }
}
