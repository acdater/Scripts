using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Update score text field

    [SerializeField] Text _scoreText;
    [SerializeField] Snake _snake;
 
    void Update()
    {
        _scoreText.text = _snake.GetCoinsCount().ToString();
    }
}
