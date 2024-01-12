using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletVelocity = 50.0f;
    [SerializeField] private float bulletLifetime = 2.0f;
    [SerializeField] private int bulletDamage = 10;

    Rigidbody rb;

    public void LaunchBullet(Vector3 xDirection)
    {
        rb = GetComponent<Rigidbody>();
        Vector3 bulletTrajectory = xDirection * bulletVelocity;
        rb.velocity = bulletTrajectory;

        Destroy(gameObject, bulletLifetime);
    }

    public int GetBulletDamage() { return bulletDamage; }

    private void OnDestroy()
    {
        //Send an event here of explosion for VFX/SFX
        EventBusManager.FireEvent<Vector3>(EventBusEnum.EventName.HitEffect, gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider _)
    {
        Destroy(gameObject);
    }
}
