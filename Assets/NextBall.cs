using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("NEXTBALL");
        //if (collision.gameObject.GetComponent<pathScript>() != null)
        //{
        GameManager.instance.NextBall(collision.gameObject);
        //}
    }
    // Update is called once per frame
    void Update () {
	
	}
}
