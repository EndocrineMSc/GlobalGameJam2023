using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBark : HealthEntity
{
    public static TreeBark leftTreebark;
    public static TreeBark rightTreebark;

    public AttackDirektion myDirection;

    private void Awake()
    {
        if (myDirection == AttackDirektion.Left)
            leftTreebark = this;
        else if (myDirection == AttackDirektion.Right)
            rightTreebark = this;
    }

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        if (this.Equals(leftTreebark))
            leftTreebark = null;
        if (this.Equals(rightTreebark))
            rightTreebark = null;
    }
}
