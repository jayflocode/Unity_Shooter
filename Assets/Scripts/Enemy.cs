using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        // enemy behavior is to move/translate down at a specific speed 4 meters etc per sec
        // if enemy reached to the bottom bounds limit, respawn enemy anywhere(top,bottom, side etc
        // respawn on top at a random x value 

        if (transform.position.y < -5.8)
        {
            float randomPosUpdate = Random.Range(-9.1f, 9.1f);
            transform.position = new Vector3(randomPosUpdate, 7.5f, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Detected with: " + other.tag);

        switch (other.tag)
        {
            case "Player":
                Debug.Log("Enemy hit Player");
                Player player = other.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.Damage();
                }
                else
                {
                    Debug.Log("No Script Associated/Script Missing for Player");
                }
                Destroy(gameObject);
                break;
            case "Laser":
                Debug.Log("Enemy hit by laser");
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
        }

    }
}
