using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using Unity.VisualScripting;
using log4net.Util;

/// <summary>
/// CSVファイルからステージとステージ用の当たり判定を
/// 作成するエディタ拡張
/// </summary>
public class CreateStageWindow : EditorWindow
{
    // ステージをまとめる親オブジェクト
    private GameObject _parent;

    [SerializeField]
    [Header("ステージパネルオブジェクト")]
    private GameObject[] _stagePartsPrefab = new GameObject[2];

    [SerializeField]
    [Header("ステージの当たり判定用オブジェクト")]
    private GameObject[] _stageCollisionPrefab = new GameObject[2];

    private string _filepath;

    // 行、列
    int _lineLength;
    int _rowLength;
    //これの二重配列に区切られたデータが格納される[行][列]で読み取れる
    private string[,] _csvDateArray;

    [MenuItem("CreateStage/Window")]
    // 静的にするのを忘れない
    private static void ShowWindow()
    {
        CreateStageWindow window = GetWindow<CreateStageWindow>();
    }

    
    private void OnGUI()
    {
        EditorGUILayout.LabelField("ステージオブジェクト配置");
        EditorGUILayout.LabelField("ステージファイル");
        EditorGUILayout.LabelField(_filepath);
        
        _stagePartsPrefab[0] = EditorGUILayout.ObjectField(
            "ノーマルパネル", _stagePartsPrefab[0], typeof(GameObject), true) as GameObject;

        _stageCollisionPrefab[0] = EditorGUILayout.ObjectField(
            "ノーマルパネル用当たり判定メッシュ", _stageCollisionPrefab[0], typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("ファイルを開く"))
        {
            _filepath = EditorUtility.OpenFilePanel("aaa", "", "csv");

            if (_filepath == "") return;
        }

        if(GUILayout.Button("読み込み"))
        {
            ReadCSV();
            ApplyStageObject();
        }

        EditorGUILayout.LabelField("ステージの当たり判定用メッシュを作成して結合");
        if (GUILayout.Button("作成して配置"))
        {
            ReadCSV();
            ApplyStageCollision();
            CombineCollisionMesh();
        }
    }

    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    private void ReadCSV()
    {
        // 読み込んだファイルをストリームに置き換える
        StreamReader sr = new StreamReader(_filepath);

        //ストリームをstringに変える
        string strStream = sr.ReadToEnd();

        // StringSplitOptionを設定 "カンマとカンマに何もなかったら格納しないことにする"
        System.StringSplitOptions options = System.StringSplitOptions.RemoveEmptyEntries;

        //行で分ける
        string[] line = strStream.Split(new char[] { '\r', '\n' }, options);

        //区切る文字を設定　この場合カンマ区切りなのでカンマを設定する
        char[] spilter = new char[1] { ',' };

        // 行の数設定
        _lineLength = line.Length;
        _rowLength = line[0].Split(spilter, options).Length;

        //データ格納用配列の初期化
        _csvDateArray = new string[_lineLength, _rowLength];

        for (int i = 0; i < _lineLength; ++i)
        {
            string[] sDate = line[i].Split(spilter, options);
            for (int j = 0; j < _rowLength; ++j)
            {
                _csvDateArray[i, j] = sDate[j];
            }
        }
    }

    /// <summary>
    /// ステージオブジェクト配置
    /// </summary>
    private void ApplyStageObject()
    {
        //新しい親オブジェクト作成。これの子オブジェクトに生成
        _parent = new GameObject("stage");

        GameObject obj;
        BoxCollider rend;
        for (int i = 0; i < _lineLength; i++)
        {
            for (int j = 0; j < _rowLength; j++)
            {
                int caseNum = int.Parse(_csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = _stagePartsPrefab[caseNum];

                rend = obj.GetComponentInChildren<BoxCollider>();
                //GameObjectとしてPrefabを作成
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                //Prefabの間隔等の設定
                Vector3 offset = new Vector3(rend.size.x * (obj.transform.position.x + i),
                                            rend.size.y * obj.transform.position.y,
                                            rend.size.z * (obj.transform.position.z + j));

                clone.transform.Translate(offset);//間隔設定を適用
                clone.transform.SetParent(_parent.transform);
            }
        }
    }

    /// <summary>
    /// ステージの当たり判定用メッシュ配置
    /// </summary>
    private void ApplyStageCollision()
    {
        //新しい親オブジェクト作成。これの子オブジェクトに生成
        _parent = new GameObject("stageCollision");
        _parent.AddComponent<MeshFilter>();

        // 当たり判定の位置を微調整
        _parent.transform.position = new Vector3(0f, 0.1f, 0f);

        GameObject obj;
        BoxCollider collider;
        for (int i = 0; i < _lineLength; i++)
        {
            for (int j = 0; j < _rowLength; j++)
            {
                int caseNum = int.Parse(_csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = _stageCollisionPrefab[caseNum];

                collider = obj.GetComponentInChildren<BoxCollider>();
                // GameObjectとしてPrefabを作成
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                // Prefabの間隔等の設定
                Vector3 offset = new Vector3(obj.transform.localScale.x * (obj.transform.position.x + i),
                                             obj.transform.localScale.y *  obj.transform.position.y,
                                             obj.transform.localScale.z * (obj.transform.position.z + j)
                                           );
                //間隔を設定
                clone.transform.Translate(offset);
                clone.transform.SetParent(_parent.transform);
            }
        }
    }

    /// <summary>
    /// 当たり判定用メッシュを結合
    /// </summary>
    private void CombineCollisionMesh()
    {
        MeshFilter[] meshFilters = _parent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        // メッシュを結合
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        // 結合したメッシュをMeshFilterに設定
        _parent.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        _parent.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        _parent.gameObject.SetActive(true);

        // MeshColliderを追加
        _parent.AddComponent<MeshCollider>();
    }
}