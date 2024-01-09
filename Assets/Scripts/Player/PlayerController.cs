using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TankBehavior))]
public class PlayerController : MonoBehaviour
{
    private TankBehavior tankBehavior;

    private void Start()
    {
        tankBehavior = GetComponent<TankBehavior>();
    }

    public void OnMove(InputValue value) 
    {
        tankBehavior.MoveTank(value.Get<float>());
    }

    public void OnRotate(InputValue value) 
    {
        tankBehavior.RotateTank(value.Get<float>());
    }

    public void OnAttack() 
    {
        tankBehavior.TankShoot();
    }
}
