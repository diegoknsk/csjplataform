using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	//teste
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add + 1 maca, destroi depois
        collision.GetComponent<Player>().IncreaseScore();
        Destroy(gameObject);
    }
}
