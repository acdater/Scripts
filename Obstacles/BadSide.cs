using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSide : MonoBehaviour
{   // check player and kill him
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            var _snake = collision.gameObject.GetComponent<Snake>();
            _snake.Die();
        }
    }
}
