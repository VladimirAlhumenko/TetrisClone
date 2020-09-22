using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    private readonly List<int[,]> molds;
    private int moldIndex;

    public bool Falling = false;
    public float Speed = 1;

    public Cell Pos { get; set; }

    public Predicate<Cell> MovementRestrict;

    public Shape(params int[][,] molds)
    {
        this.molds = new List<int[,]>();

        foreach (var m in molds)
        {
            this.molds.Add(m);
        }
    }

    public bool Move(Cell delta)
    {
        var canMove = CanMove(delta);

        if (canMove)
        {
            Pos = GetTargetPos(delta);
        }

        return canMove;
    }

    private Cell GetTargetPos(Cell delta)
    {
        return Pos + delta;
    }

    private bool CanMove(Cell delta)
    {
        return GetAffectedBlocks(GetTargetPos(delta)).TrueForAll(MovementRestrict);
    }

    public bool Rotate()
    {
        var targetPos = FindValidRotationPos(GetNextMoldIndex());
        var canRotate = targetPos != null;

        if (canRotate)
        {
            SwitchNextMold();
            Pos = targetPos;
        }

        return canRotate;
    }

    private Cell FindValidRotationPos(int mIndex)
    {
        var deltas = new[]
        {
            new Cell(0, 0),
            new Cell(0, 1),
            new Cell(0, -1),
            new Cell(1, 0),
            new Cell(-1, 0)
        };

        foreach (var delta in deltas)
        {
            if (GetAffectedBlocks(delta + Pos, mIndex).TrueForAll(MovementRestrict))
            {
                return delta + Pos;
            }
        }

        return null;
    }

    public bool Drop()
    {
        Pos = GetMostDownPos();
        return true;
    }

    private Cell GetMostDownPos()
    {
        int row = Pos.Row;
        int column = Pos.Column;

        while (true)
        {
            var currentCells = GetAffectedBlocks(new Cell(row, column));
            var nextCells = GetAffectedBlocks(new Cell(row + 1, column));

            if (!nextCells.TrueForAll(MovementRestrict) || !currentCells.TrueForAll(MovementRestrict))
            {
                return new Cell(row, column);
            }

            row += 1;
        }
    }


    public List<Cell> GetCurrentBlocks()
    {
        return GetAffectedBlocks(Pos, moldIndex);
    }

    private List<Cell> GetAffectedBlocks(Cell pos)
    {
        return GetAffectedBlocks(pos, -1);
    }

    private List<Cell> GetAffectedBlocks(Cell pos, int moldIndex)
    {
        var actualPos = pos ?? Pos;
        var actualIndex = moldIndex != -1 ? moldIndex : this.moldIndex;

        var mold = molds[actualIndex];
        var result = new List<Cell>();

        for (int row = 0; row < mold.GetLength(0); row += 1)
        {
            for (int column = 0; column < mold.GetLength(1); column += 1)
            {
                var hasBlock = (mold[row, column] == 1);

                if (hasBlock)
                {
                    var block = new Cell(actualPos.Row + row, actualPos.Column + column);
                    result.Add(block);
                }
            }
        }

        return result;
    }

    private void SwitchNextMold()
    {
        moldIndex = GetNextMoldIndex();
    }

    private int GetNextMoldIndex()
    {
        return moldIndex + 1 < molds.Count ? moldIndex + 1 : 0;
    }

}

public enum ShapeType
{
    Symmetric,
    Stick,
    Square,
    L,
    S,
    Random
}
