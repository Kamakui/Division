using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] private GameObject armObj;

    private float eulerAngleX;
    private float eulerAngleY;
    public float reception;
    private PlayerControler playerControler;
    [SerializeField] private Transform pointInven;
    [SerializeField] private Transform pointPlay;
    [SerializeField] private Transform pointAim;
    [SerializeField] private Transform cameraObj;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Transform pointObj;
    [SerializeField] private Transform spineObj;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        eulerAngleX = transform.rotation.eulerAngles.y;
        eulerAngleY = transform.rotation.eulerAngles.x;
    }

    private void Update()
    {
        if (GameManager.Instance.isDead)
            return;

        if (playerControler.IsOpenInventory)
            LookAtInventory();
        else
            FollowPlayer();

        Debug.DrawLine(cameraObj.position, cameraObj.forward * 100, Color.green);
    }

    private void FollowPlayer()
    {
        eulerAngleX -= Input.GetAxis("Mouse Y") * reception;
        eulerAngleY += Input.GetAxis("Mouse X") * reception;

        if (eulerAngleX > 75)
            eulerAngleX = 75;
        else if (eulerAngleX < -75)
            eulerAngleX = -75;

        this.transform.rotation = Quaternion.Euler(0, eulerAngleY, 0);
        armObj.transform.localRotation = Quaternion.Euler(eulerAngleX, 0, 0);
        playerModel.localRotation = Quaternion.Euler(0, 0, 0);

        CameraAimPoint();

        spineObj.localRotation = armObj.transform.localRotation;
    }

    private void LookAtInventory()
    {
        cameraObj.position = pointInven.position;
        cameraObj.rotation = pointInven.rotation;
        playerControler.ResetAnimatorState();
        playerModel.LookAt(new Vector3(cameraObj.transform.position.x, playerModel.transform.position.y, cameraObj.transform.position.z));
    }

    private void CameraAimPoint()
    {
        cameraObj.position = Input.GetMouseButton(1) ? pointAim.position : pointPlay.position;
        cameraObj.rotation = Input.GetMouseButton(1) ? pointAim.rotation : pointPlay.rotation;

        //·¹ÀÌ ½î´Â Áß
        RaycastHit hit;
        if (Physics.Raycast(cameraObj.position, cameraObj.forward, out hit, 100, layerMask))
            pointObj.position = hit.point;
        else
            pointObj.position = cameraObj.forward * 100;
    }
}
