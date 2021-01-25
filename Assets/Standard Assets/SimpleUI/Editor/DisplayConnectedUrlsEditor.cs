using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayConnectedUrls))]
public class DisplayConnectedUrlsEditor : Editor
{
    int w = 225;
    int h = 150;
    int off = 5;

    static int routeSelected = -1;
    static int referenceSelected = -1;
    static int referenceFromSelected = -1;

    static Vector2 scrollPosition = Vector2.zero;
    static Vector2 scrollPosition2 = Vector2.zero;
    static Vector2 scrollPosition3 = Vector2.zero;

    //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);

    // Cached data
    List<SimpleUI.PrefabMatchInfo> matches1;
    List<SimpleUI.UsageInfo> referencesFromCode;

    string currentUrl => SimpleUI.GetCurrentUrl();

    private void OnSceneGUI()
    {
        if (EditorApplication.isCompiling)
            return;

        matches1 = SimpleUI.matches1;
        referencesFromCode = SimpleUI.urlMatchesInCode1;

        RenderUpperAndLowerRoutes(currentUrl);
        RenderReferencesToUrl(currentUrl);
        RenderReferencesFromUrl(currentUrl);
    }

    //// TODO make this call asynchronous
    //private void OnEnable()
    //{
    //    if (EditorApplication.isCompiling)
    //        return;

    //    matches1 = SimpleUI.matches1;
    //    urlMatchesInCode1 = SimpleUI.urlMatchesInCode1;

    //    //matches1 = SimpleUI.WhatUsesComponent<OpenUrl>(currentUrl);
    //    //urlMatchesInCode1 = SimpleUI.WhatScriptReferencesConcreteUrl(currentUrl);
    //}

    private void OnDisable()
    {
        ClearData();
    }

    void ClearData()
    {
        routeSelected = -1;
        referenceSelected = -1;
        referenceFromSelected = -1;
    }

    void Label(string text)
    {
        GUIStyle localStyle = new GUIStyle(GUI.skin.label);
        localStyle.normal.textColor = Color.white;

        GUILayout.Label(text, localStyle);
    }

    string Urlify(string url)
    {
        if (!url.StartsWith("/"))
            url = "/" + url;

        return url;
    }

    string GetPrettyNameForUrl(string url, string currentUrl)
    {
        url = Urlify(url);

        bool isDirectSubUrl = SimpleUI.isSubRouteOf(url, currentUrl, false);
        bool isSubSubUrl = !isDirectSubUrl && SimpleUI.isSubRouteOf(url, currentUrl, true);

        var prefix = "<- ";

        if (isDirectSubUrl)
        {
            prefix = "\u2198 ";
        }

        if (isSubSubUrl)
        {
            prefix = "\u2198 \u2198 ";
        }


        return prefix + SimpleUI.GetPrettyNameForExistingUrl(url);
    }


    void RenderReferencesFromUrl(string currentUrl)
    {
        if (matches1 == null)
            return;

        GUILayout.BeginArea(new Rect(Screen.width - w - off, off + h + off, w, h));
        //GUILayout.BeginArea(new Rect(off, off + h, w, h));

        var matches = matches1.Where(m => m.PrefabAssetPath.Equals(SimpleUI.GetCurrentAssetPath())).ToList();

        if (matches.Any())
            Label("References FROM url");

        scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);

        var prevRoute = referenceSelected;
        referenceSelected = GUILayout.SelectionGrid(referenceSelected, matches.Select(m => GetPrettyNameForUrl(m.URL, currentUrl)).ToArray(), 1);

        if (prevRoute != referenceSelected)
        {
            SimpleUI.OpenPrefabByUrl(matches[referenceSelected].URL);
            referenceSelected = -1;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderReferencesToUrl(string currentUrl)
    {
        if (matches1 == null)
            return;

        GUILayout.BeginArea(new Rect(off, off, w, h));

        var matches = matches1.Where(m => m.URL.Equals(currentUrl.TrimStart('/'))).ToList();

        if (matches.Any() || referencesFromCode.Any())
            Label("References to THIS url");

        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
        //urlMatchesInCode1

        var names = matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)} </b>" + m.PrefabAssetPath.Substring(m.PrefabAssetPath.LastIndexOf("/"))).ToList();
        var routes = matches.Select(m => m.PrefabAssetPath).ToList();

        foreach (var occurence in referencesFromCode)
        {
            var trimmedScriptName = occurence.ScriptName.Substring(occurence.ScriptName.LastIndexOf('/'));

            names.Add("<b>Script </b>" + trimmedScriptName + " #" + occurence.Line);
            routes.Add(occurence.ScriptName);
        }

        var prevRoute = referenceFromSelected;
        referenceFromSelected = GUILayout.SelectionGrid(referenceFromSelected, names.ToArray(), 1);

        if (prevRoute != referenceFromSelected)
        {
            GUILayout.EndScrollView();
            GUILayout.EndArea();

            SimpleUI.OpenPrefabByAssetPath(routes[referenceFromSelected]);
            //SimpleUI.OpenAssetByPath(routes[referenceFromSelected]);
            referenceFromSelected = -1;

            return;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderUpperAndLowerRoutes(string currentUrl)
    {
        GUILayout.BeginArea(new Rect(Screen.width - w - off, off, w, h));

        Label("Navigation");
        Label(currentUrl);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.BeginVertical();

        //
        var routes = new List<string>();
        var names = new List<string>();

        RenderRootLink(currentUrl, ref routes, ref names);
        RenderSubRoutes(currentUrl, ref routes, ref names);


        var prevRoute = routeSelected;
        routeSelected = GUILayout.SelectionGrid(routeSelected, names.ToArray(), 1);

        if (prevRoute != routeSelected)
        {
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndArea();

            SimpleUI.OpenPrefabByUrl(routes[routeSelected]);
            routeSelected = -1;

            return;
        }

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderSubRoutes(string currentUrl, ref List<string> routes, ref List<string> names)
    {
        var subRoutes = SimpleUI.GetSubUrls(currentUrl, false).OrderByDescending(p => p.Usages).ToList();

        for (var i = 0; i < subRoutes.Count(); i++)
        {
            var pref = subRoutes[i];
            var name = $"\u2198 {pref.Name}";

            routes.Add(pref.Url);
            names.Add(name);
        }
    }

    void RenderRootLink(string currentUrl, ref List<string> routes, ref List<string> names)
    {
        var root = SimpleUI.GetUpperUrl(currentUrl);

        bool hasRoot = !root.Equals(currentUrl);
        if (hasRoot)
        {
            var name = $"\u2B06 ({root})";

            routes.Add(root);
            names.Add(name);
        }
    }
}
