using UnityEngine;
namespace GAUL.EventArgs
{
    public class MoveEventArgs : System.EventArgs
    {
        public Vector2Int oldIndex;
        public Vector2Int newIndex;
    }
}