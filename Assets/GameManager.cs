using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // some global vars
    public List<GameObject> pathLeft = new List<GameObject>();
    public List<GameObject> pathRight = new List<GameObject>();
    public Material onMaterialGreen;
    public Material onMaterialPurple;
    public Material offMaterial;
    public static GameManager instance = null;
    bool leftMovingUp = false;          //is left hand moving up?
    bool rightMovingUp = false;         //is right hand moving up?
    public GameObject nextBallLeft;     //next left ball that should be displayed
    public GameObject nextBallRight;    //next right ball that should be displayed

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        nextBallLeft = pathLeft[0];         //initialize left ball that should be hit to first (lowest) ball
        nextBallRight = pathRight[0];       //initialize right ball that should be hit to first (lowest) ball
        Debug.Log("Hello");
    }

    // NextBall is called every time a collision occurs. 
    public void NextBall(GameObject gameObject)
    {
        if (gameObject != null)
        {
            //determine which side left/right was hit
            int leftIndex = pathLeft.IndexOf(gameObject);
            int rightIndex = pathRight.IndexOf(gameObject);
            bool hitRight = true;
            if (leftIndex != -1)        //gameObject is in pathLeft aka hitLeft
                hitRight = false;

            //used in calculations to find previous ball
            int prevInc = 1;
            if (!hitRight && leftMovingUp)          //if hitLeft and leftMovingUp, then prevBall is at index - 1
                prevInc = -1;
            else if (hitRight && rightMovingUp)     //if hitRight and rightMovingUp, then prevBall is at index - 1
                prevInc = -1;

            //make previous ball disappear
            GameObject prevBall;
            if (hitRight)
                prevBall = pathRight[rightIndex + prevInc];
            else
                prevBall = pathLeft[leftIndex + prevInc];
            prevBall.GetComponent<Renderer>().material = offMaterial;

            //change directions if at edges
            if (hitRight)                               //right hit
            {
                if (rightIndex == pathRight.Count - 1)  //at top of path
                    rightMovingUp = false;
                else if (rightIndex == 0)               //at bottom of path
                    rightMovingUp = true;
            }
            else                                        //left hit
            {
                if (leftIndex == pathLeft.Count - 1)    //at top of path
                    leftMovingUp = false;
                else if (leftIndex == 0)                //at bottom of path
                    leftMovingUp = true;
            }

            //set next ball and set color
            if (hitRight)           //right side of path
            {
                /*
                 * THIS IS PROBABLY THE BUGGY PART
                 */

                //if moving up and ball that is 2 positions above is valid
                if (rightMovingUp && (rightIndex + 2 < pathRight.Count))
                {
                    nextBallRight = pathRight[rightIndex + 2];
                    nextBallRight.GetComponent<Renderer>().material = onMaterialGreen;
                }
                //if moving down and ball that is 2 positions below is valid
                else if (!rightMovingUp && (rightIndex - 2 >= 0))
                {
                    nextBallRight = pathRight[rightIndex - 2];
                    nextBallRight.GetComponent<Renderer>().material = onMaterialPurple;
                }
                //if at the top, lines 60-73 set direction to be down (!rightMovingUp). 
                //so the code enters the else if in line 89 and displays the ball 2 away from the top ball
                //but we also want to display the ball 1 away from the top ball:
                if (rightIndex == pathRight.Count - 1)
                    pathRight[rightIndex - 1].GetComponent<Renderer>().material = onMaterialPurple;
                else if (rightIndex == 0)
                    pathRight[1].GetComponent<Renderer>().material = onMaterialGreen;
            }
            else                    //left side of path
            {
                if (leftMovingUp && (leftIndex + 2 < pathLeft.Count))
                {
                    nextBallLeft = pathLeft[leftIndex + 2];
                    nextBallLeft.GetComponent<Renderer>().material = onMaterialGreen;
                }
                else if (!leftMovingUp && (leftIndex - 2 >= 0))
                {
                    nextBallLeft = pathLeft[leftIndex - 2];
                    nextBallLeft.GetComponent<Renderer>().material = onMaterialPurple;
                }
                if (leftIndex == pathLeft.Count - 1)
                    pathLeft[leftIndex - 1].GetComponent<Renderer>().material = onMaterialPurple;
                else if (leftIndex == 0)
                    pathLeft[1].GetComponent<Renderer>().material = onMaterialGreen;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
