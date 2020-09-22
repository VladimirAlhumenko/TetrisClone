using System.Collections;
using System.Collections.Generic;

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}", Row, Column);
    }

    public static Cell operator +(Cell left, Cell right)
    {
        return new Cell(left.Row + right.Row, left.Column + right.Column);
    }
}
