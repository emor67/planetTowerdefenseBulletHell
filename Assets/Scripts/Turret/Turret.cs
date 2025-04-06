using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.Mathematics;

public class Turret : MonoBehaviour
{
    //References
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    
    //Variables
    [SerializeField] private float targetingRange = 6f;
    [SerializeField] private float rotationSpeed = 3000f;
    [SerializeField] private float rotationOffset;
    [SerializeField] private float bulletPerSecond = 3f;

    private Transform target;
    private float timeUntilFire;

    void Update()
    {
        if (target == null){
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;
        } else {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= (1f / bulletPerSecond)) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletobj = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
        Bullet bulletScript = bulletobj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
            
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + rotationOffset;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

}