using System.Collections;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// libraries like Java, used to access collections or tools in code

public class Player : MonoBehaviour
{
    //data types (int,float,bool,string)
    [SerializeField] // allows to control from inspector
    private float _speed = 10.0f;  // controls speed of player
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _lives = 3;
    private float _laserShotFireRate = 0.5f;  // variable represents the delay before firing
    private float _fireReady = -1f;


    void Start() //called when game starts

    {
        //take current position = new positiong (0,0,0)
        transform.position = new Vector3(0, 0, 0);



    }

    // Update is called once per frame
    void Update()
    // game loop that runs at 60 frames per second
    {

        playerMovement();

        // if space key is pressed, and Time has passed is greater than _fire ready laser authorized to shoot

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _fireReady)
        {
            fireLaserShot();
        }
        else if (Time.time < _fireReady)
        {
            //Debug.Log("Cooldown has Started");
            //Debug.Log("Time is : " + Time.time + "and _fireReady is: " + _fireReady);
        }
        else if (Time.time > _fireReady)
        {
            //Debug.Log("Fire Ready");

        }


    }

    void playerMovement()
    {

        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        // Time Delta is real time movement * the speed * Vector Direction

        Vector3 direction = new Vector3(horizontal_input, vertical_input, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // bound restriction 

        switch (transform.position.y) // evaluates position in y axis or top/bottom
        {
            case >= 0:
                transform.position = new Vector3(transform.position.x, 0, 0); //stays on 0
                break;
            case <= -3.74f:
                transform.position = new Vector3(transform.position.x, -3.74f, 0); //stays on -3.4
                break;
        }
        switch (transform.position.x) // evaluates position on x axis or left/right
        {
            case >= 11:
                transform.position = new Vector3(-11, transform.position.y, 0); // spawns left
                break;
            case <= -11:
                transform.position = new Vector3(11, transform.position.y, 0);  // spawns right
                break;
        }

    }

    void fireLaserShot()
    {
        // fire ready =  1 second + 0.5 offset for the value set for fire rate which will be 1.5 seconds
        // because the update method keeps running there will be an increase in time while the variable still carries a value of 1.5
        // for example,  2 seconds will have passed by but fireready is storing 
        _fireReady = Time.time + _laserShotFireRate;
        Debug.Log("Space Key Pressed");
        Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), quaternion.identity);
    }
    public void Damage()
    {

        _lives = _lives - 1;
        Debug.Log("Damage Taken! Lives left: " + _lives);

        if (_lives < 1)
        {
            Debug.Log("Player Dead");
            Destroy(gameObject);
        }

    }
    public int lives()
    {

        return _lives;
    }
}
