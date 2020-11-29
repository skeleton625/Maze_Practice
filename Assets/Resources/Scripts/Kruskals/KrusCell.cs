using System.Collections;
using System.Collections.Generic;
public class KrusCell : Cell
{
    public KrusCell RightCell = null;
    public KrusCell LowerCell = null;

    public KrusCell(int r, int c)
    {
        row = r;
        col = c;
    }
}
