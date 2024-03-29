using UnityEngine;

//My intention here is to make TankBehavior the only class available for PlayerController and AIController to access. 
//I wish to keep all public methods of Combat and Movement to similar to 'protected', to be accessed only for TankBehavior.

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(Health))]
public class TankBehavior : MonoBehaviour
{
    private Movement movementController;
    private Combat combatController;
    
    private bool isInitialized = false;

    public void InitBehavior() 
    {
        movementController = GetComponent<Movement>();
        combatController = GetComponent<Combat>();
        
        isInitialized = true;
    }

    public void DisableBehavior() 
    {
        isInitialized = false;
    }

    public void MoveTank(float moveInput)
    {
        if(!isInitialized) return;
        movementController.UpdateTankMovement(moveInput);
    }

    public void RotateTank(float moveInput) 
    {
        if(!isInitialized) return;
        movementController.UpdateTankRotateion(moveInput);
    }

    public void TankShoot()
    {
        if(!isInitialized) return;
        combatController.Shoot();
    }

    public void RotateTurret(Vector3 targetPosition) 
    {
        if(!isInitialized) return;
        combatController.RotateTurret(targetPosition);
    }
}
