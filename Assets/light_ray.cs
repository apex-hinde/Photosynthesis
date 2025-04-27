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
        bool isWall = true;
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
        while(isWall && lineCount<10){

            Vector2 RayDirectionSmall = lightDir * 0.2f;
            Debug.Log("hi");
            RaycastHit2D hit = Physics2D.Raycast(ray.origin+RayDirectionSmall, lightDir, 1000);
            if(hit.collider.gameObject.tag == "Wall"){
                Debug.Log("hi2");
                Debug.Log("Ray x: " + hit.point.x);
                Debug.Log("Ray y: " + hit.point.y);


                isWall = false;
                lineRenderer.SetPosition(lineCount, new Vector3(hit.point.x, hit.point.y, 0));
                break;
            }
            if(hit.collider.gameObject.tag == "Mirror"){
                lineRenderer.positionCount+=1;
                Debug.Log("hi3");
                lineRenderer.SetPosition(lineCount, new Vector3(hit.point.x, hit.point.y, 0));
                lineCount++;
                Debug.Log("ray direction: " + ray.direction);
                
                Vector2 reflectedDirection = GetReflectedDirection(ray.direction, hit.collider.gameObject.GetComponent<Mirror>().rotation);
                Debug.Log("reflected direction: " + reflectedDirection);

                ray = new Ray2D(hit.point, reflectedDirection);
                lightDir = reflectedDirection;
            }
            else{
                Debug.Log("hi4");
                break;
            }
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);

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

