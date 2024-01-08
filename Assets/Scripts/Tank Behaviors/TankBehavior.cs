using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        combatController.ActivateTurret();
    }

    // For testing
    void Update() 
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");

        movementController.MoveTank(moveInput);
        movementController.RotateTank(rotationInput);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            combatController.Shoot();
        }
    }
}
