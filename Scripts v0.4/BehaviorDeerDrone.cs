using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorDeerDrone : MonoBehaviour
{
    private BehaviorTree _behaviorTree;
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private GameObject dockStation;
    [SerializeField] private GameObject targetObject;

    public enum ActionState
    {
        Idle,
        Found
    }
    private ActionState _state = ActionState.Idle;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _behaviorTree = new BehaviorTree();
        Node drone = new Node("Find Target");
        Leaf searching = new Leaf("Drone Searching Target", SearchingTarget);;
        Leaf returnToDockStation = new Leaf("Drone Return To Dock Station", ReturnToDockStation);

        drone.AddChildren(searching);
        drone.AddChildren(returnToDockStation);
        _behaviorTree.AddChildren(drone);

        _behaviorTree.Process();

        _behaviorTree.PrintTree();
    }

    public Node.Status SearchingTarget()
    {
        _agent.SetDestination(targetObject.transform.position);
        return Node.Status.Success;
    }

    public Node.Status ReturnToDockStation() 
    {
        _agent.SetDestination(dockStation.transform.position);
        return Node.Status.Success;
    }

    Node.Status GoToAim(Vector3 destination)
    {
        float destinationToTarget = Vector3.Distance(destination, this.transform.position);
        if(_state == ActionState.Idle)
        {
            _agent.SetDestination(destination);
            _state = ActionState.Found;
        }
        else if(Vector3.Distance(_agent.pathEndPosition, destination) >= 2)
        {
            _state = ActionState.Idle;
            return Node.Status.Failed;
        }
        else if (distanceToTarget < 2)
        {
            _state = ActionState.Idle;
            return
        }
    }
}
