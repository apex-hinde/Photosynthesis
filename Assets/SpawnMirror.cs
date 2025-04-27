using UnityEngine;

public class SpawnMirror : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mirror;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(Input.GetKey(KeyCode.LeftShift)){
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                mousePos.z = 0;
                Quaternion rotation = Quaternion.Euler(0, 0, -45); 
                Instantiate(mirror, mousePos, rotation).GetComponent<Mirror>().rotation = -45; 
            }
            else{
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                mousePos.z = 0;
                Quaternion rotation = Quaternion.Euler(0, 0, 45); 
                Instantiate(mirror, mousePos, rotation).GetComponent<Mirror>().rotation = 45; 
            }
    }
            if(Input.GetKeyDown(KeyCode.R)){
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                    if(hit.collider != null && hit.collider.gameObject.CompareTag("Mirror"))
                    {
                        hit.collider.gameObject.GetComponent<Mirror>().Rotate();
                    }
        }

    }
}


