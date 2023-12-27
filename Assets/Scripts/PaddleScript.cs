using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    public float speed;
    public float offset; // How manny unit can we move from the center.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.currentState == GameState.playing){
            float axisValue = Input.GetAxisRaw("Horizontal");

            // Get our position.
            Vector3 currentPosition = transform.position;

            // Change to respond to inputs.
            currentPosition.x += (speed * Time.deltaTime * axisValue);

            // Check the result
            currentPosition.x = Mathf.Clamp(currentPosition.x, -offset, offset);   

            // Update the position
            transform.position = currentPosition;
        }
    }
}
