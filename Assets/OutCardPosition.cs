using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCardPosition : MonoBehaviour {

    public int Index;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CardPosition(List<PokeValue>ValueList)
    {
        for (int i = 0; i < ValueList.Count; i++)
        {
            ValueList[i].transform.position = new Vector3(transform.position.x-(ValueList.Count/2-i)*70,transform.position.y,0);
        }
    }
}
