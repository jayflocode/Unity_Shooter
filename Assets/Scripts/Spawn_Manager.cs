using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPreFab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private int _spawnLimit = 5;
    private int _spawnCounter = 0;
    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject _player;

    // counts enemy repawn times
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator SpawnRoutine()
    {
        // while condition remains true 
        // while player is alive run this loop 
        // how can we know the player is alive? by examining the player's lives or by examining if player is alive

        // while player is not null 
        while (_stopSpawning == false)

        {

            // spawn counter counts how many times a spawn has occured and spawn limit is the value set for the limits of spawning 
            if (_spawnCounter >= _spawnLimit)
            {
                Debug.Log("No More Spawning!");
                break;
            }
            else
            {
                //random x value so that enemy spawn at a random x coordinate
                float randomX = Random.Range(-9, 9);
                //creates game object called new Enemy that instantiates an enemy when newEnemy is called
                GameObject newEnemy = Instantiate(_enemyPreFab, new Vector3(randomX, 7, 0), quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform; // new enemy is assigned to transform container object
                _spawnCounter++;  // 1 enemy spawn added to counter
                Debug.Log("Enemies Spawned: " + _spawnCounter);
                // yield returns adds 10 seconds to the enumator method 
                yield return new WaitForSeconds(10);

            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    

}
