using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Testing vars
    public bool LockedMovement = true;

    //Public parameters
    public float NormalSpeed = 15;
    public float SlowSpeed = 10;
    public int TrashCapacity = 5;
    public float DropDistance = 5;
    public GameObject Trash;

    //Componenets
    private Rigidbody _rb;

    private int _currentTrash = 0;
    private float _currentSpeed;

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
        _currentSpeed = SlowSpeed;
	}

    #region Updates
    // Runs once per physics tick
    private void FixedUpdate()
    {
        float x_mov = LockedMovement ? Input.GetAxisRaw("Horizontal") * _currentSpeed
                                     : Input.GetAxis("Horizontal") * _currentSpeed ;
        float z_mov = LockedMovement ? Input.GetAxisRaw("Vertical") * _currentSpeed
                                     : Input.GetAxis("Vertical") * _currentSpeed;

        if (Mathf.Abs(x_mov) + Mathf.Abs(z_mov) > 1)
        {
            x_mov /= 2;
            z_mov /= 2;
        }
        var movement = new Vector3(x_mov, _rb.velocity.y, z_mov);
        _rb.velocity = movement;
        movement.y = 0;
        if(x_mov != 0 || z_mov != 0)
            transform.rotation = Quaternion.LookRotation(movement);
            
    }

    // Update is called once per frame
    void Update () {

    }
    #endregion

    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Landfill Trigger"))
        {
            GameController.instance.UpdateScore(_currentTrash);
            _currentTrash = 0;
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Road"))
        {
            _currentSpeed = NormalSpeed;
        }
        if(collision.gameObject.CompareTag("Hazard"))
        {
            if (_currentTrash > 0)
            {
                _currentTrash--;
                var newPosition = transform.position - transform.forward * DropDistance;
                var x = (GameObject)Instantiate(Trash, newPosition, transform.rotation);
            }
          
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
            _currentSpeed = NormalSpeed;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            _currentSpeed = SlowSpeed;
        }
    }
    #endregion

    #region Helper Methods
    public bool AddTrash()
    {
        if (_currentTrash < TrashCapacity)
        {
            _currentTrash++;
            return true;
        }
        return false;
    }

    public int TrashAmount()
    {
        return _currentTrash;
    }
    #endregion


}
