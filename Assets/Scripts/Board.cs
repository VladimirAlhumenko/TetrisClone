using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Board {
    private int[,] field;
    private int rows;
    private int columns;

    public Board(int rows, int columns) {
        this.rows = rows;
        this.columns = columns;

        field = new int[rows, columns];
    }

    public List<Cell> GetBlocks() {
        var result = new List<Cell>();

        for (int row = 0; row < rows; row += 1) {
            for (int column = 0; column < columns; column += 1) {
                if (field[row, column] == 1) { result.Add(new Cell(row, column)); }
            }
        }

        return result;
    }

    public void PutBlocks(List<Cell> blocks) {
        blocks.ForEach(PutBlock);
    }

    public void PutBlock(Cell c) {
        if (InsideBoundary(c)) {
            field[c.Row, c.Column] = 1;
        }
    }

    public bool IsEmptyCell(Cell c) {
        return InsideBoundary(c) && !HasBlock(c);
    }

    private bool InsideBoundary(Cell c) {
        return 0 <= c.Row && c.Row < rows && 0 <= c.Column && c.Column < columns;
    }

    private bool HasBlock(Cell c) {
        return InsideBoundary(c) && field[c.Row, c.Column] == 1;
    }

    

    public bool IsOverflow() {
        for (var column = 0; column < columns; column += 1) {
            if (field[0, column] == 1) {
                return true;
            }
        }

        return false;
    }


    public bool CleanFullRows(out int removedCount) {
        removedCount = 0;
        var fullRows = GetFullRows();
        if (fullRows.Count == 0) { return false; }

        while (true) {
            removedCount += columns;
            ShiftPreviousRows(GetFullRows()[0]);
            if (GetFullRows().Count == 0) { break; }
        }

        return true;
    }

    private void ShiftPreviousRows(int row) {
        for (var current = row - 1; current >= 0; current -= 1) {
            ReplaceRow(current, current + 1);
        }
    }

    private void ReplaceRow(int sourceRow, int destinationRow) {
        for (int column = 0; column < columns; column += 1) {
            field[destinationRow, column] = field[sourceRow, column];
        }
    }

    private List<int> GetFullRows() {
        var result = new List<int>();

        for (int row = 0; row < rows; row += 1) {
            if (IsRowFull(row)) { result.Add(row); }
        }

        return result;
    }
	
    private bool IsRowFull(int row) {
        for (var column = 0; column < columns; column += 1) {
            if (field[row, column] != 1) { return false; }
        }

        return true;
    }
}
