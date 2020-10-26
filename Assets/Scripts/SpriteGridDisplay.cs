using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using GAUL.EventArgs;
namespace GAUL
{
    public class SpriteGridDisplay : MonoBehaviour
    {

        [SerializeField] Sprite[] sprites;
        Sprite sprite1;
        [SerializeField] GameObject prefab;
        [SerializeField] GameObject prefabUnit;
        [SerializeField] GameObject selectedPrefab;
        GameObject selected;
        DynGrid<TerrainTile> grid;
        Dictionary<Vector2Int, UnitObject> units;
        GameObject[] gameObjects;
        GameObject[] gameObjectsUnits;
        Testing t;

        void Start()
        {
            t = FindObjectOfType<Testing>();
            grid = t.grid;
            units = t.units;

            gameObjects = new GameObject[grid.height * grid.width];
            gameObjectsUnits = new GameObject[grid.height * grid.width];
            var sprite = prefab.GetComponent<SpriteRenderer>();

            InitializeTiles();
            InitializeUnits();

            selected = Instantiate(selectedPrefab, Vector3.zero, Quaternion.identity);
            selected.SetActive(false);

            grid.OnGridCellChanged += UpdateVisual;
            t.OnUnitMoved += UpdateUnitPosition;
            t.OnUnitSelected += Selected;
            t.OnUnitDeSelected += UnSelected;
        }

        void InitializeUnits()
        {
            foreach (var unit in units)
            {
                Vector3 worldPosition = grid.IndicesToWorldPositionCentered(unit.Value.index);
                var u = Instantiate(prefabUnit, worldPosition, Quaternion.identity);
                u.GetComponent<SpriteRenderer>().sprite = unit.Value.sprite;
                gameObjectsUnits[Utils.ToLinearIndex(unit.Value.index, grid.width)] = u;
                // Utils.CreateWorldText("test", u.transform, grid.IndicesToWorldPositionCentered(new Vector2Int(4, 4)), 3, Color.black, TextAnchor.MiddleCenter);

            }

        }
        void InitializeTiles()
        {
            for (var y = 0; y < grid.height; y++)
            {
                for (var x = 0; x < grid.width; x++)
                {
                    Vector3 worldPosition = grid.IndicesToWorldPositionCentered(new Vector2Int(x, y)) + new Vector3(0, 0, 1);
                    var temp = Instantiate(prefab, worldPosition, Quaternion.identity);
                    temp.transform.parent = transform;
                    gameObjects[Utils.ToLinearIndex(x, y, grid.width)] = temp;

                }
            }

        }
        void UpdateVisual(object sender, IndexEventArgs eventArgs)
        {

            var newCell = grid.GetCell(eventArgs.index);
            GameObject gameObject = gameObjects[Utils.ToLinearIndex(eventArgs.index, grid.width)];

            gameObject.GetComponent<SpriteRenderer>().sprite = newCell.sprite;

        }

        void Selected(object sender, IndexEventArgs eventArgs)
        {
            selected.transform.position = grid.IndicesToWorldPositionCentered(eventArgs.index);
            selected.SetActive(true);

        }
        void UnSelected(object sender, System.EventArgs eventArgs)
        {
            selected.SetActive(false);

        }
        void UpdateUnitPosition(object sender, MoveEventArgs eventArgs)
        {

            GameObject gameObject = gameObjectsUnits[Utils.ToLinearIndex(eventArgs.oldIndex, grid.width)];
            Vector3 worldPosition = grid.IndicesToWorldPositionCentered(eventArgs.newIndex);
            gameObject.transform.position = worldPosition;
            gameObjectsUnits[Utils.ToLinearIndex(eventArgs.newIndex, grid.width)] = gameObjectsUnits[Utils.ToLinearIndex(eventArgs.oldIndex, grid.width)];
            gameObjectsUnits[Utils.ToLinearIndex(eventArgs.oldIndex, grid.width)] = null;
            // var newCell = grid.GetCell(eventArgs.index);

            // gameObject.GetComponent<SpriteRenderer>().sprite = newCell.sprite;

        }
    }
}