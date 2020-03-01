
public class Cell
{
    private int row, col;
    public int Row
    { get => row; }
    public int Col
    { get => col; }
    private Cell Parent;

    public Cell(int _row, int _col, Cell _parent)
    {
        this.row = _row;
        this.col = _col;
        this.Parent = _parent;
    }

    public Cell GetChild()
    {
        return new Cell((Row << 1) - Parent.Row, (Col << 1) - Parent.Col, null);
    }

    public override bool Equals(object obj)
    {
        Cell _obj = (Cell)obj;
        return (Row.Equals(_obj.Row) && Col.Equals(_obj.Col));
    }
}
