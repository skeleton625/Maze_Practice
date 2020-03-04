
public class PrimCell : Cell
{
    private PrimCell Parent;

    public PrimCell(int _row, int _col, PrimCell _parent)
    {
        this.row = _row;
        this.col = _col;
        this.Parent = _parent;
    }

    public PrimCell GetChild()
    {
        return new PrimCell((Row << 1) - Parent.Row, (Col << 1) - Parent.Col, null);
    }

    public override bool Equals(object obj)
    {
        PrimCell _obj = (PrimCell)obj;
        return (Row.Equals(_obj.Row) && Col.Equals(_obj.Col));
    }
}
