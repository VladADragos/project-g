using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GAUL.EventArgs;
namespace GAUL
{


    public class UITest : MonoBehaviour
    {
        // Start is called before the first frame update
        Testing t;


        [SerializeField] new Text name;
        [SerializeField] Text health;
        // [SerializeField] Text health;
        [SerializeField] Text attack;
        // [SerializeField] Text attack;
        [SerializeField] Text speed;
        // [SerializeField] Text speed;

        void Start()
        {
            t = FindObjectOfType<Testing>();

            // panel = GetComponent("Panel");
            // panel.
            t.OnUnitSelected += UnitSelected;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UnitSelected(object sender, IndexEventArgs eventArgs)
        {
            name.text = eventArgs.unit.name.ToString();
            health.text = eventArgs.unit.health.ToString();
            attack.text = eventArgs.unit.attack.ToString();
            speed.text = eventArgs.unit.speed.ToString();
        }
    }
}