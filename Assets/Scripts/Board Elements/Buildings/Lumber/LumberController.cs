using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberController : BoardBuildingController
{
    protected override void ElementActivity()
    {
        base.ElementActivity();
        GameManager.instance.Woods += boardElement.Model.MaterialPerActivity * GameManager.instance.PowerLevel;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }
}