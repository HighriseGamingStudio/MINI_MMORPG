using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float posY = 15;
    [SerializeField] float rotX = 60;
    [SerializeField] float fov = 60;

    private float posY_old;
    private float rotX_old;
    private float fov_old;

    void LateUpdate()
    {
        CheckChanges();
    }

    void CheckChanges()
    {
        if (posY != posY_old)
            SetPosition();

        if (rotX != rotX_old)
            SetRotation();

        if (fov != fov_old)
            SetFOV();

        posY_old = posY;
        rotX_old = rotX;
        fov_old = fov;
    }

    void SetPosition()
    {
        Vector3 currentPosition = transform.localPosition;
        currentPosition.y = posY;
        transform.localPosition = currentPosition;
    }

    void SetRotation()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.x = rotX;
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    void SetFOV()
    {
        GetComponent<Camera>().fieldOfView = fov;
    }
}