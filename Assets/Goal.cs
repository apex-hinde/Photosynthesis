using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Leaf")
        {
            // Handle the leaf reaching the goal
            Debug.Log("Leaf reached the goal!");
            // You can add more logic here, like updating the score or removing the leaf
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
