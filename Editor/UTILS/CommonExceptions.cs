using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvalidDataGameException: Exception
{
    public InvalidDataGameException() : base() { }
    public InvalidDataGameException(string message) : base(message) { }
}
