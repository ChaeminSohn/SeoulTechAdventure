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

    void OnTriggerStay(Collider other)
    {
        door.gameObject.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        door.gameObject.SetActive(true);
    }
}
