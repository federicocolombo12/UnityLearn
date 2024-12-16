using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace c04.exercise
{
    public class Person : MonoBehaviour
    {
        enum PersonState
        {
            GoToDestination,
            Rest,
            Work,
            Leisure
        }

        enum PersonType
        {
            Student,
            Adult,
            Elder
        }
        
        [SerializeField] private PersonType _personType;
        [SerializeField] private float _secondsInPlace;
        
        private Building _house;
        private NavMeshAgent _agent;
        
        private PersonState[] _activityOrder = {PersonState.Rest, PersonState.Work, PersonState.Leisure};
        private int _currentActivityIndex = 0;
        private PersonState _currentPersonState;
        private Building _currentBuilding = null;
        private Building _destinationBuilding;
        private Vector3 _destinationPosition;
        private float _timeAtPlaceTimer;

        private void Awake()
        {
            _currentPersonState = PersonState.GoToDestination;
            _destinationBuilding = BuildingsManager.Instance.GetBuilding(BuildingType.Park);
            _destinationPosition = _destinationBuilding.GetRandomPosition();
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(_destinationPosition);
        }

        private void Start()
        {
            _currentPersonState = GetStateForBuilding(_currentBuilding);
        }

        private void Update()
        {
            UpdateState();
            CheckTransition();
        }

        private void UpdateState()
        {
            switch (_currentPersonState)
            {
                case PersonState.GoToDestination:
                    ReachDestination();
                    break;
                case PersonState.Rest:
                    Rest();
                    break;
                case PersonState.Work:
                    Work();
                    break;
                case PersonState.Leisure:
                    Leisure();
                    break;
            }
            {
                
            }
        }

        private void ReachDestination()
        {
            _agent.SetDestination(_destinationBuilding.transform.position);
        }

        private void Work()
        {
            UpdatePlaceTimer();
            setDestinationBuilding(BuildingsManager.Instance.GetBuilding(BuildingType.Park));
        }

        private void Leisure()
        {
            UpdatePlaceTimer();
            setDestinationBuilding(BuildingsManager.Instance.GetBuilding(BuildingType.House));
        }

        private void Rest()
        {
            UpdatePlaceTimer();
            
            if (_personType == PersonType.Student)
            {
                setDestinationBuilding(BuildingsManager.Instance.GetBuilding(BuildingType.University));
            }

            if (_personType == PersonType.Adult)
            {
                setDestinationBuilding(BuildingsManager.Instance.GetBuilding(BuildingType.Office));
            }

            if (_personType == PersonType.Elder)
            {
                setDestinationBuilding(BuildingsManager.Instance.GetBuilding(BuildingType.Library));
            }
        }

        private void UpdatePlaceTimer()
        {
            StartCoroutine(WaitCoroutine());
        }
        
        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_secondsInPlace);
            _timeAtPlaceTimer = 0;
        }

        private void CheckTransition()
        {
            if (DestinationReached())
            {
                _currentActivityIndex = (_currentActivityIndex + 1) % _activityOrder.Length;
                _currentPersonState = _activityOrder[_currentActivityIndex];
                
            }
        }

        private bool DestinationReached()
        {
            //This if is commented to avoid compilation errors due to missing packages in your project.
            //Once you have the AI Navigation package installed uncomment this method AND the varialbe _agent at the top of the script
            //Use the if to check if the agent has reached its intended destination. 
            // if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f))
            // {
            //     return true;
            // }

            return (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance &&
                    (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f));

        }
        
        private PersonState GetStateForBuilding(Building building)
        {
            return building.Type switch
            {
                BuildingType.House => PersonState.Rest,
                BuildingType.University or BuildingType.Office or BuildingType.Library => PersonState.Work,
                BuildingType.Park => PersonState.Leisure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        //This is a public method which can be used to assign a reference to the house
        //which instantiated this person  
        public void AssignHouse(Building house)
        {
            _house = house;
        }
        private void setDestinationBuilding(Building building)
        {
            _destinationBuilding = building;
        }
        
        
    }
}