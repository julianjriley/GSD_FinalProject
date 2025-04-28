using System.Collections;
using System.Collections.Generic;
using GameKit.Dependencies.Utilities;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float maxScale = 1f;
    public float damage = 1f;
    public float duration = 0.5f;
    public bool damagesPlayer = false;
    public bool damagesEnemies = false;

    List<GameObject> damagedTargets = new List<GameObject>();
    float timer = 0f;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float explosionScalar = Mathf.InverseLerp(0, duration, timer);
        transform.SetScale(new Vector3(explosionScalar * maxScale, explosionScalar * maxScale, explosionScalar * maxScale));
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1 - explosionScalar);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHealth player;
        EnemyHealth enemy;
        if (!damagedTargets.Contains(col.gameObject))
        {
            if (damagesPlayer && col.TryGetComponent<PlayerHealth>(out player))
            {
                player.TakeDamage(damage);
                damagedTargets.Add(col.gameObject);
            }
            if (damagesEnemies && col.TryGetComponent<EnemyHealth>(out enemy))
            {
                enemy.TakeDamage(damage);
                damagedTargets.Add(col.gameObject);
            }
        }
    }
}
