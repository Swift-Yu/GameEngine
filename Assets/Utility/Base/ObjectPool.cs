using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IObjectReset
{
    void InitObject();
    void ResetObject();
}
public class ObjectPool<T> where T : class ,IObjectReset , new()
{
    List<T> objList = new List<T>();

    public T GetObject()
    {
        if ( objList.Count > 0 )
        {
            T obj = objList[0];
            objList.RemoveAt( 0 );
            obj.InitObject();
            return obj;
        }
        else
        {
            T obj = new T();
            obj.InitObject();
            return obj;
        }
    }
    public void ReturnObject(T obj)
    {
        obj.ResetObject();
        objList.Add( obj );
    }

}

// 对象池
public class ObjPool<T> where T : new()
{
    List<T> objList = new List<T>();

    public T Alloc()
    {
        if (objList.Count > 0)
        {
            T obj = objList[0];
            objList.RemoveAt(0);
            return obj;
        }
        else
        {
            T obj = new T();
            return obj;
        }
    }
    public void Free(T obj)
    {
        objList.Add(obj);
    }

    public void Clear()
    {
        //for(int i = 0; i < objList.Count; ++i)
        //{
        //    objList[i] = null;
        //}
        objList.Clear();
    }

}


