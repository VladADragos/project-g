using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            print("w");
            transform.position += new Vector3(0, 5 * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            print("a");
            transform.position -= new Vector3(5 * Time.deltaTime, 0);
        }
        if (Input.GetKey("s"))
        {
            print("s");
            transform.position -= new Vector3(0, 5 * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(5 * Time.deltaTime, 0);
            print("d");
        }

        if (Input.GetKey("r"))
        {

            transform.Rotate(new Vector3(0, 0, 3));
            print("d");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("space"))
        {
            if (Camera.main.orthographicSize > 2)
                Camera.main.orthographicSize -= 0.1f;

            print("zooming out");
        }
        else if (Input.GetKey("space"))
        {
            Camera.main.orthographicSize += 0.1f;

            print("zooming");
        }


    }
}
