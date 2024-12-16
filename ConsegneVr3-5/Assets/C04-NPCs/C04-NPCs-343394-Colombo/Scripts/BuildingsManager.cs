using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace c04.exercise
{
    public class BuildingsManager : MonoBehaviour
    {
        private Building[] _buildings;
        
        //This is a very basic implementation of the Singleton Pattern.
        
        public static BuildingsManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            _buildings = FindObjectsOfType<Building>();
        }

        public Building GetBuilding(BuildingType type)
        {
            return _buildings.FirstOrDefault(b => b.Type == type);
        }
    }

}