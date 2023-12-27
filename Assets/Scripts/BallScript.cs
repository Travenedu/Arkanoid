using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision) {
        
        // When the ball hits something, make a noise
        SoundManager.S.PlayCollisionSound(collision.transform.tag);
        
        // If ball collides with a deadwall. 
        if ((collision.gameObject.tag == "DeadWall") && (GameManager.S.currentState == GameState.playing)){

            // Tell game manager we hit the wall
            GameManager.S.BallOutOfPlay();
        }
    }
}
