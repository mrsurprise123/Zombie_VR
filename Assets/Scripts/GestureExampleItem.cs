using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Edwon.VR;
using Edwon.VR.Gesture;

public class GestureExampleItem : MonoBehaviour {

    private VRGestureSettings gestureSettings;
    private string gestureName;
    private int lineNumber;
    public void Init(string gestureName, int lineNumber)
    {
        this.gestureName = gestureName;
        this.lineNumber = lineNumber;
    }
	private void Awake()
    {
        gestureSettings = Utils.GetGestureSettings();
        GetComponent<Button>().onClick.AddListener(DeleteThisExample);
    }
    private void DeleteThisExample()
    {
        Gesture gesture = gestureSettings.FindGesture(gestureName);
        gesture.exampleCount--;
        Utils.DeleteGestureExample(gestureSettings.currentNeuralNet, gestureName, lineNumber);
        GetComponentInParent<GestureEditPanel>().GetAllGestureExamples(gestureName);
        GetComponentInParent<GestureEditPanel>().generateExamplesItem();
    }
}
