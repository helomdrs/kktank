using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth = 100;
    [SerializeField] AudioClip damageSfx;
    [SerializeField] AudioClip explosionSfx;

    private const string BULLET_TAG = "Bullet";

    private bool isAlive = true;

    private void TakeDamage(int bulletDamage)
    {
        if(isAlive)
        {
            totalHealth -= bulletDamage;
            PlaySFX(true);

            if(gameObject.CompareTag("Player")) {
                EventBusManager.FireEvent<int>(EventBusEnum.EventName.UIHealthUpdate, totalHealth);
            }
            
            if(totalHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die() 
    {
        PlaySFX(false);
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

    private void PlaySFX(bool isDamage)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        
        if(isDamage) 
        {
            audioSource.clip = damageSfx;
        } 
        else 
        {
            audioSource.clip = explosionSfx;
        }

        audioSource.Play();
    }
}
