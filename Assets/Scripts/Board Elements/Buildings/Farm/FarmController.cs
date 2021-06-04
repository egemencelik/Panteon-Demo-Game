using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmController : BoardBuildingController
{
    protected override void ElementActivity()
    {
        base.ElementActivity();
        GameManager.instance.Food += boardElement.Model.MaterialPerActivity * GameManager.instance.PowerLevel;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }
}