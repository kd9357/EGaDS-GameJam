using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianBehavior : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float SpawnTimerLower = 30f;
    public float SpawnTimerUpper = 60f;
    public bool StartSpawned = false;
    public float DropDistance = 8f;
    public bool Standing = true;
    public float Speed = 5f;

    private Rigidbody _rb;
    private float _timer;
    private GameObject _go;
    private bool _alive = true;

    // Use this for initialization
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _go = StartSpawned ? (GameObject)Instantiate(ItemPrefab, transform.position - transform.forward * DropDistance, transform.rotation) : null;
        _timer = Random.Range(SpawnTimerLower, SpawnTimerUpper);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Standing)
        {
            
            _rb.velocity = transform.forward * Speed;
        }
        if (_go == null && _alive)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                var newPosition = transform.position - transform.forward * DropDistance;
                _go = (GameObject)Instantiate(ItemPrefab, newPosition, transform.rotation);
                _timer = Random.Range(SpawnTimerLower, SpawnTimerUpper);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            _alive = false;
        }
    }

}