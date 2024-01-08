using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform turret;
    [SerializeField] float shotDelay = 1f;

    Coroutine rotateTurretCo;

    bool isTurretActive = false;
    float lastShotTime = 0;

    //For Testing
    private void Start() { ActivateTurret(); }
    
    public void ActivateTurret()
    {
        isTurretActive = true;
        rotateTurretCo = StartCoroutine(RotateTurret());
    }

    public void DisableTurret()
    {
        isTurretActive = false;
        StopCoroutine(rotateTurretCo);
    }

    private IEnumerator RotateTurret()
    {
        while(isTurretActive)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                turret.LookAt(hit.point);

                //-90 to fix the orientation of the model, clamp x and z rotation
                turret.rotation = Quaternion.Euler(new Vector3(-90, turret.rotation.eulerAngles.y, 0));
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void Shoot()
    {
        if(isTurretActive && (Time.time - lastShotTime >= shotDelay)) 
        {
            Debug.Log("Shooting");
            Vector3 spawnPosition = muzzle.position;
            Quaternion spawnRotation = turret.rotation;
            Vector3 xDirection = turret.TransformDirection(Vector3.down);

            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            newBullet.GetComponent<Bullet>().LaunchBullet(xDirection);

            lastShotTime = Time.time;
        }
    }
}
