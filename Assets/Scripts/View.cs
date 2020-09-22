using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour {
    public Vector2 startPos;

    private const float BLOCK_SIZE = 0.32f;

    protected List<GameObject> CreateBlocks(List<Cell> blocks, GameObject block) {
        return blocks.Select(c => CreateBlock(c, block)).ToList();
    }

    protected GameObject CreateBlock(Cell c, GameObject block) {
        return Instantiate(block, GetObjectPos(c), Quaternion.identity);
    }

    protected Vector2 GetObjectPos(Cell pos) {
        var relPos = new Vector2(pos.Column * BLOCK_SIZE, -pos.Row * BLOCK_SIZE);
        return relPos + startPos;
    }
}
