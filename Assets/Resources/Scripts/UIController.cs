using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuUI;
    [SerializeField]
    private InputField Row, Column;

    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public int GetInputFieldRow()
    {
        int _row;
        if (int.TryParse(Row.text, out _row))
            return _row;
        else
            return 0;
    }

    public int GetInputFieldCol()
    {
        int _col;
        if (int.TryParse(Row.text, out _col))
            return _col;
        else
            return 0;
    }
}
