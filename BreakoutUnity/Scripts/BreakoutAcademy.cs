//Every scene needs an academy script. 
//Create an empty gameObject and attach this script.
//The brain needs to be a child of the Academy gameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BreakoutAcademy : Academy
{
    public Ball ball;
    public Paddle paddle;
    public BrickManager brickManager;
    public float speedMultiplier;
    public int lives;

    public override void InitializeAcademy()
    {
        paddle.speed *= speedMultiplier;
        ball.speed *= speedMultiplier;
    }

    public override void AcademyReset()
    {
        brickManager.Reset();
        ball.Reset(lives);
        ball.Respawn();
    }

    public override void AcademyStep()
    {
        
    }
}
