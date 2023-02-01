using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField]
    public float speed;

    Vector3 velocity;

    [SerializeField]
    public float g;         //how much gravity on the planet

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool isGrounded;

    void Update()
    {
        //Gravity and ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Moving camera
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Fall off from high ground
        velocity.y += g * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
