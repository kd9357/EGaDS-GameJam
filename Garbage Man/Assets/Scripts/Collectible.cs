using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemId
{
    Trash,
    Heatlh
}

public class Collectible : MonoBehaviour {

    public ItemId Item;

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
            GameController.instance.IncrementScore();

            Destroy(gameObject);
        }
    }
}
