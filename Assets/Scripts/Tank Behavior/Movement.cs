using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    private Rigidbody rb;

    private float moveInput = 0.0f;
    private float rotateInput = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        MoveTank(moveInput);
        RotateTank(rotateInput);
    }

    //This method should be "protected" to be accessed only by TankBehavior
    public void UpdateTankMovement(float input) 
    {
        moveInput = input;
    }

    //This method should be "protected" to be accessed only by TankBehavior
    public void UpdateTankRotateion(float input) 
    {
        rotateInput = input;
    }

    private void MoveTank(float input)
    {
        //Send an event here of moving for VFX/SFX????

        Vector3 moveDirection = input * moveSpeed * Time.fixedDeltaTime * transform.forward;
        rb.MovePosition(rb.position + moveDirection);
    }

    private void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

}
