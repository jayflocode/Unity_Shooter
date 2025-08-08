using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 8;
    private Player _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

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
       
        if (other.tag == "Player") {

            Player player = other.transform.GetComponent<Player>();

            Debug.Log("Enemy hit Player");

                if (player != null)
                {
                    if (player.shieldCheck() != true)
                    {
                        player.Damage();
                        
                    }
                }
                Destroy(gameObject);

        }

        if (other.tag == "Laser")
        {
                Debug.Log("Enemy hit by laser");
                Destroy(other.gameObject);
            // before destroying enemy add points to the score, score variable is in player script class
            if (_player != null)
            {
                _player.scoreUpdate(10);
                    
            }
            Destroy(gameObject);
            
        }

    }

}
