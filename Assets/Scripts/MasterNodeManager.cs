using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterNodeManager : MonoBehaviour {

	public GameObject[] organChildren;
	private bool[] collideWithOtherOrganInChildren;

	void Start() {
        collideWithOtherOrganInChildren = new bool[organChildren.Length];
	}

	public void ConvertContainer2Children(List<GameObject> organObjectList) {
		organObjectList.Remove(gameObject);
		for (int i = 0; i < organChildren.Length; i++) {
			organObjectList.Add(organChildren[i]);
        }
	}

	public void UpdateCollideWith(GameObject organ, bool status) {
		Debug.Log("UpdateCollideWith " + organ.name + " " + status);
		for (int i = 0; i < organChildren.Length; i++) {
			if (organ == organChildren[i])
			{
				collideWithOtherOrganInChildren[i] = status;
			}
        }
	}

	public bool GetCollideWithOtherOrgan() {
		for (int i = 0; i < collideWithOtherOrganInChildren.Length; i++) {
			if (collideWithOtherOrganInChildren[i]) {
				return true;
			}
        }
        return false;
	}
}
