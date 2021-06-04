using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : BoardBuildingController
{
    protected override void ElementActivity()
    {
        base.ElementActivity();
        GameManager.instance.Stones += boardElement.Model.MaterialPerActivity * GameManager.instance.PowerLevel;
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }
}