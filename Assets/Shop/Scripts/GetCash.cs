using UnityEngine;
using System.Collections;

public class GetCash : MonoBehaviour {

	void Update () {
        GetComponent<TextMesh>().text = "$ " + InventorySingleton.Instance.cash;
	}
}
