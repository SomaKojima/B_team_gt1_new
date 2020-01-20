using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    public Material material;

    [SerializeField]
    public List<GameObject> materials;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Object t;
    //SerializedProperty color;
    //SerializedProperty obj;

    //private void OnEnable()
    //{
    //    t = target;
    //    color = serializedObject.FindProperty("colorBuf");
    //    obj = serializedObject.FindProperty("objBuf");
    //}
    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();
    //    EditorGUILayout.PropertyField(color);
    //    EditorGUILayout.PropertyField(obj);
        

    //    NewBehaviourScript _t = t as NewBehaviourScript;
    //    //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
    //    foreach (Renderer targetRenderer in objBuf.GetComponents<Renderer>())
    //    {
    //        foreach (Material _material in targetRenderer.materials)
    //        {
    //            _material.color = color.colorValue;
    //        }
    //    }

    //    //入力されたオブジェクトの子にも同様の処理を行う
    //    for (int i = 0; i < objBuf.transform.childCount; i++)
    //    {
    //        ChangeColorOfGameObject(objBuf.transform.GetChild(i).gameObject, colorBuf);
    //    }
    //}

    //private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    //{

    //    //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
    //    foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
    //    {
    //        foreach (Material material in targetRenderer.sharedMaterials)
    //        {
    //            material.color = color;
    //        }
    //    }

    //    //入力されたオブジェクトの子にも同様の処理を行う
    //    for (int i = 0; i < targetObject.transform.childCount; i++)
    //    {
    //        ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
    //    }

    //}
}


#if UNITY_EDITOR

[CustomEditor(typeof(NewBehaviourScript))]
public class FighterInspector : Editor
{
    Object t;
    SerializedProperty material;
    SerializedProperty materials;

    private void OnEnable()
    {
        t = target;
        material = serializedObject.FindProperty("material");
        materials = serializedObject.FindProperty("materials");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(material);

        EditorGUILayout.PropertyField(materials, true);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        NewBehaviourScript _t = t as NewBehaviourScript;

        var list = _t.materials;

        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in _t.GetComponents<Renderer>())
        {
            targetRenderer.material = _t.material;
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < _t.transform.childCount; i++)
        {
            ChangeMaterialOfGameObject(_t.transform.GetChild(i).gameObject, _t.material);
        }

        foreach (GameObject material in list)
        {
            if (material == null) continue;
            material.GetComponent<Renderer>().sharedMaterial.color = Color.white;
        }
    }

    private void ChangeMaterialOfGameObject(GameObject targetObject, Material material)
    {

        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material = material;
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeMaterialOfGameObject(targetObject.transform.GetChild(i).gameObject, material);
        }

    }
}

#endif // UNITY_EDITOR