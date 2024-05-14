using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public LogicScript logic;
    public GameObject bird;

    public bool birdIsAlive = true;
    public float flapStrength;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        bird = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (
            DetectPlayerInput() == true && birdIsAlive
        )
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        } else if (bird.transform.position.y <= logic.bottomBoundary)
        {
            Die();
        }

    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private bool DetectPlayerInput ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        } else
        {
            foreach (var touch in Touch.activeTouches)
            {
                // Only respond to first finger
                if (touch.isInProgress)
                {
                    return true;
                }
            }
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void Die()
    {
        logic.gameOver();
        birdIsAlive = false;
    }

}
