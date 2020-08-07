using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObstacle : MonoBehaviour
{   //check player and receove damage
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.gameObject.tag == "Player")
        {
            var _snake = other.gameObject.GetComponent<Snake>();
            _snake.Damage();
            Destroy(this.gameObject);
        }
    }

}
