using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth = 100;
    [SerializeField] private int bulletDamage = 10;

    private const string BULLET_TAG = "Bullet";

    private void TakeDamage()
    {
        Debug.Log("Took damage");
        totalHealth -= bulletDamage;
        if(totalHealth <= 0)
        {
            Debug.Log("Is dead");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(BULLET_TAG))
        {
            TakeDamage();
        }
    }
}
