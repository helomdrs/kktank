using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth = 100;
    [SerializeField] private int bulletDamage = 10;

    private const string BULLET_TAG = "Bullet";

    private void TakeDamage()
    {
        //Send an event here of damage for VFX/SFX

        totalHealth -= bulletDamage;
        //Send and event here of update health for HUD

        if(totalHealth <= 0)
        {
            //Send an event here of death for VFX/SFX
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        
        if(collision.gameObject.CompareTag(BULLET_TAG))
        {
            TakeDamage();
        }
    }
}
