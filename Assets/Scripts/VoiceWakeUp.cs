using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceWakeUp : MonoBehaviour {
    //Phrase confidenceLevel
    public ConfidenceLevel confidenceLevel = ConfidenceLevel.Medium;
    //The keyword that can be recognized
    public string[] keyWordArr;
    //The recognizer of keyword
    private PhraseRecognizer phraseRecognizer;

    private void Awake()
    {
        phraseRecognizer = new KeywordRecognizer(keyWordArr, confidenceLevel);
        //register the phraseRecognizer
        phraseRecognizer.OnPhraseRecognized += PhraseRecognizer_OnPhraseRecognized;
        phraseRecognizer.Start();
    }
    private void OnDestroy()
    {
        phraseRecognizer.Dispose();
    }

    private void PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //print(args.text);
        //if(args.text == keyWordArr[0])//load game scene
        //{
        //    EventCenter.Broadcast(EventDefine.StartGame);
        //}
        //else if (args.text == keyWordArr[1])//退出游戏
        //{
        //    Application.Quit();
        //}
        //else if (args.text == keyWordArr[2])//换弹夹
        //{
        //    EventCenter.Broadcast(EventDefine.LeftRelaod);
        //    EventCenter.Broadcast(EventDefine.RightReload);
        //}
        //else if (args.text == keyWordArr[3])//换手枪
        //{
        //    if (GetComponentInChildren<RadialMenuManager>() != null)
        //        GetComponentInChildren<RadialMenuManager>().OnUsePistolClick();
        //}
        //else if (args.text == keyWordArr[4])//使用炸弹
        //{
        //    if (GetComponentInChildren<RadialMenuManager>() != null)
        //        GetComponentInChildren<RadialMenuManager>().OnUseBombClick();
        //}
        //else if (args.text == keyWordArr[5])//手势识别 
        //{
        //    if (GetComponentInChildren<RadialMenuManager>() != null)
        //        GetComponentInChildren<RadialMenuManager>().OnUseGestureClick();
        //}
    }
}
