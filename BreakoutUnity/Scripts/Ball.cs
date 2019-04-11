using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Paddle paddle;
    public BrickManager brickManager;

    private Rigidbody rb;
    private int bricksCollected;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Reset(0);
    }

    public void Respawn()
    {
        transform.position = Vector3.zero;
        Vector2 startV = Random.insideUnitCircle.normalized * speed;
        rb.velocity = new Vector3(Mathf.Min(startV.x, startV.y), 0, Mathf.Max(startV.x, startV.y));
        if(lives-- <= 0)
        {
            brickManager.Done(-brickManager.GetActiveBrickCount());
        }
    }

    public void Reset(int lives)
    {
        this.lives = lives;
        bricksCollected = 0;
    }

    public int GetReward()
    {
        int reward = bricksCollected;
        bricksCollected = 0;
        return reward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("pit"))
        {
            Respawn();
        }
        else if (other.CompareTag("brick"))
        {
            bricksCollected++;
            collision.gameObject.SetActive(false);
            if(brickManager.GetActiveBrickCount() == 0)
            {
                brickManager.Done(10);
            }
        }
        else if (other.CompareTag("paddle"))
        {
            Vector3 velocity = rb.velocity;
            velocity.z = Mathf.Abs(velocity.z);
            velocity.x += 0.01f * paddle.speed * paddle.Direction;
            rb.velocity = velocity;
        }
        else if (other.CompareTag("top") || other.CompareTag("side"))
        {
            Transform tfm = other.transform;
            var velocity = rb.velocity;
            if (other.CompareTag("top"))
            {
				float direction = Mathf.Abs(velocity.x) > 0.0f ? Mathf.Sign(velocity.x) : -Mathf.Sign(rb.position.x);
                velocity.x = Mathf.Max(1f, Mathf.Abs(velocity.x)) * direction;
                velocity.z = Mathf.Abs(velocity.z) * -Mathf.Sign(tfm.position.z);
            }
            else if (other.CompareTag("side"))
            {
                velocity.z = Mathf.Max(1f, Mathf.Abs(velocity.z)) * Mathf.Sign(velocity.z);
                velocity.x = Mathf.Abs(velocity.x) * -Mathf.Sign(tfm.position.x);
            }
            rb.velocity = velocity;
        }
    }
}
