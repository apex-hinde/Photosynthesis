using System.Collections.Generic;
using UnityEngine;

public class LeavesManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject leaf;
    public List<Leaves> leaves = new List<Leaves>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Space)){
            foreach(Leaves leaf in leaves){
                Vector2 leafPos = leaf.transform.position;
                if(Random.Range(0, 10) > 2){
                    Destroy(leaf);
                    leaves.Remove(leaf);
                }
                else if(Random.Range(0, 10) > 1){
                    Grow(leafPos);
                }

            }
        }
    }
    public void Grow(Vector2 position){
            Vector2 posN = position+new Vector2(0, 0.5f);
            Vector2 posE = position+new Vector2(0.5f, 0);
            Vector2 posS = position+new Vector2(0, -0.5f);
            Vector2 posW = position+new Vector2(-0.5f, 0);
            RaycastHit2D hitN = Physics2D.Raycast(posN, Vector2.zero);

            if(hitN.collider == null && Random.Range(0, 10) > 5){
                GameObject leaf2 = Instantiate(leaf, posN, Quaternion.identity);
                leaves.Add(leaf2.GetComponent<Leaves>());
            }
            RaycastHit2D hitE = Physics2D.Raycast(posE, Vector2.zero);
            if(hitE.collider == null && Random.Range(0, 10) > 5){
                GameObject leaf2 = Instantiate(leaf, posE, Quaternion.identity);
                leaves.Add(leaf2.GetComponent<Leaves>());
                
            }
            RaycastHit2D hitS = Physics2D.Raycast(posS, Vector2.zero);
            if(hitS.collider == null && Random.Range(0, 10) > 5){
                GameObject leaf2 = Instantiate(leaf, posS, Quaternion.identity);
                leaves.Add(leaf2.GetComponent<Leaves>());

            }
            RaycastHit2D hitW = Physics2D.Raycast(posW, Vector2.zero);
            if(hitW.collider == null && Random.Range(0, 10) > 5){
                GameObject leaf2 = Instantiate(leaf, posW, Quaternion.identity);
                leaves.Add(leaf2.GetComponent<Leaves>());

            }
    }
}
