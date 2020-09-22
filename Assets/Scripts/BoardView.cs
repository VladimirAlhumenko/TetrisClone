using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : View
{
    public int Rows;
    public int Columns;
    public Vector2 Position;

    [SerializeField]
    private GameObject _block;

    [SerializeField]
    public GameObject _grid;

    private List<GameObject> staticBlocks = new List<GameObject>();

    private void Start()
    {
        for (int row = 0; row < Rows; row += 1)
        {
            for (int column = 0; column < Columns; column += 1)
            {
                CreateBlock(new Cell(row, column), _grid);
            }
        }
    }

    public void PutBlocks(List<Cell> blocks)
    {
        staticBlocks.AddRange(CreateBlocks(blocks, _block));
    }

    public void EraseBlocks()
    {
        staticBlocks.ForEach(Destroy);
        staticBlocks.Clear();
    }

}
