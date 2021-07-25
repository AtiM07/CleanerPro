using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public Rigidbody2D personRB;
    public float speed = 1; //скорость движения
   
    bool isTraveling; //движется ли персонаж
    Vector2 travel, //направление движения
            nextPosition;

    int minSwipe = 500; //для распознавания свайпа
    Vector2 swipeLastPos, swipePos, swipe;

    void Update()
    {
        //перемещение объекта
        if (isTraveling)
            personRB.velocity = speed * travel;

        //достиг ли препятствия
        if (nextPosition != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, nextPosition) < 1f)
            {
                isTraveling = false;
                travel = personRB.transform.position;
                nextPosition = Vector2.zero;
            }

        }

        //если персонаж уже движется, то код не выполняется дальше, пока объект не достигнет препятсвия
        if (isTraveling) return;

        if (Input.GetMouseButton(0))
        {
            swipePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipeLastPos != Vector2.zero)
            {
                swipe = swipePos - swipeLastPos;

                if (swipe.sqrMagnitude < minSwipe) 
                    return;

                swipe.Normalize();

                if (swipe.x > -0.5f && swipe.x < 0.5)
                {
                    SetDestination(swipe.y > 0 ? Vector2Int.down : Vector2Int.up);
                }

                if (swipe.y > -0.5f && swipe.y < 0.5)
                {
                    SetDestination(swipe.x > 0 ? Vector2Int.right : Vector2Int.left);
                }
            }

            swipeLastPos = swipePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipeLastPos = Vector2.zero;
            swipe = Vector2.zero;
        }


    }

    void SetDestination (Vector2Int direction)
    {
        Person.Instance.Move(direction);
    }

}
