using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace c04.exercise
{
    public enum BuildingType {House, University, Office, Library, Park}
    public class Building : MonoBehaviour
    
    {
        
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private MeshRenderer _groundRenderer;
        
        public BuildingType Type => _buildingType;
        

        public Vector3 GetRandomPosition()
        {
            //this return is simply to avoid compilation errors.
            //Remember to remove it if you implement and use this method. 
            
            return new Vector3(Random.Range(_groundRenderer.bounds.min.x, _groundRenderer.bounds.max.x),
                0,
                Random.Range(_groundRenderer.bounds.min.z, _groundRenderer.bounds.max.z));
        }
    }
}
