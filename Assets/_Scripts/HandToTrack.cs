using UnityEngine;
using System.Collections;

public class HandToTrack : MonoBehaviour
{

    [SerializeField]
    Vector3 _rotation;

//    [SerializeField]
//    Transform _attachedStick;

    [SerializeField]
    Rigidbody _player;

    [SerializeField]
    float _speed;

    [SerializeField]
    SteamVR_ControllerManager _vrController;

	[SerializeField]
	LineRenderer _raycastLine;

    SteamVR_TrackedObject _trackedObject;

    SteamVR_Controller.Device _inputDevice;

	public delegate void HandleHairTriggerDown(Transform _rightHand,LineRenderer _line);
	public static event HandleHairTriggerDown HairTriggerDown;

	public delegate void HandleGripDown();
	public static event HandleGripDown GripDown;

	public delegate void HandleDPadUp();
	public static event HandleDPadUp DPadUp;


    Vector3 _previousPosition;
    Vector3 _currentPosition;
    float _updatePositionRate = 0.1f;
    float _updateTimer = 0.0f;


    bool _isUnderwater = false;
    bool _wasUnderwater = false;

    //TextMesh _textPrompt;


    Vector3 leftPos;
    Vector3 rightPos;

    //Transform _previousParent;

    // Use this for initialization
    void Start()
    {

        _trackedObject = GetComponent<SteamVR_TrackedObject>();
        if (_trackedObject == null)
        {
            Debug.Log("HandTrack.cs: Couldn't find a TrackedObject component");
        }

        if (_trackedObject)
            _inputDevice = SteamVR_Controller.Input((int)_trackedObject.index);

//        if (_attachedStick)
//        {
//            _textPrompt = _attachedStick.GetComponent<TextMesh>();
//        }

        leftPos  = _player.transform.TransformPoint(new Vector3(-1, 0, 0));
        rightPos = _player.transform.TransformPoint(new Vector3(1, 0, 0));

    }

    // Update is called once per frame
    void Update()
    {
        //_attachedStick.Rotate(_rotation * Time.deltaTime);

        //  String to understand the values

        if (_inputDevice != null)
        {
            //string finalText = "Velocity: " + _inputDevice.velocity.ToString();
            //_textPrompt.text = finalText;

			if(_raycastLine)
			{
				_raycastLine.SetPosition(0,transform.position);
				_raycastLine.SetPosition(1,transform.position + (transform.forward*30.0f));
			}

            if (_inputDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (HairTriggerDown != null)
                    HairTriggerDown(transform,_raycastLine);
            }

			if (_inputDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
			{
				if (DPadUp != null)
					DPadUp();
			};

            if (_inputDevice.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip) ||
                _inputDevice.GetTouch(Valve.VR.EVRButtonId.k_EButton_Grip))
            {

//				leftPos = transform.TransformPoint (new Vector3 (-1, 0, 0));
//				rightPos = transform.TransformPoint (new Vector3 (1, 0, 0));

				float left = 0, right = 0;

				if (_vrController) {
					if (gameObject == _vrController.left)
						left = 1;
					else if (gameObject == _vrController.right)
						right = 1;
				}

				_player.GetComponent<KeyboardMovement> ().DoMovement (left, right);

            }
//
//            if (_inputDevice.GetPress(Valve.VR.EVRButtonId.k_EButton_DPad_Down) ||
//                 _inputDevice.GetTouch(Valve.VR.EVRButtonId.k_EButton_DPad_Down))
//                _attachedStick.GetComponentInChildren<TextMesh>().color = Color.red;
//            else
//                _attachedStick.GetComponentInChildren<TextMesh>().color = Color.white;

            // Calculate movement and stuff
            CheckMovement();
            CalculateDelta();

        }

        

    }

    void CheckMovement()
    {
        float yWorldPosition = transform.position.y;//transform.parent.TransformPoint(transform.position).y;
        if (yWorldPosition <= 0.3f)
        {
            _isUnderwater = true;

            //_textPrompt.text = "Touching water!";
            _previousPosition = transform.position;
            _updateTimer = Time.time + _updatePositionRate;
        }
        else
        {
            _isUnderwater = false;
            //_textPrompt.text = "Out of water!";

        }
    }

    void CalculateDelta()
    {
        if (_isUnderwater && !_wasUnderwater)
        {

            _previousPosition = transform.position;

            //  Projected on the z plane
            //float deltaPosition = (transform.position - _previousPosition).magnitude;



            //   We have the delta, multiply by a factor and then add force to the boat

            //Vector3 positionToAddForce = Vector3.zero;

            //if (_vrController)
            //{
            //    if (_trackedObject == _vrController.left)
            //        positionToAddForce = leftPos;
            //    else if (_trackedObject == _vrController.right)
            //        positionToAddForce = rightPos;

            //    //_player.AddForceAtPosition(_player.transform.forward * deltaPosition * _speed * Time.deltaTime, positionToAddForce);

            //    _player.transform.position += deltaPosition * _speed * Time.deltaTime * _player.transform.forward;
            //}
            ////_player.AddForceAtPosition(transform.forward * (intent * right), rightPos);

            _wasUnderwater = true;

        }

        else if (!_isUnderwater && _wasUnderwater)
        {
			float deltaPosition = (transform.position - _previousPosition).sqrMagnitude;

            if (_vrController)
            {
              

                //_player.AddForceAtPosition(_player.transform.forward * deltaPosition * _speed * Time.deltaTime, positionToAddForce);

                //_player.AddForce(deltaPosition * _speed * Time.deltaTime * _player.transform.forward);

                Vector3 positionToAddForce = Vector3.zero;

                if (gameObject == _vrController.left)
                    positionToAddForce = leftPos;
				else if (gameObject == _vrController.right)
                    positionToAddForce = rightPos;

                _player.AddForceAtPosition(_player.transform.forward * deltaPosition * _speed , leftPos);

                Debug.Log("Delta position was: " + deltaPosition);


				//_player.AddForce(
                _wasUnderwater = false;
            }

            //_player.AddForceAtPosition(transform.forward * (intent * right), rightPos);
        }

    }
}
