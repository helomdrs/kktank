using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth = 100;

    private const string BULLET_TAG = "Bullet";

    private bool isAlive = true;

    private void TakeDamage(int bulletDamage)
    {
        if(isAlive)
        {
            totalHealth -= bulletDamage;

            //Send and event here of update health for HUD
            EventBusManager.FireEvent<int>(EventBusEnum.EventName.UIHealthUpdate, totalHealth);

            if(totalHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die() 
    {
        //Send an event here of death for VFX/SFX
        EventBusManager.FireEvent<Vector3>(EventBusEnum.EventName.DeathEffect, gameObject.transform.position);

        Debug.Log(gameObject.name + " IS DEAD!");
        isAlive = false;

        //Send here an event of who is dead;
        EventBusManager.FireEvent<string>(EventBusEnum.EventName.TankDead, gameObject.name);
    }

    private void OnTriggerEnter(Collider collider)
    {       
        if(collider.gameObject.CompareTag(BULLET_TAG) && isAlive)
        {
            int bulletDamage = collider.gameObject.GetComponent<Bullet>().GetBulletDamage();
            TakeDamage(bulletDamage);
        }
    }
}
