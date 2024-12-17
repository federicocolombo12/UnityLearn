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

        [SerializeField] private Building _house;
        private NavMeshAgent _agent;

        private PersonState[] _activityOrder = { PersonState.Rest, PersonState.Work, PersonState.Leisure };
        private int _currentActivityIndex = 0;
        [SerializeField] private PersonState _currentPersonState;
        
        private Building _currentBuilding;
        private Building _destinationBuilding;
        private Vector3 _destinationPosition;
        private float _timeAtPlaceTimer;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            if (_agent == null)
            {
                Debug.LogError("NavMeshAgent non trovato!");
                return;
            }

            
            if (_house == null)
            {
                Debug.LogError("Building di tipo House non trovato!");
                return;
            }

            _currentBuilding = _house;
            _currentPersonState = PersonState.Rest;
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
        }

        private void ReachDestination()
        {
            if (DestinationReached())
            {
                _currentPersonState = _activityOrder[_currentActivityIndex];
                _timeAtPlaceTimer = 0;
            }
        }

        private void Work()
        {
            UpdatePlaceTimer();
        }

        private void Leisure()
        {
            UpdatePlaceTimer();
        }

        private void Rest()
        {
            UpdatePlaceTimer();
        }

        private void UpdatePlaceTimer()
        {
            _timeAtPlaceTimer += Time.deltaTime;
            if (_timeAtPlaceTimer >= _secondsInPlace)
            {
                _timeAtPlaceTimer = 0;
                _currentActivityIndex = (_currentActivityIndex + 1) % _activityOrder.Length;
                _currentPersonState = PersonState.GoToDestination;
                _destinationBuilding = GetNextBuilding();
                _destinationPosition = _destinationBuilding.GetRandomPosition();
                _agent.SetDestination(_destinationPosition);
            }
        }

        private void CheckTransition()
        {
            if (_currentPersonState == PersonState.GoToDestination && DestinationReached())
            {
                _currentPersonState = _activityOrder[_currentActivityIndex];
                _timeAtPlaceTimer = 0;
            }
        }

        private bool DestinationReached()
        {
            return (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance &&
                    (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f));
        }

        private Building GetNextBuilding()
        {
            

            switch (_activityOrder[_currentActivityIndex])
            {
                case PersonState.Rest:

                    return _house;
                case PersonState.Work:
                    return _personType switch
                    {
                        PersonType.Student => BuildingsManager.Instance.GetBuilding(BuildingType.University),
                        PersonType.Adult => BuildingsManager.Instance.GetBuilding(BuildingType.Office),
                        PersonType.Elder => BuildingsManager.Instance.GetBuilding(BuildingType.Library),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                case PersonState.Leisure:
                    return BuildingsManager.Instance.GetBuilding(BuildingType.Park);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AssignHouse(Building house)
        {
            _house = house;
        }
    }
}
