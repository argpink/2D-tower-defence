using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Referances")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;

    private Transform target;

    public void SetTarget(Transform _target) 
    { target = _target; }

    private void FixedUpdate() 
    {if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;}

    private void OnCollisionEnter2D(Collision2D other)
    {   // Take health from enemy 
        Destroy(gameObject); }
}