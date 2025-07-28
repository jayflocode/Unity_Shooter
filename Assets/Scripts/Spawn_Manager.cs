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
    // container used to store all enemies 
    private GameObject _enemyContainer;
    [SerializeField]
    //spawn limit variable that can be adjusted in unity editor
    private int _spawnLimit = 5;
    private int _spawnCounter = 0;
    // variable assigned to evaluate whether spawning needs to take place
    private bool _endSpawning = false;
    

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
        while (_endSpawning == false)

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
                Debug.Log("Enemies Spawned: " + _spawnCounter);
                // yield returns adds 10 seconds to the enumator method 
                yield return new WaitForSeconds(10);

            }
        }
    }
    // this method is called after the player has no more lives which results in no more enemies spawning

    public void stopSpawn()
    {
        _endSpawning = true;
    }
    

}
