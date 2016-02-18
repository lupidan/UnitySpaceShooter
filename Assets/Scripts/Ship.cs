using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    public bool mouseControls = false;

    

	// Use this for initialization
	void Start ()
    {
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.mousePresent && mouseControls)
        {
            UpdateMouseControls();
        }
        else
        {
            UpdateKeyboardControls();
        }
        
    }

    private void UpdateKeyboardControls()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 newPosition = transform.position + (inputVector * 5.0f * Time.deltaTime);
        SetPosition(newPosition);
    }

    private void UpdateMouseControls()
    {
        Vector3 positionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetPosition(positionVector);
    }



    
}
