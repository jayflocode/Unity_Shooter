using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// libraries like Java, used to access collections or tools in code

public class Player : MonoBehaviour
{
    //data types (int,float,bool,string)

    [SerializeField] // allows to control from inspector
    private float _player_speed = 10.0f;  // controls speed of player
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private int _playerScore = 0;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private bool _speedPowerUpActive = false;
    [SerializeField]
    private bool _shieldPowerActive = false;
    [SerializeField]
    private int _lives = 3;
    private Spawn_Manager _spawnManager;
    [SerializeField]
    private float _laserShotFireRate = 0.5f;  // variable represents the delay before firing
    private float _fireReady = -1f;
    private float _fireOffset = 1.035f;



    void Start() //called when game starts

    {
        //take current position = new positiong (0,0,0)
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

        if (_spawnManager == null)
        {

            Debug.Log("Spawn_1 is Null");
        }

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
        transform.Translate(direction * _player_speed * Time.deltaTime);

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
            case >= 9.75f:
                transform.position = new Vector3(-9.75f, transform.position.y, 0); // spawns left
                break;
            case <= -9.75f:
                transform.position = new Vector3(9.75f, transform.position.y, 0);  // spawns right
                break;
        }

    }

    void fireLaserShot()
    {
        // fire ready =  1 second + 0.5 offset for the value set for fire rate which will be 1.5 seconds
        // because the update method keeps running there will be an increase in time while the variable still carries a value of 1.5
        // for example,  2 seconds will have passed by but fireready is storing 
        _fireReady = Time.time + _laserShotFireRate;
        // Debug.Log("Space Key Pressed");
        // Instantiates a laser with an offset in distance in respect to player


        switch (_tripleShotActive)
        {
            case false:
                // fire normal shot
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + _fireOffset, 0), quaternion.identity);
                break;
            case true:
                // fire triple shot
                Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y, 0), quaternion.identity);
                break;
        }

    }
    // switch for triple shot power up, passes bool value
    public void tripleShotSwitch(bool status)
    {
        _tripleShotActive = status;
        StartCoroutine(tripleShotpowerUpRoutine());
    }
    // swithc for speed power up, passes bool value
    public void speedPowerActive(bool status)
    {
        _speedPowerUpActive = status;
        StartCoroutine(speedPowerRoutine());

    }
    // switches shield power active 
    public void shieldPowerActive(bool status)
    {
        _shieldPowerActive = status;
        StartCoroutine(shieldPowerRoutine());
    }
    // triple shot coroutine 
    IEnumerator tripleShotpowerUpRoutine()
    {

        //start the time where power up was acquired 
        // after 10 seconds turn off power up
        while (_tripleShotActive == true)
        {
            Debug.Log("Start Triple Shot Routine");
            yield return new WaitForSeconds(7);
            _tripleShotActive = false;
            Debug.Log("End Triple Shot Routine");
        }
    }
    // speed power up routine
    IEnumerator speedPowerRoutine()
    {
        while (_speedPowerUpActive == true)
        {

            float old_speed = _player_speed;
            _player_speed = 20;
            yield return new WaitForSeconds(10);
            //changes current player speed back to old speed
            _player_speed = old_speed;
            _speedPowerUpActive = false;

        }
        // creates a value to represent old speed
    }
    // starts shield routine
    IEnumerator shieldPowerRoutine()
    {
        while (_shieldPowerActive == true)
        {
            //shield is off by default but turned on during routine
            _playerShield.SetActive(true);
            yield return new WaitForSeconds(10);
            _playerShield.SetActive(false);
            _shieldPowerActive = false;
            //shield object turned off
        }

    }

    // when a collision is detected with player and object
    public void Damage()
    {
        _lives = _lives - 1;
        Debug.Log("Damage Taken! Lives left: " + _lives);

        if (_lives < 1)
        {
            Debug.Log("Player Dead");
            _spawnManager.stopSpawnOnDeath();
            Destroy(gameObject);
        }
    }
    // method used to return the amount of lives of the player *not used yet* 
    public bool shieldCheck()
    {
        return _shieldPowerActive;
    }
    public int checkLives()
    {
        return _lives;
    }
    // method adds 10 to the score
    public void scoreUpdate(int score)
    {
        _playerScore = _playerScore + score;

    }
    // communicate with UI to update the score
    public int updateUiScore()
    {
        return _playerScore;

    }
}