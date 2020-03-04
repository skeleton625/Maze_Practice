public class HKCell : Cell
{
    private bool visited;
    public bool Visited
    {
        get => visited;
        set => visited = value;
    }
    private bool[] sideWall;
    public bool this[int index]
    {
        get => sideWall[index];
        set => sideWall[index] = value;
    }

    public HKCell(int _row, int _col)
    {
        this.row = _row;
        this.col = _col;
        this.visited = false;
        this.sideWall = new bool[4] { true, true, true, true};
    }
}
