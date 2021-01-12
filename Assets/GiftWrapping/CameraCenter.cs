using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    GiftWrapping giftWrapping;

    Vector3 boxCenter;
    Vector3 boxSize;
    Vector3 newCameraPos;

    [Range(1f, 50f)] [SerializeField]
    float cameraYOffset;

    [Range(20f, 80f)]
    [SerializeField]
    float cameraTilt = 20f;

    // Start is called before the first frame update
    private void Awake()
    {
        giftWrapping = FindObjectOfType<GiftWrapping>();
        boxCenter = giftWrapping.center;
        boxSize = new Vector3(giftWrapping.size.x, 0f, giftWrapping.size.y);
        
        SetupCameraPlacement();
    }

    private void SetupCameraPlacement()
    {
        newCameraPos = new Vector3(boxCenter.x, boxSize.z / 2f, boxCenter.z - boxSize.z * 1.5f);
        Vector3 cameraRot = new Vector3(cameraTilt, 0f , 0f);
        Quaternion cameraQuatRot = Quaternion.Euler(cameraRot);
        this.gameObject.transform.SetPositionAndRotation(newCameraPos, cameraQuatRot);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetupCameraPlacement();
    }
}
