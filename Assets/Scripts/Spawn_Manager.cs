using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    //enemy game variable used for enemy spawning and placement after spawning 
    private GameObject _enemyPreFab;
    [SerializeField]
    private GameObject _tripleShotPreFab;
    [SerializeField]
    private GameObject _speedPowerUpPreFab;
    // container used to store all enemies 
    [SerializeField]
    private GameObject _shieldPowerUpPreFab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    //spawn limit variable that can be adjusted in unity editor
    private int _spawnLimit = 5;
    private int _spawnCounter = 0;
    // variable assigned to evaluate whether spawning needs to take place
    [SerializeField]
    private bool _endSpawning = false;
    //debug
    [SerializeField]
    //time until triple shot starts in seconds
    private int _r_tripleShot_Start = 5;
    //debug
    [SerializeField]
    private int _r_tripleShotEnd = 25;
    [SerializeField]
    private int _r_speedBoostStart = 15;
    [SerializeField]
    private int _r_speedBoostEnd = 30;
    [SerializeField]
    private int _r_shieldBoostStart = 17;
    [SerializeField]
    private int _r_shieldBoostEnd = 35;

    // random x value in x plane for spawning of objects



    // counts enemy repawn times
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(spawnTripleShot());
        StartCoroutine(spawnSpeedBoost());
        StartCoroutine(spawnShieldBoost());

    }

    // Update is called once per frame
    void Update()
    {




    }

    IEnumerator SpawnEnemyRoutine()
    {
        // while condition remains true 
        // while player is alive run this loop 
        // how can we know the player is alive? by examining the player's lives or by examining if player is alive

        // while player is not null 
        yield return new WaitForSeconds(3);


        while (_endSpawning == false)

        {
            float lastPos = 0;
            // spawn counter counts how many times a spawn has occured and spawn limit is the value set for the limits of spawning 
            if (_spawnCounter >= _spawnLimit)
            {
                Debug.Log("No More Spawning! All Concurrent Tasks have ended");
                _endSpawning = true;
                break;
            }
            else
            {
                //random x value so that enemy spawn at a random x coordinate
                //creates game object called new Enemy that instantiates an enemy when newEnemy is called
                float randomX = Random.Range(-9, 9);
                randomX = randomX - lastPos;
                Debug.Log(randomX + " and " + lastPos);
                GameObject newEnemy = Instantiate(_enemyPreFab, new Vector3(randomX, 7, 0), quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform; // new enemy is assigned to transform container object
                _spawnCounter++;
                Debug.Log("Enemies Spawned: " + _spawnCounter);
                // yield returns adds 10 seconds to the enumator method 
                yield return new WaitForSeconds(3);
                lastPos = randomX;

            }
        }
    }
    // this method is called after the player has no more lives which results in no more enemies spawning

    public void stopSpawnOnDeath()
    {
        _endSpawning = true;
        Destroy(_enemyContainer);

    }

    IEnumerator spawnTripleShot()
    {  // spawn power up every 20 seconds 
        // how to determine how much time has passed by
        while (_endSpawning == false)
        {

            yield return new WaitForSeconds(Random.Range(_r_tripleShot_Start, _r_tripleShotEnd));

            if (_endSpawning == true)
            {
                break;
            }
            else
            {
                float randomX = Random.Range(-9, 9);
                Instantiate(_tripleShotPreFab, new Vector3(randomX, 7, 0), quaternion.identity);
            }

        }
    }
    IEnumerator spawnSpeedBoost()
    {
        while (_endSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(_r_speedBoostStart,_r_speedBoostEnd));

            if (_endSpawning == true)
            {
                Debug.Log("Triple Shot Ended");
                break;
                
            }
            else
            {
                float randomX = Random.Range(-9, 9);
                Instantiate(_speedPowerUpPreFab, new Vector3(randomX, 7, 0), quaternion.identity);

            }


        }
    }
    IEnumerator spawnShieldBoost()
    {
        while (_endSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(_r_shieldBoostStart, _r_shieldBoostEnd));

            if (_endSpawning == true)
            {
                break;
            }
            else
            {
                float randomX = Random.Range(-7, 9);
                Instantiate(_shieldPowerUpPreFab, new Vector3(randomX, 7, 0), quaternion.identity);
            }
        }
        

    }
}
