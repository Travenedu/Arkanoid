using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState{none, menu, getReady, playing, oops, gameOver}
public class GameManager : MonoBehaviour
{
    public static GameManager S;
    
    // Game state
    public GameState currentState = GameState.none;

    // ui elements
    public Text gameMessageText;
    public Text scoreText;
    
    // game parameters
    public float speed = 1.0f;
    private int score = 0;
    public Vector3 direction;
    public GameObject ballPrefab;
    public GameObject currentBall;
    private int livesRemaining;
    private int LIVES_AT_START = 3;

    public GameObject bricksPrefab;
    public GameObject currentBricks;

    public int BricksLeft;
    public bool _checkBricks;






    private void Awake(){

        if(S){
            
            Destroy(this.gameObject);
        } else {
            S = this; // Singleton definition.
        }
    }
    
    
    // Start is called before the first frame update

    void Start()
    {
        // Go to game.
        SetGameMenu();
    }

    private void SetGameMenu(){

        // These are the things that run when the game begins

        // Set the state to menu
        currentState = GameState.menu;

        // reset the game message
        gameMessageText.text = "Press \"S\" to Start Game";
        gameMessageText.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == GameState.menu){

            // Wait for the prompt to start the game.
            if(Input.GetKeyDown(KeyCode.S)){
                
                // Launch a new game.
                InitializeGame();
            }
        }

        if ((currentState == GameState.playing) && (_checkBricks)){
            checkRemainingBricks();
            _checkBricks = false;

    }
    }

    private void InitializeGame(){

        //reset the score text & value
        score = 0;
        scoreText.text = "000";

        // Reset the lives
        livesRemaining = LIVES_AT_START;

        // Go to the ready state(start of round)
        ReadyRound();

    }

    private void ReadyRound(){

        // Put us in the get ready state
        currentState = GameState.getReady;

        // Update the UI
        gameMessageText.text = "Get Ready!!!";
        gameMessageText.enabled = true;


        // Spawn the ball
        if(currentBall){Destroy(currentBall);}
        currentBall = Instantiate(ballPrefab);

        // Spawn bricks
        if(currentBricks){Destroy(currentBricks);}
            currentBricks = Instantiate(bricksPrefab);

        // Start the get ready delay.
        StartCoroutine(GetReadyDelay());

        // Start the round
        //StartRound();


    }


    private void StartRound(){

        // Turn off the message
        gameMessageText.enabled = false;

        // Get the velocity
        Vector3 ballVelocity = direction.normalized * speed;

        // push the ball
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        rb.velocity = ballVelocity;

        // Set the state to playing.
        currentState = GameState.playing;
    }

    public void BallOutOfPlay(){

        // This is called by the ball when it hits the backwall

        // Go to the oops state
        currentState = GameState.oops;

        // Stop the ball
        currentBall.GetComponent<Rigidbody>().isKinematic = true;

        // Reduce the number of lives left
        livesRemaining--;

        StartCoroutine(OopsState());

    }

    public IEnumerator OopsState(){

        // Set the message
        gameMessageText.enabled = true;
        gameMessageText.text = "You have " + livesRemaining + " lives left!";

        yield return new WaitForSeconds(2.0f);

               // Decide whether the game is over or not
        if (livesRemaining <= 0){
            
            // the game is over
            GameOver();

        } else {
            // Reset the game
            ReadyRound();
        }

    }

    private void GameOver(){
        // Change the gamestate
        currentState = GameState.gameOver;

        // Display the game over message
        gameMessageText.enabled = true;
        gameMessageText.text = "Game Over!!!";
    }

    public void BrickDestroyed(int scoreValue){
        
        // Increase the score.
        score += scoreValue;
        scoreText.text = score.ToString();

        // Queue a brick check
        _checkBricks = true;
    }

    private void checkRemainingBricks(){
        if(currentBricks.transform.childCount <= 0){
            GameOver();
        }
    
    }

    public IEnumerator GetReadyDelay(){

        Renderer ballRender = currentBall.GetComponent<Renderer>();

        for (int i = 0; i<3; i++){
            ballRender.enabled = false;
            gameMessageText.enabled = true;
            
            yield return new WaitForSeconds(0.5f);
            
            ballRender.enabled = true;
            gameMessageText.enabled = false;
            
            yield return new WaitForSeconds(0.5f);

        }

        StartRound();
    }
}
