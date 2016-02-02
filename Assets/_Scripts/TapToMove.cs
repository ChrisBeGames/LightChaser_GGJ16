using UnityEngine;
using System.Collections;

public class TapToMove : MonoBehaviour
{

    //flag to check if the user has tapped / clicked. 
    //Set to true on click. Reset to false on reaching destination
    private bool flag = false;
    //destination point
    private Vector3 endPoint;
	//Current indicator
	private GameObject indicator;

	private Rigidbody rigid;

	private bool _isOrbitTriggered=false;

	//	Orbit
	[SerializeField]
	Transform targetOrbit;

	[SerializeField]
	float orbitDistance = 5.0f;

	[SerializeField]
	float orbitDegreesPerSec = 100.0f;

	[SerializeField]
	float minDistanceToStartOrbit= 5.5f;

	//	CB
	[SerializeField][Tooltip("Min. 1")]
	float _acceleration;

	[SerializeField]
	float _maxSpeed;

	[SerializeField]
	float _rayDistance;

	[SerializeField]
	Transform _playerTransform;

	public float indicatorTimer;

	[SerializeField]
	GameObject _targetIndicatorPrefab;

	[SerializeField]
	Camera _mainCamera;

	[SerializeField]
	LineRenderer _raycastLine;

	void Start(){
		//Set the rigid if we haven't yet
		if (!rigid)
			rigid = GetComponent<Rigidbody> ();

		HandToTrack.HairTriggerDown += HandleHairTriggerDown;
		HandToTrack.DPadUp += HandleDPadUp;
	}

    void Update()
    {
		if (!SteamVR.active)
		{
			Vector3 mousePos = Input.mousePosition;

			//Create a Ray on the tapped / clicked position
			Ray ray = _mainCamera.ScreenPointToRay(mousePos);

			//declare a variable of RaycastHit struct
			RaycastHit hit;

			//Draw the line
			Vector3 start = ray.origin - Vector3.up * 0.5f;
			Vector3 end = (ray.origin + (ray.direction * _rayDistance)) - Vector3.up * 0.5f;

			_raycastLine.SetPosition(0, start);

			bool rayHit = Physics.Raycast(ray, out hit);

			if (rayHit)
			{
				_raycastLine.SetPosition(1, hit.point);
			}
			else
			{
				_raycastLine.SetPosition(1, end);

			}

			//check if the screen is touched / clicked   
			if (Input.GetMouseButtonDown(0))
			{

				if (rayHit)
					SetDestination(hit.point);

				//CB
				//Check if we are too far before trying to go to that point
				else
				{
					SetDestination(ray.origin + ray.direction * _rayDistance);
				}

				_isOrbitTriggered = false;
			
			}

			if(Input.GetMouseButtonDown(1))
			{
				_isOrbitTriggered = true;
				flag = false;
			}
		}

        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
		if (flag && !_isOrbitTriggered)
        {
			//Move, if speed isn't above max
			rigid.AddForce(_acceleration * (endPoint - transform.position));

			rigid.velocity = Vector3.ClampMagnitude (rigid.velocity, _maxSpeed);

			//Are we close enough to stop?
			if ((transform.position - endPoint).sqrMagnitude <= 1)
			{
				flag = false;
				Destroy (indicator);
			}

        }

		if (_isOrbitTriggered)
		{
			Orbit();
		}

		//Stop movement
		if (indicator == null) {
			flag = false;

			//Slow
			rigid.velocity = rigid.velocity - (rigid.velocity / _acceleration);

			if (rigid.velocity.sqrMagnitude <= 1)
				rigid.velocity = Vector3.zero;
		}
    }

	/// <summary>
	/// Create an indicator at destination target and flag for movement.
	/// </summary>
	/// <param name="target">Target destination position.</param>
	void SetDestination(Vector3 target)
	{
		//Destroy any old indicator
		if (indicator)
			Destroy (indicator);

		//set a flag to indicate to move the gameobject
		flag = true;
		//save the click / tap position
		endPoint = target;

		_isOrbitTriggered = false;

		//Create an indicator with a timed death
		Destroy(indicator = Instantiate (_targetIndicatorPrefab, endPoint, Quaternion.identity) as GameObject, indicatorTimer);
	}

	//  Event handlers
	void HandleHairTriggerDown(Transform _rightHand,LineRenderer _line)
	{
		if (_rightHand)
		{
			Ray ray = new Ray();
			ray.direction   = _rightHand.forward;
			ray.origin      = _rightHand.position;

			RaycastHit hit;

			//Check if the ray hits any collider
			if (Physics.Raycast(ray, out hit))
			{
				_line.SetPosition(1, hit.point);
				SetDestination(hit.point);
			}
			//CB
			//Check if we are too far before trying to go to that point
			else
			{
				_line.SetPosition(1, ray.origin + ray.direction * _rayDistance);
				SetDestination(ray.origin + ray.direction * _rayDistance);
			}

			_isOrbitTriggered = false;
		}
	}

	void HandleDPadUp()
	{
		_isOrbitTriggered = true;
		flag = false;
	}

	void Orbit()
	{
		if (targetOrbit != null)
		{
			Vector3 pixieToTarget= (transform.position - targetOrbit.position);

			FindObjectOfType<PixAudio> ().Idle ();

			// Keep us at orbitDistance from target     // Offset...
			if (pixieToTarget.magnitude > minDistanceToStartOrbit)
			{
				//Move, if speed isn't above max
				rigid.AddForce(_acceleration * 2.0f* (targetOrbit.position - transform.position));

				rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, _maxSpeed);

			}

			else
			{
				transform.position = targetOrbit.position + pixieToTarget.normalized * orbitDistance;
				transform.RotateAround(targetOrbit.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);

			}
		}
	}
}

//using UnityEngine;
//using System.Collections;
//
//public class TapToMove : MonoBehaviour
//{
//
//	//flag to check if the user has tapped / clicked. 
//	//Set to true on click. Reset to false on reaching destination
//	private bool flag = false;
//	//destination point
//	private Vector3 endPoint;
//	//Current indicator
//	private GameObject indicator;
//
//	private Rigidbody rigid;
//
//
//	// CB
//	[SerializeField][Tooltip("Min. 1")]
//	float _acceleration;
//
//	[SerializeField]
//	float _maxSpeed;
//
//	[SerializeField]
//	float _rayDistance;
//
//	[SerializeField]
//	Transform _playerTransform;
//
//	[SerializeField]
//	GameObject _targetIndicatorPrefab;
//
//
//	void Start(){
//		//Set the rigid if we haven't yet
//		if (!rigid)
//			rigid = GetComponent<Rigidbody> ();
//
//		HandToTrack.HairTriggerDown += HandleHairTriggerDown;
//	}
//
//	void Update()
//	{
//		//check if the screen is touched / clicked   
//		if (Input.GetMouseButtonDown(0))
//		{
//			//declare a variable of RaycastHit struct
//			RaycastHit hit;
//			//Create a Ray on the tapped / clicked position
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//			//Check if the ray hits any collider
//			if (Physics.Raycast (ray, out hit))
//			{
//				SetDestination(hit.point);
//			}
//			//CB
//			//Check if we are too far before trying to go to that point
//			else
//			{
//				SetDestination(ray.origin + ray.direction * _rayDistance);
//			}
//
//		}
//		//check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
//		if (flag)
//		{
//			//Move, if speed isn't above max
//			rigid.AddForce(_acceleration * (endPoint - transform.position));
//
//			rigid.velocity = Vector3.ClampMagnitude (rigid.velocity, _maxSpeed);
//
//			//Are we close enough to stop?
//			if ((transform.position - endPoint).sqrMagnitude <= 1)
//			{
//				flag = false;
//				Destroy (indicator);
//			}
//
//		}
//		//Stop movement
//		if (indicator == null) {
//			flag = false;
//
//			//Slow
//			rigid.velocity = rigid.velocity - (rigid.velocity / _acceleration);
//
//			if (rigid.velocity.sqrMagnitude <= 1)
//				rigid.velocity = Vector3.zero;
//		}
//	}
//
//	/// <summary>
//	/// Create an indicator at destination target and flag for movement.
//	/// </summary>
//	/// <param name="target">Target destination position.</param>
//	void SetDestination(Vector3 target)
//	{
//		//Destroy any old indicator
//		if (indicator)
//			Destroy (indicator);
//
//		//set a flag to indicate to move the gameobject
//		flag = true;
//		//save the click / tap position
//		endPoint = target;
//
//		//Create an indicator with a timed death
//		Destroy(indicator = Instantiate (_targetIndicatorPrefab, endPoint, Quaternion.identity) as GameObject, 3.0f);
//	}
//
//	//  Event handlers
//	void HandleHairTriggerDown(Transform _rightHand)
//	{
//		if (_rightHand)
//		{
//			Ray ray = new Ray();
//			ray.direction = _rightHand.forward;
//			ray.origin = _rightHand.position;
//
//			RaycastHit hit;
//
//			Debug.DrawRay(ray.origin, ray.direction, Color.blue, 2.0f);
//
//			//Check if the ray hits any collider
//			if (Physics.Raycast(ray, out hit))
//			{
//				SetDestination(hit.point);
//			}
//			//CB
//			//Check if we are too far before trying to go to that point
//			else
//			{
//				SetDestination(ray.origin + ray.direction * _rayDistance);
//			}
//		}
//	}
//}