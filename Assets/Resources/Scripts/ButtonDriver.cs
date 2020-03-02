using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDriver : MonoBehaviour
{
    private GeneratorDriver GD;

    private void Start()
    {
        GD = GeneratorDriver.instance;
    }

    public void OnClickMazeAlgorithm(int _num)
    {
        GD.ChooseGeneratingMaze(_num);
    }
}
