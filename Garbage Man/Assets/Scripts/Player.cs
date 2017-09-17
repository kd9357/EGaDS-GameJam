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
    public float DropTerminate = 5f;
    public GameObject Trash;
	public AudioClip[] AudioClips;

    //Componenets
    private Rigidbody _rb;
	private AudioSource _audio;

    private int _currentTrash = 0;
    private float _currentSpeed;

    private List<GameObject> _droppedTrash = new List<GameObject>();
    private float _trashTimer;

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
        _audio = gameObject.GetComponent<AudioSource>();
        _currentSpeed = SlowSpeed;
        _trashTimer = 0;
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
        if (_trashTimer > 0)
            _trashTimer -= Time.deltaTime;
        else if(_droppedTrash.Count != 0)
        {
            var t = _droppedTrash[0];
            _droppedTrash.Remove(t);
            _trashTimer = DropTerminate;
            if (t != null)
                Destroy(t);
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Landfill Trigger"))
        {
            if(_currentTrash > 0)
            {
                _audio.clip = AudioClips[1];
                _audio.Play();
            }
            GameController.instance.UpdateScore(_currentTrash);
            _currentTrash = 0;
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Road"))
            _currentSpeed = NormalSpeed;
		if (collision.gameObject.CompareTag ("Hazard")) {
			_audio.clip = AudioClips [2];
			_audio.Play ();
			DropTrash();
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
            _currentSpeed = SlowSpeed;
    }
    #endregion

    #region Helper Methods
    public bool AddTrash()
    {
        if (_currentTrash < TrashCapacity)
        {
            _currentTrash++;
            _audio.clip = AudioClips[0];
            _audio.Play();
            return true;
        }
        return false;
    }

    public void DropTrash()
    {
        if(_currentTrash > 0)
        {
            _currentTrash--;
            var newPosition = transform.position - transform.forward * DropDistance;
            //var x = (GameObject)Instantiate(Trash, newPosition, transform.rotation);
            _droppedTrash.Add((GameObject)Instantiate(Trash, newPosition, transform.rotation));
            _trashTimer = DropTerminate;
        }
    }

    public int TrashAmount()
    {
        return _currentTrash;
    }
    #endregion


}
