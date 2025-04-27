using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mirror;
    public GameObject leaf;
    public GameObject goal;
    public Vector2Int goalPos;
    public List<Vector2Int> leaves = new();
    public Grid grid = new();
    private Vector2 gridSize = new(1,1); // Size of each grid cell
    void Start()
    {
        grid.goalPos = goalPos;
        GameObject leafObj = Instantiate(leaf, new Vector2(0,-9), Quaternion.identity);
        leaves.Add(new Vector2Int(0,-9));
        grid.AddCell(new Vector2Int(0,-9), new Cell(leafObj));
        GameObject goalObj = Instantiate(goal, new Vector2(goalPos.x, goalPos.y), Quaternion.identity);
        grid.AddCell(goalPos, new Cell(goalObj));

        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            if(Input.GetKey(KeyCode.LeftShift)){
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                mousePos.z = 0;
                if(grid.CellExists(new Vector2Int((int)Math.Round(mousePos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(mousePos.y, 0, MidpointRounding.AwayFromZero)))){
                    Debug.Log("Cell already exists at this position.");
                    return;
                }
                Quaternion rotation = Quaternion.Euler(0, 0, -45); 
                GameObject obj = Instantiate(mirror, mousePos, rotation);
                Mirror mirrorObj = obj.GetComponent<Mirror>();
                mirrorObj.rotation = -45; 
                Vector2 gridPosition = new(Mathf.Round(mirrorObj.transform.position.x / gridSize.x),Mathf.Round(mirrorObj.transform.position.y / gridSize.y));
                Vector2 worldPosition = new(gridPosition.x * gridSize.x, gridPosition.y * gridSize.y);
                mirrorObj.transform.position = new Vector3(worldPosition.x, worldPosition.y, mirrorObj.transform.position.z);
                // Add the mirror to the grid
                grid.AddCell(new Vector2Int((int)gridPosition.x, (int)gridPosition.y), new Cell(obj));
                
            }
            else{
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                mousePos.z = 0;
                if(grid.CellExists(new Vector2Int((int)Math.Round(mousePos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(mousePos.y, 0, MidpointRounding.AwayFromZero)))){
                    Debug.Log("Cell already exists at this position.");
                    return;
                }
                Quaternion rotation = Quaternion.Euler(0, 0, 45); 
                GameObject obj = Instantiate(mirror, mousePos, rotation);
                Mirror mirrorObj = obj.GetComponent<Mirror>();
                mirrorObj.rotation = 45;
                Vector2 gridPosition = new Vector2(Mathf.Round(mirrorObj.transform.position.x / gridSize.x),Mathf.Round(mirrorObj.transform.position.y / gridSize.y));
                Vector2 worldPosition = new Vector2(gridPosition.x * gridSize.x, gridPosition.y * gridSize.y);
                mirrorObj.transform.position = new Vector3(worldPosition.x, worldPosition.y, mirrorObj.transform.position.z);

                grid.AddCell(new Vector2Int((int)Math.Round(worldPosition.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(worldPosition.y, 0, MidpointRounding.AwayFromZero)), new Cell(obj));
            }
        }
        if(Input.GetMouseButtonDown(1)){
         Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                mousePos.z = 0;
                if(grid.CellExists(new Vector2Int((int)Math.Round(mousePos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(mousePos.y, 0, MidpointRounding.AwayFromZero)))){
                    if(grid.GetCell(new Vector2Int((int)Math.Round(mousePos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(mousePos.y, 0, MidpointRounding.AwayFromZero))).CompareTag("Mirror")){
                        grid.RemoveCell(new Vector2Int((int)Math.Round(mousePos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(mousePos.y, 0, MidpointRounding.AwayFromZero)));
                    }
                    else{
                        Debug.Log("you can only delete Mirrors silly.");
                    }
                }
        }
        if(Input.GetKeyDown(KeyCode.R)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject cell = grid.GetCell(new Vector2Int((int)Math.Round(mousePos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(mousePos.y, 0, MidpointRounding.AwayFromZero)));
            if(cell is null){
                Debug.Log("No cell found at this position.");
            }
            else if(cell.CompareTag("Mirror")){
                cell.GetComponent<Mirror>().Rotate();
            }    
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            NaturalGrow();
        }

    }
    
    public void LightGrow(Vector2 position){

            List<Vector2Int> adjacentCells = GetAdjacentCells(new Vector2Int((int)Math.Round(position.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(position.y, 0, MidpointRounding.AwayFromZero)));
            foreach(Vector2Int cellCoord in adjacentCells){
                GameObject Obj = Instantiate(leaf, new Vector2(cellCoord.x, cellCoord.y), Quaternion.identity);
                leaves.Add(cellCoord);
                grid.AddCell(cellCoord, new Cell(Obj));
            }
    }
    private void NaturalGrow(){
        List<Vector2Int> localLeaves = leaves;
        
        System.Random rnd = new System.Random();
        foreach(Vector2Int leafPos in localLeaves){
            if(rnd.Next(0, 11) > 9){
                LightGrow(leafPos);
            }
            }
        List<Vector2Int> localLeaves2 = leaves;

        foreach(Vector2Int leafPos in localLeaves2){
            Leaves leafObj = grid.GetCell(leafPos).GetComponent<Leaves>();
            if(rnd.Next(0, 11) > 1){
                leafObj.leafHealth--;
                if(leafObj.leafHealth == 1){
                    Renderer renderer = leafObj.GetComponent<Renderer>();
                    renderer.material.color = Color.brown;
                }
                if(leafObj.leafHealth <= 0){
                    leaves.Remove(leafPos);
                    grid.RemoveCell(leafPos);
                }
            }
        }

    }
    
    
    

    private List<Vector2Int> GetAdjacentCells(Vector2Int position){
        System.Random rnd = new System.Random();
        List<Vector2Int> adjacentCells = new List<Vector2Int>();
        if(grid.CellExists(new Vector2Int(position.x+1, position.y))){
            if(grid.GetCell(new Vector2Int(position.x+1, position.y)).CompareTag("Goal")){
                Debug.Log("Leaf reached the goal!");
            }
        }
        else if(!grid.CellExists(new Vector2Int(position.x+1, position.y)) && (rnd.Next(0, 11) > 5)){
            adjacentCells.Add(new Vector2Int(position.x+1, position.y));
        }
        if(grid.CellExists(new Vector2Int(position.x-1, position.y))){
            if(grid.GetCell(new Vector2Int(position.x-1, position.y)).CompareTag("Goal")){
                Debug.Log("Leaf reached the goal!");
            }
        }
        else if(!grid.CellExists(new Vector2Int(position.x-1, position.y)) && (rnd.Next(0, 11) > 5)){
            adjacentCells.Add(new Vector2Int(position.x-1, position.y));
        }
        if(grid.CellExists(new Vector2Int(position.x, position.y+1))){
            if(grid.GetCell(new Vector2Int(position.x, position.y+1)).CompareTag("Goal")){
                Debug.Log("Leaf reached the goal!");
            }
        }
        else if(!grid.CellExists(new Vector2Int(position.x, position.y+1)) && (rnd.Next(0, 11) > 5)){
            adjacentCells.Add(new Vector2Int(position.x, position.y+1));
        }
        if(grid.CellExists(new Vector2Int(position.x, position.y-1))){
            if(grid.GetCell(new Vector2Int(position.x, position.y-1)).CompareTag("Goal")){
                Debug.Log("Leaf reached the goal!");
            }
        }
        else if(!grid.CellExists(new Vector2Int(position.x, position.y-1)) && (rnd.Next(0, 11) > 5)){
            adjacentCells.Add(new Vector2Int(position.x, position.y-1));
        }
        return adjacentCells;
    }


}
