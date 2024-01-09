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
    private Health healthController;

    void Start()
    {
        movementController = GetComponent<Movement>();
        combatController = GetComponent<Combat>();
        healthController = GetComponent<Health>();

        //Put this in a initialize event later
        combatController.ActivateTurret();
    }

    public void MoveTank(float moveInput)
    {
        movementController.UpdateTankMovement(moveInput);
    }

    public void RotateTank(float moveInput) 
    {
        movementController.UpdateTankRotateion(moveInput);
    }

    public void TankShoot()
    {
        combatController.Shoot();
    }
}
