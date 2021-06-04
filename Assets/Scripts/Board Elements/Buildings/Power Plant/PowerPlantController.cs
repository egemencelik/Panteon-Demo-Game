using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantController : BoardBuildingController
{
    private PowerPlantModel model => (PowerPlantModel) boardElement.Model;
    private bool created;

    protected override void ElementActivity()
    {
        base.ElementActivity();
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        GameManager.instance.PowerLevel += model.PowerLevel;
        GameManager.instance.UpdateText();
    }

    public override void OnMove()
    {
        base.OnMove();
        GameManager.instance.PowerLevel -= model.PowerLevel;
        GameManager.instance.UpdateText();
    }
}