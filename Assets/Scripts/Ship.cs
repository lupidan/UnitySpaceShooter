using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    private bool isMouseControlEnabled = false;

    

	// Use this for initialization
	void Start ()
    {
        this.isMouseControlEnabled = GamePlayerPrefs.IsMouseControlEnabled;
	}

    // Update is called once per frame
    void Update()
    {
        if (GamePlayerPrefs.IsMouseControlEnabled)
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
        this.transform.position = newPosition;
    }

    private void UpdateMouseControls()
    {
        Vector3 positionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionVector.z = this.transform.position.z;
        this.transform.position = positionVector;
        
    }



    
}
