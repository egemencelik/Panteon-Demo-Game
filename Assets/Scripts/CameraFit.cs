﻿using System;
using System.Collections;
using UnityEngine;

// http://gamedev.stackexchange.com/a/89973/50623 script reference
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraFit : MonoBehaviour
{
    #region FIELDS

    public float UnitsForWidth = 1;
    public static CameraFit Instance;

    private float _width;

    private float _height;

    //*** bottom screen
    private Vector3 _bl;
    private Vector3 _bc;

    private Vector3 _br;

    //*** middle screen
    private Vector3 _ml;
    private Vector3 _mc;

    private Vector3 _mr;

    //*** top screen
    private Vector3 _tl;
    private Vector3 _tc;
    private Vector3 _tr;

    #endregion

    #region PROPERTIES

    public float Width
    {
        get { return _width; }
    }

    public float Height
    {
        get { return _height; }
    }

    // helper points:
    public Vector3 BottomLeft
    {
        get { return _bl; }
    }

    public Vector3 BottomCenter
    {
        get { return _bc; }
    }

    public Vector3 BottomRight
    {
        get { return _br; }
    }

    public Vector3 MiddleLeft
    {
        get { return _ml; }
    }

    public Vector3 MiddleCenter
    {
        get { return _mc; }
    }

    public Vector3 MiddleRight
    {
        get { return _mr; }
    }

    public Vector3 TopLeft
    {
        get { return _tl; }
    }

    public Vector3 TopCenter
    {
        get { return _tc; }
    }

    public Vector3 TopRight
    {
        get { return _tr; }
    }

    #endregion

    #region METHODS

    private void Awake()
    {
        try
        {
            if ((bool) GetComponent<Camera>())
            {
                if (GetComponent<Camera>().orthographic)
                {
                    ComputeResolution();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    private void ComputeResolution()
    {
        float deviceWidth;
        float deviceHeight;
        float leftX, rightX, topY, bottomY;
#if UNITY_EDITOR
        deviceWidth = GetGameView().x;
        deviceHeight = GetGameView().y;
#else
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
#endif

        /* Set the ortograpish size (shich is half of the vertical size) when we change the ortosize of the camera the item will be scaled 
         * autoamtically to fit the size frame of the camera
         */
        GetComponent<Camera>().orthographicSize = 1f / GetComponent<Camera>().aspect * UnitsForWidth / 2f;

        //Get the new height and Widht based on the new orthographicSize
        _height = 2f * GetComponent<Camera>().orthographicSize;
        _width = _height * GetComponent<Camera>().aspect;

        float cameraX, cameraY;
        cameraX = GetComponent<Camera>().transform.position.x;
        cameraY = GetComponent<Camera>().transform.position.y;

        leftX = cameraX - _width / 2;
        rightX = cameraX + _width / 2;
        topY = cameraY + _height / 2;
        bottomY = cameraY - _height / 2;

        //*** bottom
        _bl = new Vector3(leftX, bottomY, 0);
        _bc = new Vector3(cameraX, bottomY, 0);
        _br = new Vector3(rightX, bottomY, 0);
        //*** middle
        _ml = new Vector3(leftX, cameraY, 0);
        _mc = new Vector3(cameraX, cameraY, 0);
        _mr = new Vector3(rightX, cameraY, 0);
        //*** top
        _tl = new Vector3(leftX, topY, 0);
        _tc = new Vector3(cameraX, topY, 0);
        _tr = new Vector3(rightX, topY, 0);
        Instance = this;
    }

    private void Update()
    {
#if UNITY_EDITOR
        ComputeResolution();
#endif
    }


    private Vector2 GetGameView()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo getSizeOfMainGameView =
            T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object resolution = getSizeOfMainGameView.Invoke(null, null);
        return (Vector2) resolution;
    }

    #endregion
}