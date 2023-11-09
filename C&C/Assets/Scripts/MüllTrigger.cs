using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MüllTrigger : MonoBehaviour
{
    public LogicScript logic;
    public float minHeight = 0.5f; // Minimum height of the object
    public float maxHeight = 2.0f; // Maximum height of the object
    public float moveSpeed = 1.0f; // Speed of the up and down movement
    private bool isMovingUp = true;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        StartCoroutine(ChangeHeight());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ChangeHeight()
    {
        while (true)
        {
            waitTime = Random.Range(minHeight, maxHeight);
            yield return new WaitForSeconds(waitTime);

            if (isMovingUp)
            {
                while (transform.position.y < maxHeight)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
                    yield return null;
                }
                isMovingUp = false;
            }
            else
            {
                while (transform.position.y > minHeight)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
                    yield return null;
                }
                isMovingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.addScore(1);
            this.gameObject.SetActive(false);
        }
    }
}
