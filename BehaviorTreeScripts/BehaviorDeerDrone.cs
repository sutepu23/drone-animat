using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorDeerDrone : MonoBehaviour
{
    private BehaviorTree _behaviorTree;
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private GameObject _dockStation;
    [SerializeField] private GameObject _targetObject;

    private float stopDuration = 2.0f;
    private float currentStopTime = 0.0f;

    public enum ActionState
    {
        Idle,
        Found
    }
    private ActionState _state = ActionState.Idle;

    private Node.Status treeStatus = Node.Status.Running;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _behaviorTree = new BehaviorTree();
        Sequence drone = new Sequence("Find Target");
        Leaf searching = new Leaf("Drone Searching Target", SearchingTarget);;
        Leaf returnToDockStation = new Leaf("Drone Return To Dock Station", ReturnToDockStation);

        drone.AddChildren(searching);
        drone.AddChildren(returnToDockStation);
        _behaviorTree.AddChildren(drone);


        _behaviorTree.PrintTree();
    }

    private void Update()
    {
       if (treeStatus == Node.Status.Running)
       {
            treeStatus = _behaviorTree.Process();
       }
    }

    public Node.Status SearchingTarget()
    {
        return SearchingGoal();
    }

    public Node.Status ReturnToDockStation() 
    {
        return GoToDockStation(_dockStation.transform.position);
    }

    Node.Status GoToDockStation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
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
            return Node.Status.Success;
        }
        return Node.Status.Running;
    }

    public Node.Status SearchingGoal()
    {
        // Генерация случайной точки в 3D пространстве
        Vector3 randomOffset = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0, 10.0f), Random.Range(-10.0f, 10.0f));
        Vector3 randomPoint = transform.position + randomOffset;

        float distanceToTarget = Vector3.Distance(randomPoint, transform.position);

        if (_state == ActionState.Idle)
        {
            _agent.SetDestination(randomPoint);
            _state = ActionState.Found;
        }
        else if (distanceToTarget < 0.2f)
        {
            // Достигнута случайная точка, начать отсчет времени остановки
            currentStopTime = 0.0f;
            _state = ActionState.Idle;
        }
        else if (_agent.remainingDistance < 0.2f)
        {
            // Достигнута текущая точка, начать отсчет времени остановки
            currentStopTime = 0.0f;
            _state = ActionState.Idle;
        }

        // Если находимся в состоянии остановки, проверяем время остановки
        if (_state == ActionState.Idle)
        {
            currentStopTime += Time.deltaTime;

            // Если время остановки истекло, снова генерируем случайную точку
            if (currentStopTime >= stopDuration)
            {
                return Node.Status.Success;
            }
        }

        return Node.Status.Running;
    }
}
