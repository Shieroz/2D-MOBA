using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Terrain map;
    public int width, height;
    public float moveRate, cutOffX, cutOffZ;
    float minX, minZ, maxX, maxZ;

    // Utility wrappers
    int mX { get { return (int)Input.mousePosition.x; } }
    int mZ { get { return (int)Input.mousePosition.y; } }

    void Start() {
        width = Screen.width - 1;
        height = Screen.height;
        Vector3 mapSize = map.terrainData.size;
        float size = GetComponent<Camera>().orthographicSize;
        minX = size + cutOffX;
        minZ = size + cutOffZ;
        maxX = mapSize.x - size - cutOffX;
        maxZ = mapSize.z - size - cutOffZ;
    }

    void PanCamera() {
        float x = transform.position.x;
        float z = transform.position.z;
        if (mX <= 0 && x > minX)
            transform.position += -transform.right * moveRate * Time.deltaTime;
        if (mX >= width && x < maxX)
            transform.position += transform.right * moveRate * Time.deltaTime;
        if (mZ <= 0 && z > minZ)
            transform.position += -transform.up * moveRate * Time.deltaTime;
        if (mZ >= height && z < maxZ)
            transform.position += transform.up * moveRate * Time.deltaTime;
    }

    public void MoveCamera(Vector3 newPosition)
    {
        MoveCamera(new Vector2(newPosition.x, newPosition.z));
    }

    public void MoveCamera(Vector2 newPosition) {
        float x, z;
        if (newPosition.x > minX) {
            if (newPosition.x < maxX)
                x = newPosition.x;
            else
                x = maxX;
        }
        else
            x = minX;
        if (newPosition.y > minZ)
        {
            if (newPosition.y < maxZ)
                z = newPosition.y;
            else
                z = maxZ;
        }
        else
            z = minZ;
        transform.position = new Vector3(x, 20f, z);
    }
    // Update is called once per frame
    void Update()
    {
        PanCamera();
    }
}
