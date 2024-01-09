using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletVelocity = 50.0f;
    [SerializeField] private float bulletLifetime = 2.0f;

    Rigidbody rb;

    public void LaunchBullet(Vector3 xDirection)
    {
        rb = GetComponent<Rigidbody>();
        Vector3 bulletTrajectory = xDirection * bulletVelocity;
        rb.velocity = bulletTrajectory;

        Destroy(gameObject, bulletLifetime);
    }

    private void OnDestroy()
    {
        //Send an event here of explosion for VFX/SFX
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
