using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    [SerializeField]
    Camera _mainCamera;

	void Update ()
	{
        if(_mainCamera)
            transform.LookAt(_mainCamera.transform.position, Vector3.up);
	}
}
