using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c04.exercise
{
    public class House : Building
    {
        [SerializeField] private GameObject _studentPrefab;
        [SerializeField] private GameObject _adultPrefab;
        [SerializeField] private GameObject _elderPrefab;
        
        [SerializeField] private int _studentNumber;
        [SerializeField] private int _adultNumber;
        [SerializeField] private int _elderNumber;

        private void Start()
        {
            //Is this a good place where to instantiate the house members??
            InstantiateHouseMember();
            
        }

        private void InstantiateHouseMember(/* pass in some variables to make the method general purpose */)
        {
            //This should be a general purpose method which helps in avoiding writing the same code multiple times
            for (int i = 0; i < _studentNumber; i++)
            {
                Instantiate(_studentPrefab, GetRandomPosition(), Quaternion.identity);
            }
            for (int i=0; i<_adultNumber; i++)
            {
                Instantiate(_adultPrefab, GetRandomPosition(), Quaternion.identity);
            }
            for (int i=0; i<_elderNumber; i++)
            {
                Instantiate(_elderPrefab, GetRandomPosition(), Quaternion.identity);
            }
        }
    }
}
