﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// editing route mode
public partial class SimpleUI
{
    static string searchUrl = "";
    static Vector2 searchScrollPosition = Vector2.zero;

    public static bool renameUrlRecursively = true;
    public static string newEditingUrl = "";

    public static string newUrl = "";
    public static string newName = "";
    public static string newPath = "";

    void RenderChosenPrefab()
    {
        if (!isConcreteUrlChosen)
        {
            // pick concrete URL
            RenderUrlsWhichAreAttachedToSamePrefab();
        }
        else
        {
            if (isUrlEditingMode)
            {
                if (isUrlRemovingMode)
                {
                    RenderUrlRemovingMode();
                }
                else
                {
                    RenderEditingPrefabMode();
                }
            }
            else
                RenderLinkToEditing();
        }
    }

    void RenderUrlsWhichAreAttachedToSamePrefab()
    {
        var chosenPrefab = prefabs[ChosenIndex];
        var samePrefabUrls = prefabs.Where(p => p.AssetPath.Equals(chosenPrefab.AssetPath));

        Label("Prefab " + chosenPrefab.Name + " is attached to these urls");
        Label("Choose proper one!");

        Space();
        RenderPrefabs(samePrefabUrls);
    }

    void RenderLinkToEditing()
    {
        var index = ChosenIndex;
        var prefab = prefabs[index];

        Label(prefab.Url);

        if (Button("Edit prefab"))
        {
            isUrlEditingMode = true;
            isUrlRemovingMode = false;

            newUrl = prefab.Url;
            newEditingUrl = newUrl;
            newPath = prefab.AssetPath;
            newName = prefab.Name;
        }

        Space();
        RenderPrefabs();
    }

    string WrapStringWithTwoSlashes(string str)
    {
        str = WrapStringWithLeftSlash(str);
        str = WrapStringWithRightSlash(str);

        return str;
    }

    string WrapStringWithLeftSlash(string str)
    {
        if (!str.StartsWith("/"))
            str = "/" + str;

        return str;
    }

    string WrapStringWithRightSlash(string str)
    {
        if (!str.EndsWith("/"))
            str = str + "/";

        return str;
    }

    string TrimSlashes(string str)
    {
        // no trimming in / route
        if (str.Equals("/"))
            return str;

        return str.TrimStart('/').TrimEnd('/');
    }


    string ReplaceUrlInCode(string text, string from, string to)
    {
        var txt = text;

        // a/b/c
        // ==
        // a/b/c/
        // ==
        // /a/b/c
        // ==
        // /a/b/c/

        //txt = txt.Replace(WrapStringWithTwoSlashes(from), WrapStringWithTwoSlashes(to)); // two slashes
        //txt = txt.Replace(WrapStringWithLeftSlash(from), WrapStringWithLeftSlash(to)); // left slashes
        //txt = txt.Replace(WrapStringWithRightSlash(from), WrapStringWithRightSlash(to)); // right slashes
        //txt = txt.Replace(TrimSlashes(from), TrimSlashes(to)); // no slashes

        return text.Replace(from, to);
    }

    bool RenameUrl(string route, string from, string to)
    {
        var matches = WhatUsesComponent(route, allReferencesFromAssets);
        var codeRefs = WhichScriptReferencesConcreteUrl(route);

        try
        {
            AssetDatabase.StartAssetEditing();

            Debug.Log($"Replacing {from} to {to} in {route}");

            Print("Rename in assets");
            foreach (var match in matches)
            {
                if (match.IsNormalPartOfNestedPrefab)
                    continue;

                var asset = match.Asset;
                //var asset = AssetDatabase.LoadAssetAtPath<GameObject>(match.PrefabAssetPath);
                //var asset = AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(match.PrefabAssetPath));

                var component = match.Component;

                // edit URL property
                bool addedSlash = false;
                var formattedUrl = component.Url;
                if (!formattedUrl.StartsWith("/"))
                {
                    addedSlash = true;
                    formattedUrl = "/" + formattedUrl;
                }
                var newUrl2 = formattedUrl.Replace(from, to);
                if (addedSlash)
                    newUrl2 = newUrl2.TrimStart('/');

                Debug.Log($"Renaming {component.Url} => {newUrl2} on component {match.ComponentName} in {match.PrefabAssetPath}");
                //component.Url = newUrl2;


                // saving changes
                if (isSceneAsset(match.PrefabAssetPath))
                {
                    // if scene
                    // save change in scene

                    // https://forum.unity.com/threads/scripted-scene-changes-not-being-saved.526453/

                    Debug.Log("Set scene as dirty");
                    component.Url = newUrl2;

                    EditorUtility.SetDirty(component);
                    //GameObjectUtility.RemoveMonoBehavioursWithMissingScript(asset);
                    EditorSceneManager.SaveScene(asset.scene);

                    //var saved = EditorSceneManager.SaveScene(asset.scene);

                    //if (saved)
                    //    Debug.Log("SUCCEED Save changes in scene: " + match.PrefabAssetPath);
                    //else
                    //    Debug.Log("FAILED Save changes in scene: " + match.PrefabAssetPath);
                }

                if (isPrefabAsset(match.PrefabAssetPath))
                {
                    // if prefab
                    // save change in prefab

                    Debug.Log("Saving prefab: " + match.ComponentName);

                    //EditorUtility.SetDirty(component);

                    // https://docs.unity3d.com/2020.1/Documentation/ScriptReference/PrefabUtility.EditPrefabContentsScope.html
                    using (var editingScope = new PrefabUtility.EditPrefabContentsScope(match.PrefabAssetPath))
                    {
                        var prefabRoot = editingScope.prefabContentsRoot;

                        var prefabbedComponent = prefabRoot.GetComponentsInChildren<OpenUrl>(true)[match.ComponentID];

                        Debug.Log($"Renaming component with url={prefabbedComponent.Url} to {newUrl2}");

                        prefabbedComponent.Url = newUrl2;

                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(prefabRoot);
                    }

                    //GameObjectUtility.RemoveMonoBehavioursWithMissingScript(asset);

                    //PrefabUtility.SaveAsPrefabAsset(asset, match.PrefabAssetPath);
                    //PrefabUtility.UnloadPrefabContents(asset);
                }
            }

            Print("Rename in code");
            foreach (var match in codeRefs)
            {
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(match.ScriptName);

                var replacedText = ReplaceUrlInCode(script.text, from, to);

                StreamWriter writer = new StreamWriter(match.ScriptName, false);
                writer.Write(replacedText);
                writer.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occured during renaming " + route);

            return false;
        }
        finally
        {
            //AssetDatabase.SaveAssets();
            AssetDatabase.StopAssetEditing();

            var prefab = GetPrefabByUrl(route);
            prefab.Url = newEditingUrl;

            UpdatePrefab(prefab);
        }

        return true;
    }

    void RenderStatButtons(SimpleUISceneType pref)
    {
        Space();
        Space();
        if (pref.Usages > 0 && GUILayout.Button("Reset Counter"))
        {
            pref.Usages = 0;

            UpdatePrefab(pref);
        }

        var maxUsages = prefabs.Max(p => p.Usages);
        if (pref.Usages < maxUsages && GUILayout.Button("Prioritize"))
        {
            pref.Usages = maxUsages + 1;

            UpdatePrefab(pref);
        }
    }

    void RenderRenameUrlButton(SimpleUISceneType prefab)
    {
        Space();

        renameUrlRecursively = EditorGUILayout.ToggleLeft("Rename subroutes too", renameUrlRecursively);

        Space();
        if (renameUrlRecursively)
            EditorGUILayout.HelpBox("Renaming this url will lead to renaming these urls too...", MessageType.Warning);
        else
            EditorGUILayout.HelpBox("Will only rename THIS url", MessageType.Warning);

        List<string> RenamingUrls = new List<string>();
        List<string> RenamingCodeUrls = new List<string>();

        if (renameUrlRecursively)
        {
            Space();
            var subroutes = GetSubUrls(prefab.Url, true);

            RenamingUrls.Add(prefab.Url);

            foreach (var route in subroutes)
            {
                RenamingUrls.Add(route.Url);
            }

            // render
            foreach (var route in RenamingUrls)
            {
                BoldLabel(route);
            }
        }

        var phrase = renameUrlRecursively ? "Rename url & subUrls" : "Rename THIS url";

        var matches = WhatUsesComponent(newUrl, allReferencesFromAssets);
        var referencesFromCode = WhichScriptReferencesConcreteUrl(prefab.Url);

        // references from prefabs & scenes
        var names = matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)}</b> " + SimpleUI.GetTrimmedPath(m.PrefabAssetPath)).ToList();
        var routes = matches.Select(m => m.PrefabAssetPath).ToList();

        // references from code
        foreach (var occurence in referencesFromCode)
        {
            names.Add($"<b>Code</b> {SimpleUI.GetTrimmedPath(occurence.ScriptName)}");
            routes.Add(occurence.ScriptName);
        }

        Space();
        if (Button(phrase))
        {
            if (EditorUtility.DisplayDialog("Do you want to rename url " + prefab.Url, "This action will rename url and subUrls in X prefabs, Y scenes and Z script files.\n\nPRESS CANCEL IF YOU HAVE UNSAVED PREFAB OR SCENE OR CODE CHANGES", "Rename", "Cancel"))
            {
                Print("Rename starts now!");

                foreach (var url in RenamingUrls)
                {
                    Print("Rename URL " + url);
                    RenameUrl(url, newUrl, newEditingUrl);
                    Print("----------------");
                }
            }
        }

        //EditorUtility.DisplayProgressBar("Renaming url", "Info", UnityEngine.Random.Range(0, 1f));
    }

    void RenderEditingPrefabMode()
    {
        var index = ChosenIndex;
        var prefab = prefabs[index];

        Label(prefab.Url);

        if (Button("Go back"))
        {
            isUrlEditingMode = false;
        }

        var prevUrl = newUrl;
        var prevName = newName;
        var prevPath = newPath;

        Space();



        Label("Edit url");
        newEditingUrl = EditorGUILayout.TextField("Url", newEditingUrl);

        if (newEditingUrl.Length > 0)
        {
            newName = EditorGUILayout.TextField("Name", newName);

            if (newName.Length > 0)
            {
                newPath = EditorGUILayout.TextField("Asset Path", newPath);
            }
        }



        // if data changed, save it
        if (!prevPath.Equals(newPath) || !prevName.Equals(newName))
        {
            //prefab.Url = newEditingUrl;
            prefab.Name = newName;
            prefab.AssetPath = newPath;

            UpdatePrefab(prefab);
        }

        // if Url changed, rename everything
        if (!newUrl.Equals(newEditingUrl))
        {
            RenderRenameUrlButton(prefab);
        }

        Space();
        RenderRootPrefab();
        RenderSubroutes();

        // TODO url or path?
        // opened another url
        if (!newPath.Equals(prevPath))
            return;

        RenderStatButtons(prefab);

        Space(450);
        if (GUILayout.Button("Remove URL"))
        {
            isUrlRemovingMode = true;
            //prefabs.RemoveAt(index);
            //SaveData();
        }
    }
}
