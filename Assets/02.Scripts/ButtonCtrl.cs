using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public GameObject door;
    private Transform field;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = door.gameObject.activeSelf;
        field = door.transform.Find("ForceField");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(isActive)
            door.gameObject.SetActive(false);
        else 
            door.gameObject.SetActive(true);    
    }

    void OnTriggerExit(Collider other)
    {
        if(isActive)
            door.gameObject.SetActive(true);
        else
            door.gameObject.SetActive(false);
    }
}
