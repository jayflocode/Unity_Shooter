using UnityEngine;
using UnityEngine.UIElements;

public class Triple_Shot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     [SerializeField]
    private float _tripleLaserSpeed = 8;

    // Update is called once per frame
    void Update()
    {
        //method called when a laser is shot
        tripleLaserShot();


    }

    void tripleLaserShot()
    {
    
        //controls laser speed
        transform.Translate(Vector3.up * Time.deltaTime * _tripleLaserSpeed);

        // if laser travels "out of sight" object is then destroyed or discarded 
        if (transform.position.y > 10.5)
        {
            Destroy(gameObject);
        }
    }
}
