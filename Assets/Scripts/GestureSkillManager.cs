using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR.Gesture;
using Edwon.VR;
public class GestureSkillManager{
    //Bond the gesture and skills
    private static Dictionary<string, string> GestureSkillDic = new Dictionary<string, string>();
    private static VRGestureSettings gestureSettings;
    private static string SkillName = "Skill";
    static GestureSkillManager()
    {
        gestureSettings = Utils.GetGestureSettings();
        GestureSkillDic = getGestureSkillDic();
    }
    private static Dictionary<string, string> getGestureSkillDic()
    {
        Dictionary<string, string> tempDic = new Dictionary<string, string>();
        if (PlayerPrefs.HasKey("GestureSkill"))
        {
            string gestureskill = PlayerPrefs.GetString("GestureSkill");
            string[] arr = gestureskill.Split(':');
            foreach (var item in arr)
            {
                string[] temp = item.Split('-');
                tempDic.Add(temp[0],temp[1]);
            }
        }
        else
        {
            for (int i = 0; i < gestureSettings.gestureBank.Count; i++)
            {
                tempDic.Add(gestureSettings.gestureBank[i].name, SkillName + (i + 1).ToString());
            }
            SaveGestureSkillDic(tempDic);
        }
        return tempDic;
    }
    private static void SaveGestureSkillDic(Dictionary<string, string> dic)
    {
        string temp = "";
        int index = 0;
        foreach (var item in dic)
        {
            temp += item.Key + '-' + item.Value;
            index++;
            if (index != dic.Count)
                temp += ':';
        }
        PlayerPrefs.SetString("GestureSkill",temp);
    }
    public static string GetSkillNameByGestureName(string gestureName)
    {
        if (GestureSkillDic.ContainsKey(gestureName))
        {
            return GestureSkillDic[gestureName];
        }
        return null;
    }
    public static void ChangeSkillName(string gestureName, string newSkillName)
    {
        if (GestureSkillDic.ContainsKey(gestureName))
        {
            GestureSkillDic[gestureName] = newSkillName;
            SaveGestureSkillDic(GestureSkillDic);
        }
    }
    public static Sprite GetSkillSpriteByGestureName(string gestureName)
    {
        if (GestureSkillDic.ContainsKey(gestureName))
        {
            return ResourceManager.LoadSprite(GestureSkillDic[gestureName]);
        }
        return null;
    }
}
