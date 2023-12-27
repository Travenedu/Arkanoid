using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int value;
    public bool isStrong;
    private int hitsLeft = 0;
    private int STRONG_HITS = 2;

    private void Start(){ 
        
        if (isStrong){

            // Set the higher hit value.
            hitsLeft = STRONG_HITS;
            }
    } 

    private void OnCollisionEnter(Collision collision) {

        // If ball collides with a brick. 
        if (collision.gameObject.tag == "Ball"){

            if(hitsLeft > 0){

                hitsLeft--; // Decrement the hits.
            } else{
                
                GameManager.S.BrickDestroyed(value);
                Destroy(this.gameObject);
            }

        }
    }

}
