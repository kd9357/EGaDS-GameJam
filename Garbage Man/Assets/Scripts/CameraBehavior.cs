using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public GameObject target;
    public float MinSize = 5;
    public float StartSize = 15;
    public float MaxSize = 30;
    public float ScrollSpeed = 300;
    public float Distance = 30;

    //I don't think I really need this
    private Camera _cam;

    void Start()
    {
        _cam = (Camera)this.gameObject.GetComponent("Camera");
        _cam.transform.rotation = Quaternion.Euler(30, 45, 0);
        _cam.orthographicSize = StartSize;
    }

    void Update()
    {
        _cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed * Time.deltaTime;
        if (_cam.orthographicSize > MaxSize)
            _cam.orthographicSize = MaxSize;
        else if (_cam.orthographicSize < MinSize)
            _cam.orthographicSize = MinSize;

        float distance = 30;

        transform.position = target.transform.position + new Vector3(-distance, distance, -distance);
        _cam.transform.LookAt(target.transform);
    }
}
