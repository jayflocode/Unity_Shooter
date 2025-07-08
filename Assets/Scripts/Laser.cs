using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float _laserSpeed = 8;

    // Update is called once per frame
    void Update()
    {
        //method called when a laser is shot
        laserShot();


    }

    void laserShot()
    {
    
        //translates prefab upwards
        transform.Translate(Vector3.up * Time.deltaTime * _laserSpeed);

        // if laser travels "out of sight" object is then destroyed or discarded 
        if (transform.position.y > 10.5)
        {
            Destroy(gameObject);
        }
    }
}
