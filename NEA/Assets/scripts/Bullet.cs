using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 7f;
    Rigidbody2D rb;
    [SerializeField] public GameObject target;
    public Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        direction = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(direction.x, direction.y);
        //Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            col.GetComponent<Damage>().TakeDamage(1);
        }
    }
}
