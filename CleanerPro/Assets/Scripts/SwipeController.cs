using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    bool swipe, swipeLeft, swipeRight, swipeUp, swipeDown;
    Vector2 startSwipe, swipeDelta, position;
    bool isDragging = false;
    public Transform person;

    void Update()
    {
        swipe = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        
        //input for pc
        if (Input.GetMouseButtonDown(0))
        {
            swipe = true;
            isDragging = true;
            startSwipe = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }

        //input for mobile
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                swipe = true;
                isDragging = true;
                startSwipe = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

                isDragging = false;
                Reset();
            }
        }
        //distance
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startSwipe;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startSwipe;
        }

        //border
        if (swipeDelta.magnitude > 125)
        {

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y)) //left or right
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else //up or down
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            Reset();
        }
        Swipe();

    }
    void Reset()
    {
        startSwipe = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    void Swipe()
    {
            if (swipeDown)
                position += Vector2.down;
            if (swipeLeft)
                position += Vector2.left;
            if (swipeRight)
                position += Vector2.right;
            if (swipeUp)
                position += Vector2.up;

           // if (Vector2.Distance(person.transform.position, GameObject.FindGameObjectWithTag("block").transform.position) >1f * Time.deltaTime)
            person.transform.position = Vector2.MoveTowards(person.transform.position, position, 1f * Time.deltaTime);
       
    }
}
