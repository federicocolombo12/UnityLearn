using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace c04.exercise
{
    public enum BuildingType { House, University, Office, Library, Park }
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private MeshRenderer _groundRenderer;

        public BuildingType Type => _buildingType;

        private void Awake()
        {
            if (_groundRenderer == null)
            {
                _groundRenderer = GetComponent<MeshRenderer>();
                if (_groundRenderer == null)
                {
                    Debug.LogError("MeshRenderer non trovato sul GameObject o non assegnato manualmente.");
                }
            }
        }

        public Vector3 GetRandomPosition()
        {
            if (_groundRenderer == null)
            {
                Debug.LogError("MeshRenderer non assegnato, impossibile calcolare una posizione casuale.");
                return Vector3.zero;
            }

            return new Vector3(
                Random.Range(_groundRenderer.bounds.min.x, _groundRenderer.bounds.max.x),
                0,
                Random.Range(_groundRenderer.bounds.min.z, _groundRenderer.bounds.max.z)
            );
        }
    }
}
