using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConfigureZombie : MonoBehaviour {
    private void CreatModel(GameObject go, GameObject friend, Material mat, Mesh mesh)
    {
        go.AddComponent<BodyPartDrop>().SetFriend(friend);

        GameObject model = new GameObject("Model");
        model.transform.position = go.transform.position;
        model.transform.rotation = go.transform.rotation;
        model.transform.SetParent(go.transform);
        model.AddComponent<MeshRenderer>().sharedMaterial = mat;
        model.AddComponent<MeshFilter>().sharedMesh = mesh;
        model.SetActive(false);
    }
    public void AddMeshCollider()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var smr in skinnedMeshRenderers)
        {
            ZombieController controller = GetComponent<ZombieController>();
            GameObject go = new GameObject(smr.transform.name);
            go.tag = "Zombie";
            go.transform.position = smr.transform.position;
            go.transform.rotation = smr.transform.rotation;
            go.AddComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
            go.GetComponent<MeshCollider>().convex = true;
            go.GetComponent<MeshCollider>().inflateMesh = true;
            if (smr.transform.name == "ArmL")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Left, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);

                Transform parent = GameObject.Find("Shoulder_L").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "ArmR")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Right, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);

                Transform parent = GameObject.Find("Shoulder_R").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "Body")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Other, controller);
                Transform parent = GameObject.Find("Root_M").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "ForeArmL")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Left, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);

                Transform parent = GameObject.Find("Elbow_L").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "ForeArmR")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Right, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);

                Transform parent = GameObject.Find("Elbow_R").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "HandL")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Left, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);
                Transform parent = GameObject.Find("Wrist_L").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "HandR")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Right, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);
                Transform parent = GameObject.Find("Wrist_R").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "Head")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Head, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);
                Transform parent = GameObject.Find("Head_M").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "KneeL")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Other, controller);
                Transform parent = GameObject.Find("Knee_L").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "KneeR")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Other, controller);
                Transform parent = GameObject.Find("Knee_R").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "LegL")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Other, controller);
                Transform parent = GameObject.Find("Hip_L").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "LegR")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Other, controller);
                Transform parent = GameObject.Find("Hip_R").transform;
                go.transform.SetParent(parent);
            }
            else if (smr.transform.name == "Neck")
            {
                go.AddComponent<ZombieHit>().Set(BodyPartType.Other, controller);
                CreatModel(go, smr.gameObject, smr.sharedMaterial, smr.sharedMesh);
                Transform parent = GameObject.Find("Neck_M").transform;
                go.transform.SetParent(parent);
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ConfigureZombie))]
public class ConfigureZombieEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ConfigureZombie configure = target as ConfigureZombie;
        if (GUILayout.Button("ConfigureZombie"))
        {
            configure.gameObject.AddComponent<ZombieController>();
            configure.AddMeshCollider();
            configure.gameObject.AddComponent<NavMeshAgent>();
            configure.GetComponent<NavMeshAgent>().angularSpeed = 200;
            configure.GetComponent<NavMeshAgent>().radius = 0.2f;
            configure.GetComponent<NavMeshAgent>().height = 1.75f;
            configure.gameObject.AddComponent<AudioSource>();
            Debug.Log("配置成功");
        }
    }
}
#endif