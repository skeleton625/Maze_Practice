using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntandKill : MonoBehaviour, MazeGenerator
{
    private int Rows, Cols;
    private GameObject Block;
    private Transform CurrentField;
    private HKMaze Hmaze;
    private int[,] offset;

    public HuntandKill(int r, int c, GameObject _block, Transform _field, HKMaze _maze)
    {
        Rows = (r % 2) == 1 ? r : r - 1;
        Cols = (c % 2) == 1 ? c : c - 1;
        Hmaze = _maze;
        Block = _block;
        CurrentField = _field;
        offset = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
    }

    public void AlgorithmStart()
    {
        int _curR = 0, _curC = 0;
        while(true)
        {
            Walk(_curR, _curC);
            if (Hunt(ref _curR, ref _curC))
                break;
        }
    }

    private void Walk(int _r, int _c)
    {
        int _nr, _nc, dir;
        Hmaze.At(_r, _c).Visited = true;
        while (Hmaze.IsMoveable(_r, _c))
        {
            dir = Random.Range(0, 4);
            _nr = _r + offset[dir, 0];
            _nc = _c + offset[dir, 1];

            if (Hmaze.IsValid(_nr, _nc) && !Hmaze.At(_nr, _nc).Visited)
            {
                Hmaze.At(_r, _c)[dir] = false;
                Hmaze.At(_nr, _nc)[4+dir] = false;
                _r = _nr;
                _c = _nc;
            }
            Hmaze.At(_r, _c).Visited = true;
        }
    }

    private bool Hunt(ref int _r,ref int _c)
    {
        for(int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                if(!Hmaze.At(i,j).Visited && Hmaze.IsMoveable(i, j))
                {
                    _r = i;
                    _c = j;
                    Hmaze.DestroyAdjacentWall(i, j);
                    Hmaze.At(i, j).Visited = true;
                    return false;
                }
            }
        }
        return true;
    }

    public void GenerateMaze()
    {
        float _x = 6f, _z = 6f;
        GameObject _clone = null;
        HKCell[,] _grid = Hmaze.GetMazeMap();

        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
            {
                _clone = Instantiate(Block, CurrentField);
                _clone.transform.position = new Vector3(_x, 2, _z);
                _clone.GetComponent<BlockController>().SetBlockEnable(_grid[i, j].SidelWall);
                _x += 4f;
            }
            _x = 6;
            _z += 4f;
        }
        Destroy(_clone);
    }
}
