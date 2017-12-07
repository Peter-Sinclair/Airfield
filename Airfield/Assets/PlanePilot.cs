using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour {

	public float speed = 60.0f;

	// Use this for initialization
	void Start () {
		Debug.Log("Plane pilot script added to: " + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {

		// Transform is the relative position, Vector 3 is the absolute position. If the plane is
		// pointing down, it will still be objectively pointing up. 
		//Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f;
		//float bias = 0.5f;
		//Camera.main.transform.position = (Camera.main.transform.position * bias) + (moveCamTo * (1.0f - bias));
		//Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);


		// Transform.forward is a unit vector that points out the front of the plane. 
		// As the plane tilts up, the y value will decrease. The inverse is also true. 
		transform.position += transform.forward * Time.deltaTime * speed;

		speed -= transform.forward.y * Time.deltaTime * 35.0f;

		if (speed < 20.0f)
		{
			speed = 20.0f;
		}

        if (speed > 80.0f)
        {
            speed = 80.0f;
        }

		// If no game object is specified, the compiler will assume that any objects are referencing the object
		// it is attached to. In this case, it is PlaneWhole.
		transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

		// Terrain.activeTerrain is a handle for the current active terrain. This has a height map that corresponds to a position
		// in space, which we access by using SampleHeight. 
		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

		// Any code placed here can make a reaction based on a collision with the ground, such as losing speed, explosion etc.
		if (terrainHeightWhereWeAre > transform.position.y - 10)
		{
			// You cannot explicitly change the variable of a Vector 3. Therefore, a new Vector 3 must be created.
			transform.position = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
		}
	}
}
