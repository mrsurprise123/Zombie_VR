using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    /// <summary>
    /// 图片名与图片的字典
    /// </summary>
    private static Dictionary<string, Sprite> m_SpriteDic = new Dictionary<string, Sprite>();
    private static Dictionary<string, GameObject> m_ObjDic = new Dictionary<string, GameObject>();

    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadSprite(string path)
    {
        if (m_SpriteDic.ContainsKey(path))
        {
            return m_SpriteDic[path];
        }
        else
        {
            Sprite sp = Resources.Load<Sprite>(path);
            m_SpriteDic.Add(path, sp);
            return sp;
        }
    }
    /// <summary>
    /// 加载预制体
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject LoadObj(string path)
    {
        if (m_ObjDic.ContainsKey(path))
        {
            return m_ObjDic[path];
        }
        else
        {
            GameObject go = Resources.Load<GameObject>(path);
            m_ObjDic.Add(path, go);
            return go;
        }
    }
}
