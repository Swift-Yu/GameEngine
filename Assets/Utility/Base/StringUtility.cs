using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Utility
{
    /// <summary>
    ///  字符串处理的相关方法
    /// </summary>
    public class StringUtility
    {
        // 异或编码
        static byte[] XORCODE = System.Text.Encoding.Default.GetBytes("P/:3FH*,:d@A2jks'j023~D)(#KL%@(*U*#UOD9:/?sfj023JDW(");

        /**
        @brief 分割字符串
        @param str 源字串
        @param strKey 分割字符
        @param lst 返回列表
        */
        static public void SplitString(ref string str, string strKey, out List<string> lst)
        {
            lst = new List<string>();
            string[] sl = str.Split(strKey.ToCharArray());
            for (int i = 0; i < sl.Length; ++i)
            {
                lst.Add(sl[i]);
            }
        }

        /// <summary>
        /// 从带有 ../../../的路径字符串 获取提取不带../../的路径串
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strOut"></param>
        static public void GetAbsolutePath(ref string strPath, out string strOut)
        {
            string strSrcPath = strPath;
            strSrcPath = strSrcPath.Replace("\\", "/");
            int pos = strSrcPath.IndexOf("/..");
            string strPrefix = "";
            string strSuffix = "";
            while (pos != -1)
            {
                strPrefix = strSrcPath.Substring(0, pos);
                strSuffix = strSrcPath.Substring(pos, strSrcPath.Length - pos);
                int lastpos = strPrefix.LastIndexOf("/");
                if (lastpos <= 0)
                {
                    break;
                }

                //string strPrefixNew = strPrefix.Substring(0, lastpos);
                strPrefix = strPrefix.Substring(0, lastpos);
                strSuffix = strSuffix.Substring(3, strSuffix.Length - 3);
                //strPrefix = strPrefixNew;
                strSrcPath = strPrefix + strSuffix;
                pos = strSrcPath.IndexOf("/..");
            }

            strOut = strSrcPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPathFileName">文件路径</param>
        /// <param name="strPath">路径名</param>
        /// <param name="strFileName">文件名</param>
        /// <param name="strFileNameNoExt">文件名不带扩展名</param>
        /// <param name="strExt">扩展名</param>
        static public void ParseFileName(ref string strPathFileName, out string strPath, out string strFileName, out string strFileNameNoExt, out string strExt)
        {
            strFileNameNoExt = "";
            strFileName = "";
            strPath = "";
            strExt = "";

            string strTemp = strPathFileName.Replace("\\", "/");
            int pos = strTemp.LastIndexOf(".");
            if (pos > 0)
            {
                strExt = strTemp.Substring(pos + 1, strTemp.Length - pos - 1);
                strTemp = strTemp.Substring(0, pos);

                pos = strTemp.LastIndexOf("/");
                if (pos >= 0)
                {
                    strPath = strTemp.Substring(0, pos);
                    strFileNameNoExt = strTemp.Substring(pos + 1);
                }
                else
                {
                    strFileNameNoExt = strTemp;
                }

                strFileName = strFileNameNoExt + "." + strExt;
            }
            else if (pos == 0) // 这种情况，还要处理下 相对路径的写法 .sss ./slf/sd ./
            {
                if (strTemp.Length > pos + 1)
                {
                    if (strTemp[pos + 1] == '/')
                    {
                        strPath = strPathFileName;
                    }
                    else
                    {
                        strExt = strTemp.Substring(pos + 1, strTemp.Length - pos - 1);
                    }
                }
            }
            else if (pos == -1)
            {
                strPath = strPathFileName;
            }
        }

        static public Vector4 ParseVector4(string strVector4)
        {
            float[] e = new float[4];
            e[0] = e[1] = e[2] = e[3] = 0;
            if (strVector4 == null || strVector4 == "")
            {
                return new Vector4(e[0], e[1], e[2], e[3]);
            }

            string[] strElement = strVector4.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 3)
            {
                return new Vector4(e[0], e[1], e[2], e[3]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!float.TryParse(strElement[i], out e[i]))
                {
                    return new Vector4(0, 0, 0, 0);
                }
            }

            return new Vector4(e[0], e[1], e[2], e[3]);
        }

        static public Rect ParseRect(string strRect)
        {
            float[] e = new float[4];
            e[0] = e[1] = e[2] = e[3] = 0;
            if (strRect == null || strRect == "")
            {
                return new Rect(e[0], e[1], e[2], e[3]);
            }

            string[] strElement = strRect.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 3)
            {
                return new Rect(e[0], e[1], e[2], e[3]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!float.TryParse(strElement[i], out e[i]))
                {
                    return new Rect(0, 0, 0, 0);
                }
            }

            return new Rect(e[0], e[1], e[2], e[3]);
        }

        static public Vector3 ParseVector3(string strVector3)
        {
            float[] e = new float[3];
            e[0] = e[1] = e[2] = 0;
            if (strVector3 == null || strVector3 == "")
            {
                return new Vector3(e[0], e[1], e[2]);
            }

            string[] strElement = strVector3.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 3)
            {
                return new Vector3(e[0], e[1], e[2]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!float.TryParse(strElement[i], out e[i]))
                {
                    return new Vector3(0, 0, 0);
                }
            }

            return new Vector3(e[0], e[1], e[2]);
        }

        static public Vector2 ParseVector2(string strVector2)
        {
            float[] e = new float[2];
            e[0] = e[1] = 0;
            if (strVector2 == null || strVector2 == "")
            {
                return new Vector2(e[0], e[1]);
            }

            string[] strElement = strVector2.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 2)
            {
                return new Vector2(e[0], e[1]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!float.TryParse(strElement[i], out e[i]))
                {
                    return new Vector2(0, 0);
                }
            }

            return new Vector2(e[0], e[1]);
        }

        static public Quaternion ParseQuaternion(string strQuaternion)
        {
            float[] e = new float[4];
            e[0] = e[1] = e[2] = 0;
            e[3] = 1;
            if (strQuaternion == null || strQuaternion == "")
            {
                return new Quaternion(e[0], e[1], e[2], e[3]);
            }

            string[] strElement = strQuaternion.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 4)
            {
                return new Quaternion(e[0], e[1], e[2], e[3]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!float.TryParse(strElement[i], out e[i]))
                {
                    return new Quaternion(0, 0, 0, 1);
                }
            }

            return new Quaternion(e[0], e[1], e[2], e[3]);
        }

        static public Color ParseColor(string strColor)
        {
            float[] e = new float[4];
            e[0] = e[1] = e[2] = 0;
            e[3] = 1;
            if (strColor == null || strColor == "")
            {
                return new Color(e[0], e[1], e[2], e[3]);
            }

            string[] strElement = strColor.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 4)
            {
                return new Color(e[0], e[1], e[2], e[3]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!float.TryParse(strElement[i], out e[i]))
                {
                    return new Color(0, 0, 0, 1);
                }
            }

            return new Color(e[0], e[1], e[2], e[3]);
        }

        static public Color32 ParseColor32(string strColor)
        {
            byte[] e = new byte[4];
            e[0] = e[1] = e[2] = 0;
            e[3] = 1;
            if (strColor == null || strColor == "")
            {
                return new Color32(e[0], e[1], e[2], e[3]);
            }

            string[] strElement = strColor.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (strElement.Length != 4)
            {
                return new Color32(e[0], e[1], e[2], e[3]);
            }

            for (int i = 0; i < strElement.Length; ++i)
            {
                if (!byte.TryParse(strElement[i], out e[i]))
                {
                    return new Color32(0, 0, 0, 1);
                }
            }

            return new Color32(e[0], e[1], e[2], e[3]);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool[] ToBoolList(string strOperation)
        {
            if (string.IsNullOrEmpty(strOperation))
            {
                return null;
            }

            string[] strItem = strOperation.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strItem.Length <= 0)
            {
                return null;
            }

            bool[] opList = new bool[strItem.Length];
            for (int i = 0; i < strItem.Length; ++i)
            {
                if (strItem[i] == "true")
                {
                    opList[i] = true;
                }
                else
                {
                    opList[i] = false;
                }
            }

            return opList;
        }

        public static float[] ToFloatList(string strOperation)
        {
            if (string.IsNullOrEmpty(strOperation))
            {
                return null;
            }

            string[] strItem = strOperation.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strItem.Length <= 0)
            {
                return null;
            }

            float[] opList = new float[strItem.Length];
            for (int i = 0; i < strItem.Length; ++i)
            {
                opList[i] = float.Parse(strItem[i]);
            }

            return opList;
        }

        public static int[] ToIntList(string strOperation)
        {
            if (string.IsNullOrEmpty(strOperation))
            {
                return null;
            }

            string[] strItem = strOperation.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strItem.Length <= 0)
            {
                return null;
            }

            int[] opList = new int[strItem.Length];
            for (int i = 0; i < strItem.Length; ++i)
            {
                opList[i] = int.Parse(strItem[i]);
            }

            return opList;
        }
        //-------------------------------------------------------------------------------------------------------
        // tostring
        // ToString 只支持基础类型 数值类型(byte,short,int,float,double)和bool类型 其它类型不保证有效
        public static string ToString<T>(T[] operation)
        {
            StringBuilder strResult = new StringBuilder();
            if (operation == null)
            {
                return strResult.ToString();
            }

            for (int i = 0; i < operation.Length; ++i)
            {
                strResult.Append(operation[i].ToString());
                if (i != operation.Length - 1)
                {
                    strResult.Append(",");
                }
            }

            return strResult.ToString();
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // tostring
        static public string ToString(Vector3 vec)
        {
            string strVector3 = vec.x.ToString();
            strVector3 += ",";
            strVector3 += vec.y.ToString();
            strVector3 += ",";
            strVector3 += vec.z.ToString();

            return strVector3;
        }

        static public string ToString(Vector2 vec)
        {
            string strVector2 = vec.x.ToString();
            strVector2 += ",";
            strVector2 += vec.y.ToString();

            return strVector2;
        }

        static public string ToString(Vector4 vec)
        {
            string strVector4 = vec.x.ToString();
            strVector4 += ",";
            strVector4 += vec.y.ToString();
            strVector4 += ",";
            strVector4 += vec.z.ToString();
            strVector4 += ",";
            strVector4 += vec.w.ToString();

            return strVector4;
        }

        static public string ToString(Rect rect)
        {
            string strRect = rect.x.ToString();
            strRect += ",";
            strRect += rect.y.ToString();
            strRect += ",";
            strRect += rect.width.ToString();
            strRect += ",";
            strRect += rect.height.ToString();

            return strRect;
        }

        static public string ToString(Quaternion rot)
        {
            string strQuan = rot.x.ToString();
            strQuan += ",";
            strQuan += rot.y.ToString();
            strQuan += ",";
            strQuan += rot.z.ToString();
            strQuan += ",";
            strQuan += rot.w.ToString();

            return strQuan;
        }

        static public string ToString(Color color)
        {
            string strColor = color.r.ToString();
            strColor += ",";
            strColor += color.g.ToString();
            strColor += ",";
            strColor += color.b.ToString();
            strColor += ",";
            strColor += color.a.ToString();

            return strColor;
        }

        static public string ToString(Color32 color)
        {
            string strColor = color.r.ToString();
            strColor += ",";
            strColor += color.g.ToString();
            strColor += ",";
            strColor += color.b.ToString();
            strColor += ",";
            strColor += color.a.ToString();

            return strColor;
        }

        // buffxor 异或编码
        static public void XorBuff(byte[] buff)
        {
            if (buff == null)
            {
                return;
            }

            for (int i = 0; i < buff.Length; ++i)
            {
                int code = i % 32;
                buff[i] ^= XORCODE[code];
            }
        }
    }
}