using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager {
    private static Dictionary<string, Sprite> pathSprite = new Dictionary<string, Sprite>(); 
    public static Sprite LoadSprite(string path)
    {
        if (pathSprite.ContainsKey(path))
        {
            return pathSprite[path];
        }
        else
        {
            Sprite sp = Resources.Load<Sprite>(path);
            pathSprite.Add(path, sp);
            return sp;
        }
        
    }
}
