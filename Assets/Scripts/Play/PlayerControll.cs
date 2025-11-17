using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("플레이어 세팅")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float mouseSensitivity = 2f;

    [Header("카메라")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Animator anim;

    private Vector3 moveDir;
    private float cameraPitch = 0f;
    private bool isGrounded = true;
    private bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //마우스 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0, 0);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        moveDir = (forward * moveZ + right * moveX).normalized;

        //애니메이션
        if (!anim.GetBool("IsJump"))
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            float speedValue = moveDir.magnitude * speed;

            float forwardValue = Vector3.Dot(transform.forward, moveDir);

            anim.SetFloat("Speed", speedValue);
            anim.SetFloat("Direction", forwardValue);
        }

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            isJumping = true;
    }

    void FixedUpdate()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

        if (isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

        if (anim != null)
        {
            anim.SetTrigger("Jump");
            anim.SetBool("IsJump", true);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (anim != null)
                anim.SetBool("IsJump", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                item.OnCollected();
            }
        }
    }
}
