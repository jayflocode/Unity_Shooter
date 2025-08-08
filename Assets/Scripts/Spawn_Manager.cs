using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn_Manager : MonoBehaviour
{

    [SerializeField]
    //enemy game variable used for enemy spawning and placement after spawning 
    private GameObject _enemyPreFab;
    private Player _player;
    [SerializeField]
    private GameObject[] Power_Ups;
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

    // random x value in x plane for spawning of objects



    // counts enemy repawn times
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(spawnPowerUps());
        

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
           
                GameObject newEnemy = Instantiate(_enemyPreFab, new Vector3(randomX, 7, 0), quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform; // new enemy is assigned to transform container object
                // spawn counter increases with the instantiation of new enemies
                _spawnCounter++;
                Debug.Log("Enemies Spawned: " + _spawnCounter);
                // 3 second wait time in between spawned enemies
                yield return new WaitForSeconds(3);
                // last post remembers last area where enemy spawned so that new spawning takes place in another
        

            }
        }
    }
    // this method is called after the player has no more lives which results in no more enemies spawning

    public void stopSpawnOnDeath()
    {
        _endSpawning = true;
        //destroys all enemies present on death
        Destroy(_enemyContainer);

    }

    IEnumerator spawnPowerUps()
    {
        
        while (_endSpawning == false)

        {

            
            Debug.Log("Power Up: Player Lives " + _player.checkLives());

            switch (_player.checkLives())
            {
                case  1:
                    Debug.Log("Low on lives, Faster power up");
                    yield return new WaitForSeconds(Random.Range(5, 8));
                    break;   
                default:
                    Debug.Log("Normal Speed Power up");
                    yield return new WaitForSeconds(Random.Range(8, 15));
                    break;

            }
        
            if (_endSpawning == false)
            {
                int randomPower = Random.Range(0, 3);
                // if players lives is less than one spawn faster
                // x coordinate values for spawn
                float randomX = Random.Range(-9, 9);
                // to randomize single selection of power ups
                Instantiate(Power_Ups[randomPower], new Vector3(randomX, 7, 0), quaternion.identity);
                
            }
            

        }

    }
    

}
