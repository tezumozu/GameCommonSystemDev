using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static T m_Instance = null;
 
    public static T instance
    {
        get
        {
            if( m_Instance != null )
            {
                return m_Instance;
            }
 
            //対応する型のインスタンスを取得
            System.Type type = typeof(T);
            T instance = GameObject.FindObjectOfType(type) as T;
 
            //インスタンスがない場合のエラー
            if( instance == null ){
                string typeName = type.ToString();
 
                GameObject gameObject = new GameObject( typeName, type );
                instance = gameObject.GetComponent<T>();
 
                if( instance == null )
                {
                    Debug.LogError("Problem during the creation of " + typeName,gameObject );
                }
            
            //ある場合初期化
            } else {
                Initialize(instance);
            }

            return m_Instance;
        }
    }

    //初期化処理
    static void Initialize(T instance)
    {
        //インスタンスがない場合
        if( m_Instance == null )
        {
            m_Instance = instance;
 
            m_Instance.OnInitialize();
        }

        //すでにインスタンスがある場合
        else if( m_Instance != instance ){
            
            DestroyImmediate( instance.gameObject );
        }
    }
 
    //オブジェクト破壊
    static void Destroyed(T instance)
    {
        if( m_Instance == instance )
        {
            m_Instance.OnFinalize();
 
            m_Instance = null;
        }
    }
 
    //各子オブジェクトの初期化処理
    protected virtual void OnInitialize() {}

    //各子オブジェクトの破壊時処理
    protected virtual void OnFinalize() {}

    void Awake()
    {
        Initialize( this as T );
    }
 
    void OnDestroy()
    {
        Destroyed( this as T );
    }
 
    void OnApplicationQuit()
    {
        Destroyed( this as T );
    }
}