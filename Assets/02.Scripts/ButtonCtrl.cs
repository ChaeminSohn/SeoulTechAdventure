using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public GameObject door;
    private Transform field;
    // Start is called before the first frame update
    void Start()
    {
        field = door.transform.Find("ForceField");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter(Collision coll)
	{
        if (coll.collider.tag == "PLAYER")
        {
            Destroy(field.gameObject);
        }

	}
}
