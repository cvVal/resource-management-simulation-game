using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // This function is called when a new color is selected in the color picker
        if (MainManager.Instance != null)
        {
            MainManager.Instance.TeamColor = color;
            Debug.Log("New team color selected: " + color);
        }
        else
        {
            Debug.LogWarning("MainManager.Instance is null when trying to set team color");
        }
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        // Only select color if MainManager exists and has been initialized
        if (MainManager.Instance != null)
        {
            ColorPicker.SelectColor(MainManager.Instance.TeamColor);
        }
        else
        {
            Debug.LogWarning("MainManager.Instance is null in MenuUIHandler.Start()");
            // You can set a default color here if needed
            ColorPicker.SelectColor(Color.white);
        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.SaveColor();
        }
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // This will quit the application when running outside of the editor
#endif
    }

    public void SaveColorClicked()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.SaveColor();
            Debug.Log("Color saved: " + MainManager.Instance.TeamColor);
        }
        else
        {
            Debug.LogWarning("MainManager.Instance is null when trying to save color");
        }
    }

    public void LoadColorClicked()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.LoadColor();
            ColorPicker.SelectColor(MainManager.Instance.TeamColor);
            Debug.Log("Color loaded: " + MainManager.Instance.TeamColor);
        }
        else
        {
            Debug.LogWarning("MainManager.Instance is null when trying to load color");
        }
    }
}
