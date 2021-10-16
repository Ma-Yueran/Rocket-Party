using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public int maxBounce = 5;
    public string hitTag;
    public CharacterControl parentCharacter;

    private int currentBounce = 0;
    public Collider2D objectCollider;

    private void Awake()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        currentBounce = 0;
        StartCoroutine(EnableColliderAfter(0.2f));
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == hitTag)
        {
            CharacterControl hitCharacter = collision.collider.GetComponent<CharacterControl>();
            hitCharacter.TakeDamage();
            if (hitCharacter != parentCharacter)
            {
                parentCharacter.UpdateScore(1);
            }
        }

        if (currentBounce >= maxBounce)
        {
            Destroy(transform.gameObject);
            return;
        }

        Vector2 collisionNormal = collision.contacts[0].normal;
        float collisionAngle = Vector2.SignedAngle(-transform.up, collisionNormal);
        transform.up = -transform.up;
        transform.Rotate(Vector3.forward, collisionAngle * 2);
        currentBounce++;
    }

    private IEnumerator EnableColliderAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreCollision(parentCharacter.collider, objectCollider, false);
    }
}
