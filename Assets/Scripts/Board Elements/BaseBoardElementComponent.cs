using System;
using UnityEngine;

public class BaseBoardElementComponent : MonoBehaviour
{
    public BoardElement boardElement;

    protected virtual void Awake()
    {
        boardElement = GetComponent<BoardElement>();
    }
}