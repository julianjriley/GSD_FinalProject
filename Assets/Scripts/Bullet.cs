using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 20f;

    private void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        EnemyHealth enemy;
        if (col.gameObject.TryGetComponent<EnemyHealth>(out enemy))
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }


    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }

}
