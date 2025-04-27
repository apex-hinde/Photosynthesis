using System;
using UnityEngine;

public class light_ray : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.AddComponent<LineRenderer>();

        RenderRay();
    }

    void RenderRay(){
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        int lineCount = 0;
        bool isWall = false;
        Vector2 lightDir = transform.right;
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2; // Set the number of points in the line renderer
        lineRenderer.SetPosition(lineCount, transform.position);
        lineCount++;
        Ray2D ray = new Ray2D(currentPos, lightDir);
        while(!isWall && lineCount<10){

            Vector2 RayDirectionSmall = lightDir * 0.2f;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin+RayDirectionSmall, lightDir, 1000);
            if(hit.collider.gameObject.tag == "Wall"){
                isWall = true;
                lineRenderer.SetPosition(lineCount, new Vector3(hit.point.x, hit.point.y, 0));
                break;
            }
            else if(hit.collider.gameObject.tag == "Mirror"){
                lineRenderer.positionCount+=1;
                lineRenderer.SetPosition(lineCount, new Vector3(hit.point.x, hit.point.y, 0));
                lineCount++;
                
                Vector2 reflectedDirection = GetReflectedDirection(ray.direction, hit.collider.gameObject.GetComponent<Mirror>().rotation);

                ray = new Ray2D(hit.point, reflectedDirection);
                lightDir = reflectedDirection;
            }
            else if(hit.collider.gameObject.tag == "Leaves"){

                isWall = true;
                lineRenderer.SetPosition(lineCount, new Vector3(hit.point.x, hit.point.y, 0));
                Vector2 leafPos = hit.collider.gameObject.transform.position;
                Vector2Int leafGridPos = new Vector2Int((int)Math.Round(leafPos.x, 0, MidpointRounding.AwayFromZero), (int)Math.Round(leafPos.y, 0, MidpointRounding.AwayFromZero));
                GameManager gameManager = FindFirstObjectByType<GameManager>();
                gameManager.LightGrow(leafGridPos);

                break;
            }
            else{
                break;
            }

        }



    }
    
    Vector2 GetReflectedDirection(Vector2 RayDirection, int MirrorRotation){
        if(MirrorRotation == 45){
            if(RayDirection.x == 1 && RayDirection.y == 0){
                return new Vector2(0, -1);
            }
            else if(RayDirection.x == 0 && RayDirection.y == -1){
                return new Vector2(1, 0);
            }
            else if(RayDirection.x == -1 && RayDirection.y == 0){
                return new Vector2(0, 1);
            }
            else if(RayDirection.x == 0 && RayDirection.y == 1){
                return new Vector2(-1, 0);
            }
            return RayDirection;
        }
        if(MirrorRotation == -45){
            if(RayDirection.x == 1 && RayDirection.y == 0){
                return new Vector2(0, 1);
            }
            else if(RayDirection.x == 0 && RayDirection.y == -1){
                return new Vector2(-1, 0);
            }
            else if(RayDirection.x == -1 && RayDirection.y == 0){
                return new Vector2(0, -1);
            }
            else if(RayDirection.x == 0 && RayDirection.y == 1){
                return new Vector2(1, 0);
            }
            return RayDirection;
        }
    return RayDirection;

    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            RenderRay();
        }
    }
    }

