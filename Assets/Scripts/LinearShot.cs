using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearShot : MonoBehaviour
{
    [SerializeField] float _skillStartDistance = 0;
    [SerializeField] float _skillEndDistance = 10;
    [SerializeField] GameObject _projectile;

    Vector3 _skillStart = Vector3.zero;
    Vector3 _skillEnd = Vector3.zero;

    private void Update()
    {
        _skillStart = transform.forward * _skillStartDistance;
    }

    public void Fire()
    {

    }
}
