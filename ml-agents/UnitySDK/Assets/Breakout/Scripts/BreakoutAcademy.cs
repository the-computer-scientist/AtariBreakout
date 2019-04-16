//Every scene needs an academy script. 
//Create an empty gameObject and attach this script.
//The brain needs to be a child of the Academy gameObject.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class BreakoutAcademy : Academy
{
    public Ball ball;
    public Paddle paddle;
    public BrickManager brickManager;
    public float speedMultiplier;
    public int lives;
    public Text episodeNum;
    public Text stateVec;

    private int episode;

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
        UpdateEpisodesUI();
    }

    public override void AcademyStep()
    {
        UpdateStateUI();
    }

    private void UpdateEpisodesUI()
    {
        if(episodeNum)
        {
            episode = resetParameters.ContainsKey("episode") ? (int)resetParameters["episode"] : -1;
            episodeNum.text = episode >= 0 ? episode.ToString() : "";
        }
    }

    private void UpdateStateUI()
    {
        if(stateVec)
        {
            float[] state = ball.GetState().ToArray();
            var physics = state.Take(6).Select(num => String.Format("\t\t{0:0.0}\n", num));
            var bricks = state.Skip(6).Select((n,i) => String.Format("{1}{0}", n, i%brickManager.cols == 0 ? "\n\t\t" : ""));
            stateVec.text = "Ball PosX: \t" + physics.ElementAt(0)
                        + "Ball PosY:   \t" + physics.ElementAt(1)
                        + "Ball VelX:   \t" + physics.ElementAt(2)
                        + "Ball VelY:   \t" + physics.ElementAt(3)
                        + "Paddle PosX: " + physics.ElementAt(4)
                        + "Paddle VelX: " + physics.ElementAt(5)
                        + "Brick Status:" + String.Join(" ", bricks);
        }
    }
}
