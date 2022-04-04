using System;
using UnityEngine;
public class PlayerMoveController : MonoBehaviour
{
  
    public float speed = 4.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 MoveDir = Vector3.zero;
    private CharacterController controller;
    public float sensitivityX = 2.0f;
    private float _rotationX;
    Quaternion originalRotation;
    private string portalTag = "Portal";

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        
        if (controller.isGrounded)
        {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = transform.TransformDirection(MoveDir);
            MoveDir *= speed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            MoveDir.y = jumpSpeed;
        }
        MoveDir.y -= gravity * Time.deltaTime;
        controller.Move(MoveDir * Time.deltaTime);
        
        _rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
        transform.localRotation = originalRotation * xQuaternion;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        var obj = other.gameObject.GetComponent<ICollectable>();
        obj?.Collect();
        if (other.gameObject.CompareTag(portalTag))
        {
            LevelData.Instance.LoadNextLevel();
        }
    }
}
