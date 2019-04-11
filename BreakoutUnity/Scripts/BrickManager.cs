using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public int cols;
    public float space;
    public GameObject brickPrefab;

    private bool done;
    private float score;
    private List<GameObject> bricks;
    private readonly List<Color> colors = new List<Color> {
        new Color(186f/255, 51f/255, 56f/255),
        new Color(184f/255, 87f/255, 45f/255),
        new Color(164f/255, 103f/255, 37f/255),
        new Color(145f/255, 147f/255, 32f/255),
        new Color(59f/255, 146f/255, 56f/255),
        new Color(50f/255, 48f/255, 188f/255),
    };

    // Start is called before the first frame update
    void Start()
    {
        bricks = new List<GameObject>();
        Vector3 brickDim = brickPrefab.transform.localScale;
        int rows = colors.Count;
        for (int z = 0; z < rows; z++)
        {
            float zPos = (z - 0.5f * (rows - 1)) * (brickDim.z + space);
            for (int x = 0; x < cols; x++)
            {
                float xPos = (x - 0.5f * (cols - 1)) * (brickDim.x + space);
                Vector3 pos = transform.position + new Vector3(xPos, 0, zPos);
                GameObject brick = Instantiate(brickPrefab, pos, Quaternion.identity);
                brick.GetComponent<MeshRenderer>().material.color = colors[rows - 1 - z];
                bricks.Add(brick);
            }
        }
    }

    // Update is called once per frame
    public List<float> GetBricksStatus()
    {
        List<float> status = new List<float>();
        bricks.ForEach((brick) => status.Add(brick.active ? 1.0f : 0.0f));
        return status;
    }

    public float GetReward()
    {
        float reward = score;
        score = 0;
        return reward;
    }

    public int GetActiveBrickCount()
    {
        return bricks.FindAll((brick) => brick.active).Count;
    }

    public void Done(float reward)
    {
        done = true;
        score = reward;
    }

    public bool GetDone()
    {
        bool result = done;
        done = false;
        return result;
    }

    public void Reset()
    {
        bricks.ForEach((brick) => brick.SetActive(true));
        done = false;
        score = 0;
    }
}
