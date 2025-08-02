using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomChangeTransition : MonoBehaviour
{
    public UnityEvent TransitionEvent;
    public bool isMoving = false;
    public RectTransform black;
    public float screenLeftPosition, screenRightPosition;
    public float speed, waitTime;
    float xPos;
    public float threshold;
    void OnEnable()
    {
        StartTransition();
    }
    void Update()
    {
        if (isMoving)
        {
            float target = screenRightPosition;
            if (xPos <= -threshold)
            {
                target = 0;
            }
            xPos = Mathf.Lerp(xPos, target, speed * Time.deltaTime);

            if (xPos >= -threshold && xPos <= threshold)
            {
                Debug.Log("reached center");
                TransitionEvent?.Invoke();
            }
            
            if (xPos >= screenRightPosition - threshold)
                {
                    isMoving = false;
                }
        }
        else
        {
            xPos = screenLeftPosition;
        }

        black.anchoredPosition = new Vector2(xPos, 0);
    }

    public void StartTransition()
    {
        isMoving = true;
        Time.timeScale = 1f;
    }
}
