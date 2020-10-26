using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GAUL
{

    [CreateAssetMenu(fileName = "New TerrainTile", menuName = "TerrainTile")]
    public class TerrainTile : ScriptableObject
    {
        public string Name;
        public Vector2Int index;
        public Sprite sprite;
        public bool isBlocking = true;

    }
}