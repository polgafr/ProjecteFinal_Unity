using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour {

	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;


	//public float xMinLimit = -20f;
	//public float xMaxLimit = 80f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = 5.0f;
	public float distanceMax = 5.0f;

	private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;




	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public bool followBehind = true;
	public float rotationDamping = 10.0f;





	// Use this for initialization
	void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x =  angles.y;
		y =  angles.x;

		//GetComponent<Rigidbody> g = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>() != null)
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	void LateUpdate () 
	{
		if (target) 
		{
			x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);


			Quaternion rotation = Quaternion.Euler(y, x, 0);

			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);

			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) 
			{
				distance -=  hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}


	void Update () {
		Vector3 wantedPosition;
		if(followBehind)
			wantedPosition = target.TransformPoint(0, height, -distance);
		else
			wantedPosition = target.TransformPoint(0, height, distance);

		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

		if (smoothRotation) {
			Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		}
		else transform.LookAt (target, target.up);
	}
}
