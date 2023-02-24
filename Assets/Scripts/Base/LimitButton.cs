using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitButton : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.instance != null)
        {
            if(GameManager.instance.IsGameOver)
            {
                transform.position = new Vector2(Mathf.Clamp(transform.position.x,-2.2f,2.2f),transform.position.y);
            }
        }
    }
}