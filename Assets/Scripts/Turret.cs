using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationpoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint; 

    [Header("Attributes")]
    [SerializeField] private float targetRange = 3f; // How far the turrets can shoot. 
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bbs = 1f; // Bullets Per Second

    private Transform target;
    private float timeUntilFire; 

    private void Update()
    {
        if(target == null)
        {FindTarget();
        return;}

        RotateTowardsTarget();

        if(!CheckTargetIsInRange())
        {target = null;}
        else
        {timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / bbs) 
            { Shoot();
            timeUntilFire = 0f;
            }
        }
    }

   private void Shoot()
    {GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target); 
    }

    private void FindTarget()
    {
        {RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask); 
        if (hits.Length > 0)
            { 
                target = hits[0].transform; 
            }
        }
    }

    private bool CheckTargetIsInRange()
    { return Vector2.Distance(target.position, transform.position) <= targetRange; }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
    Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationpoint.rotation = Quaternion.RotateTowards(turretRotationpoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    
    }

    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange); // The area of which enemies can be shot.
    }
}
