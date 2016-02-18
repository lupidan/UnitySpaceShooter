using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputVector *= 5.0f;
        transform.position += inputVector * Time.deltaTime;
	}
}
