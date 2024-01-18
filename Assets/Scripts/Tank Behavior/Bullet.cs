using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletVelocity = 50.0f;
    [SerializeField] private float bulletLifetime = 2.0f;
    [SerializeField] private int bulletDamage = 10;

    Rigidbody rb;

    private string myCharacter;

    public void LaunchBullet(Vector3 xDirection, string character)
    {
        rb = GetComponent<Rigidbody>();
        Vector3 bulletTrajectory = xDirection * bulletVelocity;
        rb.velocity = bulletTrajectory;
        myCharacter = character;

        Destroy(gameObject, bulletLifetime);
    }

    public int GetBulletDamage() { return bulletDamage; }
    public string GetWhoShoot() { return myCharacter; }

    private void OnDestroy()
    {
        EventBusManager.FireEvent<Vector3>(EventBusEnum.EventName.HitEffect, gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider _)
    {
        Destroy(gameObject);
    }
}
