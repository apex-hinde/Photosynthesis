using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grid{
    Dictionary<Vector2Int, Cell> cells = new Dictionary<Vector2Int, Cell>();
    public Vector2Int goalPos; // Size of each grid cell


    public void AddCell(Vector2Int position, Cell cell){
        if(!cells.ContainsKey(position)){
            cells.Add(position, cell);
        } 
        else {
            if(position == goalPos){
                Debug.Log("Leaf reached the goal!");

            }
            Debug.LogWarning("Cell already exists at this position.");
        }
    }
    public bool CellExists(Vector2Int position){
        return cells.ContainsKey(position);

        
    }
    public GameObject GetCell(Vector2Int position){
        if(cells.TryGetValue(position, out Cell cell)){
            return cell.obj;
        } else {
            Debug.LogWarning("No cell found at this position.");
            return null;
        }
    }
    public void RemoveCell(Vector2Int position){
        if(cells.ContainsKey(position)){
            cells.TryGetValue(position, out Cell cell);
            Object.Destroy(cell.obj);
            cells.Remove(position);
        } else {
            Debug.LogWarning("No cell found at this position to remove.");
        }
    }

}

public class Cell{
    public GameObject obj; 
    public Cell(GameObject obj){
        this.obj = obj;
    }
    

}