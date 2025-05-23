using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelect : MonoBehaviour
{
    public GameObject instructionText;
    // Sub-menus and selection objects
    private GameObject modeSelector;
    private GameObject EditSubMenu;
    private GameObject AnalysisSubMenu;
    private GameObject SketchSubMenu;
    private Button translateButton;
    private Button rotateButton;
    private Button editNodes;
    private Button scaleButton;
    private Button measureButton;
    private Button extrudeButton;
    private Button revolveButton;
    public InputHandler playerInputHandler;


    private List<Button> toggles;

    public enum state
    {
        Scan,
        Edit,
        Analysis,
        Sketch
    }

    public state mode;

    public Boolean TranslateActive;
    public Boolean RotateActive;
    public Boolean EditActive;
    public Boolean ScaleActive;
    public Boolean measureActive;
    public Boolean extrudeActive;
    public Boolean revolveActive;

    void Start()
    {
        mode = state.Scan;
        // Get UI buttons
        translateButton = GameObject.Find("TranslateTool").GetComponent<Button>();
        rotateButton = GameObject.Find("RotateTool").GetComponent<Button>();
        editNodes = GameObject.Find("PointEdit").GetComponent<Button>();
        scaleButton = GameObject.Find("Scaling").GetComponent<Button>();
        measureButton = GameObject.Find("Measure").GetComponent<Button>();
        extrudeButton = GameObject.Find("Extrude").GetComponent<Button>();
        revolveButton = GameObject.Find("Revolve").GetComponent<Button>();
        playerInputHandler = GameObject.Find("UserInputHandler").GetComponent<InputHandler>();

        // Get Sub Menus
        modeSelector = GameObject.Find("Mode");
        EditSubMenu = GameObject.Find("EditMenu");
        AnalysisSubMenu = GameObject.Find("Analysis");
        SketchSubMenu = GameObject.Find("SketchMenu");
        

        toggles = new List<Button>();
        toggles.Add(translateButton);
        toggles.Add(rotateButton);
        toggles.Add(editNodes);
        toggles.Add(scaleButton);
        toggles.Add(measureButton);
        toggles.Add(extrudeButton);
        toggles.Add(revolveButton);
        modeSelector.SetActive(false);


        // Clear tools on startup
        clearButtonActive();
        deactiveButtons();
    }

    float time;
    Boolean scanPeriodOver;
    void FixedUpdate()
    {
        if(scanPeriodOver) { return; }
        if(time < 10) {
            time += Time.deltaTime;
        }
        else {
            scanPeriodOver = true;
            instructionText.SetActive(false);
            activateButtons();
        }
    }

    public void selectTranslate() {
        clearButtonActive();
        translateButton.image.color = Color.gray;
        TranslateActive = true;
    }
    public void selectRotate() {
        clearButtonActive();
        rotateButton.image.color = Color.gray;
        RotateActive = true;
    }    
    public void pointEdit() {
        clearButtonActive();
        editNodes.image.color = Color.gray;
        EditActive = true;
    }
    public void scaleEdit() {
        clearButtonActive();
        scaleButton.image.color = Color.gray;
        ScaleActive = true;
    }
    public void measureeEdit() {
        clearButtonActive();
        measureButton.image.color = Color.gray;
        measureActive = true;
    }
    public void extrudeEdit() {
        clearButtonActive();
        extrudeButton.image.color = Color.gray;
        extrudeActive = true;
    }
    public void revolveEdit() {
        clearButtonActive();
        if (playerInputHandler.objectHeld.TryGetComponent<RevolveTool>(out RevolveTool revolve))
        {
            revolve.useRevolveTool();
        }
        revolveActive = true;
    }
    private void clearEnabledTools()
    {
        TranslateActive = false;
        RotateActive = false;
        EditActive = false;
        ScaleActive = false;
        measureActive = false;
        extrudeActive = false;
        revolveActive = false;
    }
    private void clearButtonActive()
    {
        foreach (Button currButton in toggles)
        {
            currButton.image.color = Color.white;
        }
        clearEnabledTools();
    }

    public void deactiveButtons() {
        foreach(Button currButton in toggles ) {
            currButton.gameObject.SetActive(false);
        }
        clearEnabledTools();
    }

    public void activateButtons() {
        changeModeState();
        foreach(Button currButton in toggles ) {
            currButton.gameObject.SetActive(true);
        }
        modeSelector.SetActive(true);
        mode = state.Edit;
        clearEnabledTools();
        instructionText.SetActive(false);
    }

    public void changeModeState()
    {
        TMP_Dropdown dropdown = modeSelector.GetComponent<TMP_Dropdown>();
        if (dropdown.value == 0)
        {
            AnalysisSubMenu.SetActive(false);
            EditSubMenu.SetActive(true);
            SketchSubMenu.SetActive(false);
            mode = state.Edit;
        }
        else if (dropdown.value == 1)
        {
            EditSubMenu.SetActive(false);
            AnalysisSubMenu.SetActive(true);
            SketchSubMenu.SetActive(false);
            mode = state.Analysis;
        }
        else if(dropdown.value == 2) {
            EditSubMenu.SetActive(false);
            AnalysisSubMenu.SetActive(false);
            SketchSubMenu.SetActive(true);
            mode = state.Sketch;
        }
    }
}
