using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool rewindeTime = false;
    public bool isRewinding = false;
    public List<Vector3> positions;
    public List<Quaternion> rotation;

    // Start is called before the first frame update
    void Start()
    {
        isRewinding = false;
        positions = new List<Vector3>();
        rotation = new List<Quaternion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rewindeTime = true;
        }
        if (rewindeTime)
        {
            StartRewinde();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            for (int i = 0; i < 6; i++)
            {
                Rewind();
            }
        }
        else
        {
            Record();
        }
    }

    void StartRewinde()
    {
        isRewinding = true;
    }
    void Rewind()
    {
        if (transform.position != null)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        if (transform.rotation != null)
        {
            transform.rotation = rotation[0];
            rotation.RemoveAt(0);
        }

    }
    void Record()
    {
        positions.Insert(0, transform.position);
        rotation.Insert(0, transform.rotation);
    }
}
