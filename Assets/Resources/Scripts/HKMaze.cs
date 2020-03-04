using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKMaze : Maze<HKCell, HKCell>
{
    public HKMaze(int _row, int _col, HKCell _init)
        : base(_row, _col, _init) { }

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
}
