using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoock : MonoBehaviour
{
    private float _rotationY = 0f;
    public float sensitivityY = 2.0f;
    public float minimumY = -360.0f;
    public float maximumY = 360.0f;
    Quaternion originalRotation;
    void Start()
    {
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);
        Quaternion yQuaternion = Quaternion.AngleAxis(-_rotationY ,Vector3.right);
        transform.localRotation = originalRotation * yQuaternion;
        
    }
}
