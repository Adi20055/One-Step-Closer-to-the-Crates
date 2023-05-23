using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRotationController : MonoBehaviour
{
    private Transform rotation;
    public float fixedRotationX = 0;
    public float fixedRotationY = 0;
    public float fixedRotationZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = new Quaternion(0f, transform.rotation.y, 0f).eulerAngles;
    }
}
