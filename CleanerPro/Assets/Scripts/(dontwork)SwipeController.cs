using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    
    Vector2 swipe, tapPos;
    int minSwipe = 500;
    private bool isSwiping;

    private void Start()
    {
       
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    OnInput(Vector2Int.left);
        //if (Input.GetKeyDown(KeyCode.D))
        //    OnInput(Vector2Int.right);
        //if (Input.GetKeyDown(KeyCode.S))
        //    OnInput(Vector2Int.up);
        //if (Input.GetKeyDown(KeyCode.W))
        //    OnInput(Vector2Int.down);
        
            if (Input.GetMouseButton(0))
                {
                    isSwiping = true;
                    tapPos = Input.mousePosition;
                }
            //else if (Input.GetMouseButtonUp(0))
            //    ResetSwipe();
        
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPos = Input.mousePosition;
                }
                //else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                //    ResetSwipe();
            }                 

        CheckSwipe();
    }
    public void OnInput(Vector2Int direction)
    {
        Person.Instance.Move(direction);
    }

    private void CheckSwipe()
    {        
        swipe = Vector2.zero;

        if (isSwiping)
        {
            if (Input.GetMouseButton(0))
                swipe = (Vector2)Input.mousePosition - tapPos;
            else if (Input.touchCount > 0)
                swipe = Input.GetTouch(0).position - tapPos;
        }
        if (swipe.sqrMagnitude < minSwipe)
        {
            if (swipe.y > -0.5f && swipe.y < 0.5)
            {
                Debug.Log("tut");
                OnInput(swipe.x > 0 ? Vector2Int.right : Vector2Int.left);
                //SwipeEvent?.Invoke(swipe.x > 0 ? Vector2Int.right : Vector2Int.left);
            }
                //SwipeEvent?.Invoke(swipe.x > 0 ? Vector2Int.right : Vector2Int.left);
                //OnInput(swipe.x > 0 ? Vector2Int.right : Vector2Int.left);
           
                //SwipeEvent?.Invoke(swipe.y > 0 ? Vector2Int.up : Vector2Int.down);
                //OnInput(swipe.y > 0 ? Vector2Int.up : Vector2Int.down);


                ResetSwipe();
        }

    }
    private void ResetSwipe()
    {
        isSwiping = false;
        swipe = Vector2.zero;
        tapPos = Vector2.zero;
    }
}
