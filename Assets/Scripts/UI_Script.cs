using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Player _player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scoreText.text = "Score: ";
       _player = GameObject.Find("Player").GetComponent<Player>();
        

    }

    // Update is called once per frame
    void Update()
    {
        // I need to get score from player referenc

       
        if (_player != null)
        {

            _scoreText.text = "Score: " + _player.updateUiScore();
            

        }
        
  
    }

   
}
