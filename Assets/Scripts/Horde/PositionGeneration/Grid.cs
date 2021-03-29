using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class Grid : MonoBehaviour
{
    public bool displayGridGizmos;
    private float nodeRaduis;
    private Node[,] grid;
    private Vector2Int gridSize;
    private LayerMask unwalkableMask;
    void CreateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];
        Vector2 worldBottomLeft = (Vector2)transform.position - (Vector2)gridSize * nodeRaduis;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 nodeCenterPosInWorld = new Vector2(((2 * x) + 1) * nodeRaduis, ((2 * y) + 1) * nodeRaduis) + worldBottomLeft;
                bool walkable = !Physics2D.OverlapCircle(nodeCenterPosInWorld, nodeRaduis, unwalkableMask);
                grid[x, y] = new Node(walkable, nodeCenterPosInWorld, new Vector2Int(x, y));
            }
        }
    }

    public void SetNodeAsChosen(Node node)
    {
        grid[node.posInGrid.x, node.posInGrid.y].isChosen = true;
    }

    public void UpdateGrid(Vector2Int gridSize, float nodeRaduis, LayerMask unwalkableMask)
    {
        this.gridSize = gridSize;
        this.nodeRaduis = nodeRaduis;
        this.unwalkableMask = unwalkableMask;
        CreateGrid();
    }

    public Node WorldPointToNode(Vector2 point)
    {
        float percentX = Mathf.Clamp01((point.x + nodeRaduis * gridSize.x) / (nodeRaduis * 2 * gridSize.x));
        float percentY = Mathf.Clamp01((point.y + nodeRaduis * gridSize.y) / (nodeRaduis * 2 * gridSize.y));
        return grid[Mathf.RoundToInt(percentX * (gridSize.x - 1)), Mathf.RoundToInt(percentY * (gridSize.y - 1))];

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector2)gridSize * nodeRaduis * 2f);
        if (grid != null && displayGridGizmos)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = node.walkable ? Color.red : Color.white;
                Gizmos.DrawWireCube(node.posInWorld, Vector2.one * nodeRaduis * 2f);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                Vector2 neighbourGridPos = node.posInGrid + new Vector2(x, y);
                if (neighbourGridPos.x >= 0 && neighbourGridPos.y >= 0 && neighbourGridPos.x < gridSize.x && neighbourGridPos.y < gridSize.y)
                {
                    neighbours.Add(grid[(int)neighbourGridPos.x, (int)neighbourGridPos.y]);
                }
            }
        }
        return neighbours;
    }

    public List<Vector2> GetChosenPositions()
    {
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                if (grid[i,j].isChosen)
                    positions.Add(grid[i,j].posInWorld);
            }
        }
        return positions;
    }
}
