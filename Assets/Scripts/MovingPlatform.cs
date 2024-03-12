using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //speed of the platform
    public int startingPoint; //starting position of the platform
    public Transform[] points; //array of points the platform needs to move to 

    private int i;
    private void Start()
    {
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        //check distance of platform and point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length)
            {
                i = 0; // reset index
            }
        }
        //moviing the platform to the point position with i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }


    //private void OnCollisionEnter2D(Collision2D collision)
   
    //{
    //    collision.transform.SetParent(transform);


    //}
    
    // private void OnCollisionExit2D(Collision2D collision)
    //{
    //    collision.transform.SetParent(null);
    //}












}
