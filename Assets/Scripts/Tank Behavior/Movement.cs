using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20.0f;
    [SerializeField] private float rotationSpeed = 30.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //This method should be "protected" to be accessed only by TankBehavior
    public void MoveTank(float input)
    {
        //Send an event here of moving for VFX/SFX????

        Vector3 moveDirection = input * moveSpeed * Time.fixedDeltaTime * transform.forward;
        rb.MovePosition(rb.position + moveDirection);
    }

    //This method should be "protected" to be accessed only by TankBehavior
    public void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

}
