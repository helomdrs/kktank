using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TankBehavior))]
public class PlayerController : MonoBehaviour
{
    private TankBehavior tankBehavior;

     private void OnEnable() 
    {
        EventBusManager.Subscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe(EventBusEnum.EventName.EndMatch, OnMatchEnded);
    }

    private void Start()
    {
        tankBehavior = GetComponent<TankBehavior>();
    }

    private void OnMatchStarted()
    {
        //Change this later to BIND the inputs from input system
        tankBehavior.InitBehavior();
    }

    private void OnMatchEnded()
    {
        //Change this later to UNBIND the inputs from input system
        tankBehavior.DisableBehavior();
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
