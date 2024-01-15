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

            EventBusManager.FireEvent<int>(EventBusEnum.EventName.UIHealthUpdate, totalHealth);

            if(totalHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die() 
    {
        EventBusManager.FireEvent<Vector3>(EventBusEnum.EventName.DeathEffect, gameObject.transform.position);

        Debug.Log(gameObject.name + " IS DEAD!");
        isAlive = false;

        Destroy(gameObject, 1f);

        EventBusManager.FireEvent<string>(EventBusEnum.EventName.TankDead, gameObject.tag);
    }

    private void OnTriggerEnter(Collider collider)
    {       
        if(collider.gameObject.CompareTag(BULLET_TAG) && isAlive)
        {
            Bullet bullet = collider.gameObject.GetComponent<Bullet>();
            if(bullet.GetWhoShoot() != gameObject.tag) 
            {
                int bulletDamage = bullet.GetBulletDamage();
                TakeDamage(bulletDamage);
            }
            
        }
    }
}
