using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxController : MonoBehaviour
{
    #region Private Variables
    
    private Transform _cam;
    private float _distance;
    private Vector3 _camStartPos;
    private GameObject[] _backGrounds;
    private Material[] _materials;
    private float[] _backSpeed;
    private float _farthestBack;
   [Range(0.01f, 0.05f)] public float parallaxSpeed;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    #endregion
   
    void Start()
    {
        if (Camera.main != null) _cam = Camera.main.transform;
        _camStartPos = _cam.position;
        int backCount = transform.childCount;
        _materials = new Material[backCount];
        _backSpeed = new float[backCount];
        _backGrounds = new GameObject[backCount];
        for (int i = 0; i < backCount; i++)
        {
            _backGrounds[i] = transform.GetChild(i).gameObject;
            _materials[i] = _backGrounds[i].GetComponent<MeshRenderer>().material;
        }
        BackSpeedCalculate(backCount);
    }
    
    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((_backGrounds[i].transform.position.z - _cam.transform.position.z) > _farthestBack)
            {
                _farthestBack = _backGrounds[i].transform.position.z - _cam.transform.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            _backSpeed[i] = (_backGrounds[i].transform.position.z - _cam.position.z) / _farthestBack;
        }
    }

    private void LateUpdate()
    {
        var position = _cam.position;
        _distance = position.x - _camStartPos.x;
        var transform1 = transform;
        transform1.position = new Vector3(position.x, transform1.position.y, 0);
        for (int i = 0; i < _backGrounds.Length; i++)
        {
            float speed = _backSpeed[i] * parallaxSpeed;
            _materials[i].SetTextureOffset(MainTex,new Vector2(_distance,0) * speed);
        }
    }
}
