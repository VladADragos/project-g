using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAUL.EventArgs;
namespace GAUL
{
    public class Testing : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] int width, height = 0;
        [SerializeField] float cellSize = 0;
        [SerializeField] Vector3 origin = Vector3.zero;
        public DynGrid<TerrainTile> grid { get; private set; }
        public Dictionary<Vector2Int, UnitObject> units { get; private set; } = new Dictionary<Vector2Int, UnitObject>();


        Dictionary<string, TerrainTile> terrainObjects { get; set; } = new Dictionary<string, TerrainTile>();
        Dictionary<string, UnitObject> unitsObjects { get; set; } = new Dictionary<string, UnitObject>();

        public UnitObject selectedUnit;

        public event EventHandler<MoveEventArgs> OnUnitMoved;
        public event EventHandler<IndexEventArgs> OnUnitSelected;
        public event EventHandler<System.EventArgs> OnUnitDeSelected;

        private void Awake()
        {

            LoadResources();
            grid = new DynGrid<TerrainTile>(width, height, cellSize, transform.position, (DynGrid<TerrainTile> g, int x, int y) => { return terrainObjects["Grass"]; }, true);
            SpawnUnitAtPos(unitsObjects["Cavalary"], new Vector2Int(1, 0));
            SpawnUnitAtPos(unitsObjects["Cavalary"], new Vector2Int(2, 0));
            SpawnUnitAtPos(unitsObjects["Cavalary"], new Vector2Int(3, 0));
            SpawnUnitAtPos(unitsObjects["Infantry"], new Vector2Int(4, 0));


        }
        void SpawnUnitAtPos(UnitObject type, Vector2Int index)
        {
            UnitObject unit = Instantiate(type);
            unit.SetIndex(index);
            units.Add(index, unit);
        }
        void MoveUnit(Vector2Int index)
        {
            if (selectedUnit != null)
            {

                Vector2Int oldPos = selectedUnit.index;
                selectedUnit.index = index;
                OnUnitMoved?.Invoke(this, new MoveEventArgs { oldIndex = oldPos, newIndex = index });
                units.Add(index, units[oldPos]);
                units.Remove(oldPos);
                selectedUnit = null;
                OnUnitDeSelected?.Invoke(this, System.EventArgs.Empty);
            }

        }



        public void SelectUnit(Vector2Int index)
        {

            if (Utils.IsInRectBoundsInt(index, width, height) && units.ContainsKey(index))
            {
                UnitObject unit = units[index];
                SelectUnit(unit);
                OnUnitSelected?.Invoke(this, new IndexEventArgs { index = index, unit = unit });

            }
            // SelectUnit(unit);
        }

        void SelectUnit(UnitObject unit)
        {
            selectedUnit = unit;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {

                // var worldPosition = Utils.GetMouseWorldPosition();
                // var index = grid.WorldPositionToIndices(worldPosition);
                SelectUnit(grid.MousePositionToIndex());

            }
            if (Input.GetMouseButtonDown(1))
            {
                // var worldPosition = Utils.GetMouseWorldPosition();
                // var index = grid.WorldPositionToIndices(worldPosition);
                MoveUnit(grid.MousePositionToIndex());
            }
        }

        void LoadResources()
        {
            print(Resources.Load<TerrainTile>("Tiles/Grass"));
            terrainObjects.Add("Grass", Resources.Load<TerrainTile>("Tiles/Grass"));
            terrainObjects.Add("Dirt", Resources.Load<TerrainTile>("Tiles/Dirt"));
            terrainObjects.Add("Water", Resources.Load<TerrainTile>("Tiles/Water"));
            unitsObjects.Add("Cavalary", Resources.Load<UnitObject>("Units/Cavalary"));
            unitsObjects.Add("Infantry", Resources.Load<UnitObject>("Units/Infantry"));
        }

    }

}