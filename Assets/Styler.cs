﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum TextStyle
{
    None,
    ScreenTitle,
    PanelTitle,
    PanelText,

    KpiLabel,
    KpiValue,
    
    PlainDescription,
    
    SmallText,
    Link
}

public enum ImageColor
{
    None,
    Primary,
    Secondary,
}

public class Styler : MonoBehaviour
{
    [FormerlySerializedAs("TextStyle")] public TextStyle textStyle;
    public ImageColor imageColor;

    [ExecuteInEditMode]
    void OnValidate()
    {
        var txt = GetComponent<Text>();

        if (txt != null)
        {
            SetTextComponent(txt);
        }

        var img = GetComponent<Image>();

        if (img != null)
        {
            SetImageComponentColor(img);
        }
    }

    void SetImageComponentColor(Image img)
    {
        switch (imageColor)
        {
            case ImageColor.None:
                break;
            case ImageColor.Primary:
                // img.color = new Color(0.34f, 0f, 1f);
                // 250090
                break;
            case ImageColor.Secondary:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void SetTextComponent(Text txt)
    {
        txt.color = Color.white;
        
        switch (textStyle)
        {
            case TextStyle.None:
                break;
            case TextStyle.ScreenTitle:
                break;

            case TextStyle.PanelText:
                txt.fontSize = 22;
                txt.fontStyle = FontStyle.Bold;
                break;
            case TextStyle.PanelTitle:
                txt.fontSize = 34;
                txt.fontStyle = FontStyle.Normal;
                break;

            case TextStyle.KpiLabel:
                txt.fontSize = 34;
                txt.fontStyle = FontStyle.Normal;
                break;
            case TextStyle.KpiValue:
                txt.fontSize = 60;
                txt.fontStyle = FontStyle.Bold;
                break;

            case TextStyle.PlainDescription:
                txt.fontSize = 28;
                txt.fontStyle = FontStyle.Normal;
                break;
            case TextStyle.SmallText:
                txt.fontSize = 20;
                txt.fontStyle = FontStyle.Normal;
                
                break;
            case TextStyle.Link:
                txt.color = Visuals.Link();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}