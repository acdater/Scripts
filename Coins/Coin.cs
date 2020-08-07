using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  
    //check player and add coin to tail or die if its already owned coin
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null && other.gameObject.tag == "Player")
        {
            var _snakeHead = other.gameObject.GetComponent<Snake>();
            if (_snakeHead.HasCoin(this.gameObject) == false)
            {
                _snakeHead.AddToTail(this.gameObject);
            }
            else
            {
                _snakeHead.Die();
            }
            
        }
    }
}
