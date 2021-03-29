using UnityEngine;
public class Node {
	
	public bool walkable;
	public Vector2 posInWorld;
    public Vector2Int posInGrid;
    public bool isChecked;
    public bool isChosen;
	public Node(bool walkable, Vector2 posInWorld , Vector2Int posInGrid) {
        isChosen = false;
        isChecked = false;
		this.walkable = walkable;
		this.posInWorld = posInWorld;
        this.posInGrid = posInGrid;
	}
}