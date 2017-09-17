using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {

    public GameObject ItemPrefab;

    public float SpawnTimerLower = 30f;
    public float SpawnTimerUpper = 60f;
    public bool StartSpawned = false;

    private float _timer;
    private GameObject _go;

	// Use this for initialization
	void Start () {
        _go = StartSpawned ? (GameObject)Instantiate(ItemPrefab, transform.position, transform.rotation) : null;
        _timer = Random.Range(SpawnTimerLower, SpawnTimerUpper);
	}
	
	// Update is called once per frame
	void Update () {
        if(_go == null)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                _go = (GameObject)Instantiate(ItemPrefab, transform.position, transform.rotation);
                _timer = Random.Range(SpawnTimerLower, SpawnTimerUpper);
                Debug.Log("Time: " + _timer);
            }
        }
    }
}
