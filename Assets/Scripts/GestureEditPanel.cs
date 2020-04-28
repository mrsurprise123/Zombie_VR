using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Edwon.VR.Gesture;
using Edwon.VR;

public class GestureEditPanel : MonoBehaviour {
    public GameObject gestureExampleItem;
    public Material material;
    private Text GestureName;
    private VRGestureSettings gestureSettings;
    private VRGestureRig gestureRig;
    private List<GestureExample> gestureExamples = new List<GestureExample>();
    private List<GameObject> examples = new List<GameObject>();
    private Button back_button;
    private Button record_button;
    private string m_gestureName;
    private bool isRecording = false;
    private void Awake()
    {
        Init();
        gestureSettings = Utils.GetGestureSettings();
        gestureRig = VRGestureRig.GetPlayerRig(gestureSettings.playerID);
        EventCenter.AddListener<string>(EventDefine.ShowGestureEditPanel,ShowGestureEditPanel);
        EventCenter.AddListener(EventDefine.FinishedGestureRecord, FinishedGestureRecord);
        EventCenter.AddListener<bool>(EventDefine.UIPointHovering, UIPointHovering);
    }
    private void Init()
    {
        GestureName = transform.Find("Title").GetComponent<Text>();
        back_button = transform.Find("Back_Button").GetComponent<Button>();
        record_button = transform.Find("Record_Button").GetComponent<Button>();
        back_button.onClick.AddListener(() => {
            BeginTrainNeuralNetwork();
            EventCenter.Broadcast(EventDefine.ShowGestureInfPanel);
            gameObject.SetActive(false);
            isRecording = false;
        });
        record_button.onClick.AddListener(() => {
            BeginGestureRecord();
        });
        gameObject.SetActive(false);
    }
    private void FinishedGestureRecord()
    {
        GetAllGestureExamples(m_gestureName);
        generateExamplesItem();
    }
    //Start record gesture
    private void BeginGestureRecord()
    {
        isRecording = true;
        gestureRig.BeginReadyToRecord(m_gestureName);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventDefine.ShowGestureEditPanel, ShowGestureEditPanel);
        EventCenter.RemoveListener(EventDefine.FinishedGestureRecord, FinishedGestureRecord);
        EventCenter.RemoveListener<bool>(EventDefine.UIPointHovering, UIPointHovering);
    }
    private void ShowGestureEditPanel(string gestureName)
    {
        m_gestureName = gestureName;
        gameObject.SetActive(true);
        GestureName.text = gestureName;
        BeginEditGesture(gestureName);
    }
    private void BeginTrainNeuralNetwork()
    {
        gestureSettings.BeginTraining(FinishTraining);
    }
    private void FinishTraining(string networlName)
    {

    }
    private void BeginEditGesture(string gestureName)
    {
        gestureRig.uiState = VRGestureUIState.Editing;
        gestureRig.BeginEditing(gestureName);
        GetAllGestureExamples(gestureName);
        generateExamplesItem();
    }
    public void GetAllGestureExamples(string gestureName)
    {
        gestureExamples = Utils.GetGestureExamples(gestureName, gestureSettings.currentNeuralNet);
        foreach (var item in gestureExamples)
        {
            if (item.raw)
            {
                item.data = Utils.SubDivideLine(item.data);
                item.data = Utils.DownScaleLine(item.data);
            }
        }
    }
    //if line from the pistol collide the UI
    private void UIPointHovering(bool value)
    {
        if (isRecording)
        {
            if (value)
            {
                gestureRig.uiState = VRGestureUIState.Gestures;
            }
            else
            {
                BeginGestureRecord();
            }
        }
    }
    public void generateExamplesItem()
    {
        foreach (var item in examples)
        {
            Destroy(item);
        }
        examples.Clear();
        for (int i = 0; i < gestureExamples.Count; i++)
        {
            GameObject ob = Instantiate(gestureExampleItem, transform.Find("ParentGesture"));
            ob.GetComponent<GestureExampleItem>().Init(gestureExamples[i].name, i);
            LineRenderer line = ob.GetComponentInChildren<LineRenderer>();
            line.useWorldSpace = false;
            line.material = material;
            line.startColor = Color.green;
            line.endColor = Color.blue;
            float lineWidth = 0.01f;
            line.startWidth = lineWidth - (lineWidth * 0.5f);
            line.endWidth = lineWidth + (lineWidth * 0.5f);
            line.positionCount = gestureExamples[i].data.Count;
            for (int j = 0; j < gestureExamples[i].data.Count; j++)
            {
                gestureExamples[i].data[j] = gestureExamples[i].data[j] * 40;
            }
            line.SetPositions(gestureExamples[i].data.ToArray());
            examples.Add(ob);
        }
    }
}
