using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using GAUL.EventArgs;
namespace GAUL
{


    public class DynGrid<T>
    {

        public int width { get; private set; }
        public int height { get; private set; }

        public float cellSize { get; private set; }
        public Vector3 origin { get; private set; }
        public T[,] array { get; private set; }

        public event EventHandler<IndexEventArgs> OnGridCellChanged;


        // TextMesh[,] debugTextArray;
        public DynGrid(int width, int height, float cellSize, Vector3 origin, Func<DynGrid<T>, int, int, T> itemInitializer, bool debugMode = false)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.origin = origin;
            array = new T[height, width];

            Initialize(itemInitializer);
            if (debugMode)
            {
                DrawDebugLines();
            }


        }

        void DrawDebugLines()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //array[x,y] = itemInitializer(this,x,y);
                    //debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, IndicesToWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    //debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, IndicesToWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(IndicesToWorldPosition(new Vector2Int(x, y)), IndicesToWorldPosition(new Vector2Int(x, y + 1)), Color.white, 100);
                    Debug.DrawLine(IndicesToWorldPosition(new Vector2Int(x, y)), IndicesToWorldPosition(new Vector2Int(x + 1, y)), Color.white, 100);
                }
                Debug.DrawLine(IndicesToWorldPosition(new Vector2Int(0, height)), IndicesToWorldPosition(new Vector2Int(width, height)), Color.white, 100);
                Debug.DrawLine(IndicesToWorldPosition(new Vector2Int(width, 0)), IndicesToWorldPosition(new Vector2Int(width, height)), Color.white, 100);

            }
        }

        void Initialize(Func<DynGrid<T>, int, int, T> itemInitializer)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    array[y, x] = itemInitializer(this, x, y);

                }

            }
        }

        public void SetCell(Vector2Int index, T item)
        {
            if (Utils.IsInRectBoundsInt(index, height, width))
            {
                array[index.y, index.x] = item;

                OnGridCellChanged?.Invoke(this, new IndexEventArgs { index = index });
            }

        }

        public void SetCell(Vector3 worldPosition, T item)
        {
            SetCell(WorldPositionToIndices(worldPosition), item);

        }

        public void TriggerOnGridCellChanged(Vector2Int index)
        {
            OnGridCellChanged?.Invoke(this, new IndexEventArgs { index = index });
        }
        public T GetCell(Vector2Int index)
        {

            if (Utils.IsInRectBoundsInt(index, height, width))
            {
                return array[index.y, index.x];
            }
            return default(T);

        }

        public T GetCell(Vector3 worldPosition)
        {
            WorldPositionToIndices(worldPosition);
            return GetCell(WorldPositionToIndices(worldPosition));
        }

        public Vector3 IndicesToWorldPosition(Vector2Int index)
        {
            return new Vector3(index.x, index.y) * cellSize + origin;
        }
        public Vector3 IndicesToWorldPositionCentered(Vector2Int index)
        {
            return new Vector3(index.x, index.y) * cellSize + origin + new Vector3(cellSize * 0.5f, cellSize * 0.5f); ;
        }
        public Vector2Int WorldPositionToIndices(Vector3 worldPosition)
        {
            return Utils.TruncatePoint(worldPosition - origin, cellSize);
        }
        public Vector2Int MousePositionToIndex()
        {
            return WorldPositionToIndices(Utils.GetMouseWorldPosition());
        }



    }
}