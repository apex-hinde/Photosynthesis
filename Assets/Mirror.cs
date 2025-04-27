
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

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Rotate(){
        transform.Rotate(0, 0, rotation*(-2));
        rotation *= -1;
    }

}
