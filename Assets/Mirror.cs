
using UnityEngine;
using UnityEngine.EventSystems;


public class Mirror : MonoBehaviour
{
    private enum Rotation{
        GoingUp = -45,
        GoingDown = 45
    }
    public int rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotation = 45;
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if(Input.GetMouseButtonDown(0))
        {   
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            mousePos.z = 0;
            Quaternion rotation = Quaternion.Euler(0, 0, 45); 
            this.rotation = (int)Rotation.GoingDown;
            GameObject cloned = Instantiate(gameObject, mousePos, rotation);
            cloned.GetComponent<Mirror>().rotation = (int)Rotation.GoingDown;        }
        if(Input.GetMouseButtonDown(1))
        {   
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            mousePos.z = 0;
            Quaternion rotation = Quaternion.Euler(0, 0, -45); 

            GameObject cloned = Instantiate(gameObject, mousePos, rotation);
            cloned.GetComponent<Mirror>().rotation = (int)Rotation.GoingUp;
            }
        */
    }
    public void Rotate(){
                                transform.Rotate(0, 0, rotation*(-2));
                        rotation *= -1;
    }

}
