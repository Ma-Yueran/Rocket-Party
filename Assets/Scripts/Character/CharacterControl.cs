using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterControl : MonoBehaviour
{
    [SerializeField] protected float characterSpeed;
    [SerializeField] protected float rocketCooldownDuration;
    [SerializeField] protected GameObject rocketClass;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected bool isInvincible = false; // Mutable at runtime
    protected float rocketCooldownTimer = 0.0f;
    
    public new Collider2D collider;

    private int score = 0;
    [SerializeField]
    private Text scoreText;
    
    protected virtual void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }
    
    public virtual void TakeDamage()
    {
        if (isInvincible) return;
        transform.position = spawnPos.position;
        UpdateScore(-1);
    }

    protected void TickCooldownTimer()
    {
        if (rocketCooldownTimer > 0.0f)
        {
            rocketCooldownTimer = Mathf.Max(0.0f, rocketCooldownTimer - Time.deltaTime);
        }
    }

    public bool FireWeapon(Vector3 targetPosition)
    {
        if (rocketCooldownTimer > 0.0f)
        {
            return false;
        }
        rocketCooldownTimer = rocketCooldownDuration;
        var angle = Vector2.SignedAngle(Vector2.up, targetPosition - transform.position);
        var rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        var rocket = Instantiate(rocketClass, transform.position, rotation);
        rocket.GetComponent<Rocket>().parentCharacter = this;
        Physics2D.IgnoreCollision(collider, rocket.GetComponent<Rocket>().objectCollider, true);
        return true;
    }

    public void UpdateScore(int change)
    {
        score += change;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
