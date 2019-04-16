using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public GameObject pit;

    private float direction;
    public float Direction
    {
        get { return direction; }
        private set { direction = value; }
    }

    public void MovePaddle(float direction)
    {
        Vector3 paddlePos = transform.position;
        float xPos = transform.position.x + (direction * speed * Time.deltaTime);
        float width = (pit.transform.localScale.x - this.transform.localScale.x) * 0.5f;
        paddlePos.x = Mathf.Clamp(xPos, -width, width);
        transform.position = paddlePos;
        this.direction = direction;
    }

    public void Reset()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
        direction = 0;
    }
}
