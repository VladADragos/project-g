using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAUL
{

    [CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
    public class UnitObject : ScriptableObject
    {

        public new string name;
        public Vector2Int index;
        public Sprite sprite;
        public int speed;
        public int health;
        public int attack;

        public void SetIndex(int x, int y)
        {
            index.x = x;
            index.y = y;
        }
        public void SetIndex(Vector2Int index)
        {
            this.index = index;
        }


    }
}