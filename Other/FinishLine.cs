using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // check the player, stop game and call finishPanel function from _uiManager

    [SerializeField] UIManager _uiInstance;
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.gameObject.tag == "Player")
        {
            var _snake = other.gameObject.GetComponent<Snake>();
            _snake.SetStopValue(true);
            _uiInstance.SetFinishPanel();
        }
    }
}
