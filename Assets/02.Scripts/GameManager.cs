using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> robots = new List<GameObject>();
    public static GameManager instance = null;
    private RaycastHit hit;
    Camera cam;

    public GameObject robot;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        robot = GameObject.Find("MiniRobot");
        cam = Camera.main;
    }

    public void GiveCommand(int cmd)
    {
        robot.GetComponent<MiniRobot>()?.Command(cmd, hit.point);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
    }
}
