using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Triple_Shot_PowerUp : MonoBehaviour
{
    [SerializeField]
    float _powerUpSpeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // power up when is instantiated will travel down at a random x pattern 
        transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);

        if (transform.position.y < -5.8)
        {
            //float randomPosUpdate = Random.Range(-9.1f, 9.1f);
            //transform.position = new Vector3(randomPosUpdate, 7.5f, 0);
            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Collision Detected with: " + other.tag);

        if (other.tag == "Player")
        {

            Destroy(gameObject);
            Player player = other.transform.GetComponent<Player>();
            player.tripleShotSwitch(true);
    }

    }

}
