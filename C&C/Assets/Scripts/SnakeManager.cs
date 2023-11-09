using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public LogicScript logic;
    public GameObject camaraTracer;
    [SerializeField] float distanceBetween = 0.2f;
    [SerializeField] float speed = 280f;
    [SerializeField] float turnSpeed = 180f;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> snakeBody = new List<GameObject>();
    private bool rewindeTime = false;

    float countUp = 0;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        CreateBodyParts();
    }
    private void Update()
    {
       if (GameObject.FindGameObjectWithTag("Hook").GetComponent<TimeController>().positions.Count == 0)
        {
           logic.resartGame();
       }

    }

    void FixedUpdate()
    {
        if (bodyParts.Count > 0 && !Input.GetKey(KeyCode.W))
        {
            CreateBodyParts();
        }
        if (!Input.GetKey(KeyCode.W))
        {
            SnakeMovement();
        }
    }

    void SnakeMovement()
    {
        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.fixedDeltaTime;
        if (Input.GetAxis("Horizontal") != 0)
        {
            RotateObjectAroundZAxis(snakeBody[0]);
            //snakeBody[0].transform.Rotate(new Vector3(0,0, -turnSpeed * Time.fixedDeltaTime * Input.GetAxis("Horizontal")));
        }
        if (snakeBody.Count > 1 )
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                    MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                    snakeBody[i].transform.position = markM.markerList[0].position;
                    snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                    markM.markerList.RemoveAt(0);
            }
        }   
    }
    private void RotateObjectAroundZAxis(GameObject obj)
    {
        if (Input.GetKey(KeyCode.A) && obj.transform.rotation.z < -0.1f)
        {
            obj.transform.Rotate(Vector3.forward * turnSpeed * Time.fixedDeltaTime);
            //Debug.Log(obj.transform.rotation);
        }
        if (Input.GetKey(KeyCode.D) && obj.transform.rotation.z > -0.98f)
        {
            obj.transform.Rotate(Vector3.back * turnSpeed * Time.fixedDeltaTime);
            //Debug.Log(obj.transform.rotation);
        }

    }
    void CreateBodyParts()
    {
        if (snakeBody.Count == 0)
        {
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            temp1.layer = 3;
            temp1.tag = "Hook";
            if (!temp1.GetComponent<MarkerManager>())
            {
                temp1.AddComponent<MarkerManager>();   
            }
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            if (!temp1.GetComponent<CircleCollider2D>())
            {
                temp1.AddComponent<CircleCollider2D>();
                temp1.GetComponent<CircleCollider2D>().radius = 6;
                temp1.GetComponent<CircleCollider2D>().offset = new Vector2(3.5f, 0);
            }
            if (!temp1.GetComponent<TimeController>())
            {
                temp1.AddComponent<TimeController>();
            }

            if (!camaraTracer.GetComponent<MarkerManager>())
            {
                camaraTracer.AddComponent<MarkerManager>();
            }
            if (!camaraTracer.GetComponent<Rigidbody2D>())
            {
                camaraTracer.AddComponent<Rigidbody2D>();
                camaraTracer.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            if (!camaraTracer.GetComponent<TimeController>())
            {
                camaraTracer.AddComponent<TimeController>();
            }

            snakeBody.Add(camaraTracer);
            snakeBody.Add(temp1);
            bodyParts.RemoveAt(0);
        }

        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp == 0)
        {
            markM.ResetMarkerList();
        }
        countUp += Time.deltaTime;
        
        if (countUp >= distanceBetween)
        {
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation, transform);
            if (!temp.GetComponent<MarkerManager>())
            {
                temp.AddComponent<MarkerManager>();
            }
            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            if (!temp.GetComponent<TimeController>())
            {
                temp.AddComponent<TimeController>();
            }
            snakeBody.Add(temp);
            //bodyParts.RemoveAt(0);
            temp.GetComponent<MarkerManager>().ResetMarkerList();
            countUp = 0;
        }   
    }
}
