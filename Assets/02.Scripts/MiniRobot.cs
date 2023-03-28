using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MiniRobot : MonoBehaviour
{
    public enum State
    {
        WAIT, TRACE, MOVE, OPEN, CLOSED
    }
    public State state;
    private Transform robotTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isAlive = true;

    private readonly int hashWalk = Animator.StringToHash("Roll_Anim");

    void Start()
    {
        robotTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = true;
        state = State.WAIT;


        StartCoroutine(RobotAction());
    }

    public void Command(System.String cmd, Vector3 pos)
    {
        switch (cmd)
        {
            case "Follow":
                state = State.TRACE;
                break;
            case "Move":
                state = State.MOVE;
                agent.destination = pos;
                break;
            case "Explode":
                break;
        }
    }

    IEnumerator RobotAction()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(0.3f);

            switch (state)
            {
                case State.WAIT:
                    agent.isStopped = true;
                    anim.SetBool(hashWalk, false);
                    agent.speed = 0.0f;
                    break;
                case State.TRACE:
                    float distance = Vector3.Distance(robotTr.position, playerTr.position);
                    if (distance > 10.0f)
                    {
                        agent.SetDestination(playerTr.position);
                        anim.SetBool(hashWalk, true);
                        agent.speed = (float)playerTr.GetComponent<Player>()?.moveSpeed * 2;
                        agent.isStopped = false;
                    }
                    else
                    {
                        anim.SetBool(hashWalk, false);
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                    }
                    break;
                case State.MOVE:
                    anim.SetBool(hashWalk, true);
                    agent.speed = (float)playerTr.GetComponent<Player>()?.moveSpeed * 2;
                    agent.isStopped = false;
                    break;

            }
        }
    }

}
