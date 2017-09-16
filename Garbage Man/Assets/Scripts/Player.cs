using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Speed = 10;

    private Rigidbody _rb;

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
	}

    // Runs once per physics tick
    private void FixedUpdate()
    {
        float x_mov = Input.GetAxisRaw("Horizontal") * Speed;
        float z_mov = Input.GetAxisRaw("Vertical") * Speed;

        Vector3 movement = new Vector3(x_mov, 0.0f, z_mov);

        _rb.velocity = new Vector3(x_mov, 0, z_mov);
        if(x_mov != 0 || z_mov != 0)
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
    }

    // Update is called once per frame
    void Update () {

    }
}
