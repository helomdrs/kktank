using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TankBehavior))]
public class PlayerController : MonoBehaviour
{
    private TankBehavior tankBehavior;

    private void Start()
    {
        tankBehavior = GetComponent<TankBehavior>();
        tankBehavior.InitBehavior();
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
    
    public void OnMouse() 
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

        if(Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            tankBehavior.RotateTurret(hit.point);
        }
    }
}
