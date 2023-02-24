using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float speed => moveSpeed;
    private Vector3 startPosition;
    [SerializeField] private float maxDis;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving)
        {
            Move();
        }
    }
    public void StartMoving()
    {
        isMoving = true;
    }
    public void StopMoving()
    {
        isMoving = false;
    }
    
    private void Move()
    {
        moveSpeed += Time.deltaTime * .02f;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if(transform.position.x < maxDis)
        {
            transform.position = startPosition;
        }
    }
}
