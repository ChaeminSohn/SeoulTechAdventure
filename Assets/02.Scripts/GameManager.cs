using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> robots = new List<GameObject>();
    public List<int> ctrl = new List<int>();    
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
        //GameObject robotGroup = GameObject.Find("ROBOTS");
        
        foreach(GameObject robot in GameObject.FindGameObjectsWithTag("ROBOT"))
            robots.Add(robot);
   
        //robot = GameObject.Find("MiniRobot");
        ctrl.Add(0);
        
        cam = Camera.main;
    }

    public void GiveCommand(System.String cmd)
    { 
        foreach(int i in ctrl)
        {
            robots[i].GetComponent<MiniRobot>()?.Command(cmd, hit.point);
        }
    }
   

    public void SetCtrl(int num)
    {
        if (robots.Count >= num)
        {
            num--;
            if (ctrl.Contains(num))
                ctrl.Remove(num);
            else ctrl.Add(num);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
    }

    public void OnPlayerWin()
    {

    }
}
