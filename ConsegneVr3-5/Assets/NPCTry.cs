using c04.exercise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTry : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(BuildingsManager.Instance.GetBuilding(BuildingType.House).transform.position);
    }

    // Update is called once per frame
    
}
