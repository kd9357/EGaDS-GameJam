using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Do some effect
            //GameController.instance.IncrementScore();
            if(other.GetComponent<Player>().AddTrash())
                 Destroy(gameObject);
        }
    }
}
