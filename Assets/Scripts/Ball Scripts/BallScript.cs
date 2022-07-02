using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody myBody;
    [HideInInspector]
    public bool rollLeft;

    public float speed = 4f;

    private bool gameFinished = false;


    private bool gameStarted = false;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        rollLeft = true;
        
    }


    void Update()
    {
        if (gameStarted) {
            
            CheckInput();
            CheckBallOutOfBounds();
        }
        if (gameFinished) {
            StopBall();
        }
        Debug.Log("GamePlaying : " + GamePlayController.instance.gamePlaying);
        
        
    }
    void StopBall() {
        myBody.velocity = Vector3.zero;
        myBody.angularVelocity = Vector3.zero;
        
    }
    public void StartGame() {
        gameStarted = true;
    }
    public void GameFinished() {
        gameFinished = true;
    }
    
     void FixedUpdate()
    {

        if (gameStarted)
        {

            if (GamePlayController.instance.gamePlaying)
            {

                if (rollLeft)
                {
                    myBody.velocity = new Vector3(-speed, Physics.gravity.y, 0f);
                }
                else
                {
                    myBody.velocity = new Vector3(0f, Physics.gravity.y, +speed);
                }
            }
        }
        
        
       
        

    }
    void CheckBallOutOfBounds() {
        if (GamePlayController.instance.gamePlaying) {
            if (transform.position.y < -4f) {
                GamePlayController.instance.gamePlaying = false;
                GamePlayController.instance.FailedLvl();
                
            }
        }
    }
    void CheckInput() {
            
            if (!GamePlayController.instance.gamePlaying) {
                GamePlayController.instance.gamePlaying = true;
                //GamePlayController.instance.ActiveTileSpawner();
            }
        
            if (GamePlayController.instance.gamePlaying) {
              if (Input.GetMouseButtonDown(0)) {
                rollLeft = !rollLeft;
            }
        }


    }

    
}//class








