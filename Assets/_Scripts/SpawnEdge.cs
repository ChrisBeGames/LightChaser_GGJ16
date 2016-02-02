using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**Lazy af. */
public class SpawnEdge : MonoBehaviour {

	public GameObject edgePrefab, buoyPrefab;
	public float prefabWidth, radiusToPopulate;
	public int spacesBetweenBuoys;

	public List<GameObject> spawnedBuoys = new List<GameObject> ();

	void Start () {
//		for (int i = 0; i < (int)(Mathf.PI * 2 * radiusToPopulate / prefabWidth); i++) {
//			Instantiate(edgePrefab, new Vector3(
//		}
	
		float degreeOffset = 360.0f / (Mathf.PI * 2 * radiusToPopulate / prefabWidth);

		Vector3 tempPosition = Vector3.zero;

		int buoySpaceCount = 0;

		for (float currentDegree = 0; currentDegree < 360; currentDegree += degreeOffset) {

			tempPosition.x= Mathf.Cos (currentDegree) * radiusToPopulate;
			tempPosition.z= Mathf.Sin (currentDegree) * radiusToPopulate;

			var tempObj = (GameObject)Instantiate (edgePrefab);
			tempObj.transform.position = tempPosition + transform.position;
			tempObj.transform.rotation = Quaternion.LookRotation(-(transform.position - tempObj.transform.position));
			tempObj.transform.SetParent (transform);

			if (buoyPrefab) {
				if (buoySpaceCount >= spacesBetweenBuoys){
					var buoy = (GameObject)Instantiate(buoyPrefab, tempObj.transform.position, tempObj.transform.rotation);
					buoy.transform.SetParent (tempObj.transform);
					spawnedBuoys.Add (buoy);
					buoySpaceCount = 0;
				}
				buoySpaceCount++;
			}
		}
	}
}
