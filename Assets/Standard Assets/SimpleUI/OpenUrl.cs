﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUrl : MonoBehaviour
{
    public string Url;

    Button Button;

    void OnEnable()
    {
        Button = GetComponentInChildren<Button>() ?? GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    void OnDisable()
    {
        if (Button)
            Button.onClick.RemoveListener(Execute);
        else
            Debug.LogWarning("This component is not assigned to Button. It is assigned to " + gameObject.name);
    }

    public void Execute()
    {
        var newUrl = "";
        if (Url.StartsWith("/"))
            newUrl = Url;
        else
            newUrl = "/" + Url;
        
        SimpleUI.OpenUrl(newUrl);
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position - new Vector3(0, -250, 0);

        Gizmos.DrawCube(pos - Vector3.one * 20, Vector3.one * 20);

        //Debug.Log($"Position of /{Url}={pos}");
        //Handles.Label(pos, SimpleUI.GetPrettyNameForExistingUrl("/" + Url)); // transform.position - new Vector3(0, -250, 0)
    }
}