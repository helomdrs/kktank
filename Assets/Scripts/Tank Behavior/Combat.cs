using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform turret;
    [SerializeField] float shotDelay = 1f;

    float lastShotTime = 0;

    public void RotateTurret(Vector3 targetPosition)
    {  
        if(targetPosition != null) 
        {
             turret.LookAt(targetPosition);

            //-90 to fix the orientation of the model, clamp x and z rotation
            turret.rotation = Quaternion.Euler(new Vector3(-90, turret.rotation.eulerAngles.y, 0));
        }
    }

    //This method should be "protected" to be accessed only by TankBehavior
    public void Shoot()
    {
        if((Time.time - lastShotTime) >= shotDelay) 
        {
            EventBusManager.FireEvent<Vector3>(EventBusEnum.EventName.ShootEffect, muzzle.position);
            
            Vector3 spawnPosition = muzzle.position;
            Quaternion spawnRotation = turret.rotation;

            //Vector3.down used because of X -90 rotation of model
            Vector3 xDirection = turret.TransformDirection(Vector3.down);

            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            newBullet.GetComponent<Bullet>().LaunchBullet(xDirection, gameObject.tag);

            lastShotTime = Time.time;
        }
    }
}
