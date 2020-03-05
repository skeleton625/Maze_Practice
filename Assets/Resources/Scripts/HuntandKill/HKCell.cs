public class HKCell : Cell
{
    private bool visited;
    public bool Visited
    {
        get => visited;
        set => visited = value;
    }
    private bool[] sideWall;
    public bool[] SidelWall
    { get => sideWall; }
    public bool this[int index]
    {
        get => sideWall[index];
        set
        {
            switch(index)
            {
                case 4:
                    index = 1;
                    break;
                case 5:
                    index = 0;
                    break;
                case 6:
                    index = 3;
                    break;
                case 7:
                    index = 2;
                    break;
            }
            sideWall[index] = value;
        }
    }

    public HKCell(int _row, int _col)
    {
        this.row = _row;
        this.col = _col;
        this.visited = false;
        this.sideWall = new bool[4] { true, true, true, true};
    }
}
