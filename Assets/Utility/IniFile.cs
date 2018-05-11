using System;
using System.Collections.Generic;
using System.IO;

namespace Utility
{
    // ini文件解析 读/写 只支持独立行注释 // 不支持行尾注释 
    public class IniFile
    {
        class IniKey
        {
            private string m_strKeyNodeName = "";
            public int m_nLine;
            public List<string> m_strComment = new List<string>();

            class IniValue
            {
                public IniValue(string v, int line) { strValue = v; nLine = line; }
                public string strValue;
                public int nLine;  // 在文件中的行号
                public List<string> m_strComment = null;
            }

            // 数据
            private Dictionary<string, IniValue> m_values = new Dictionary<string, IniValue>();

            public IniKey(string name, int line)
            {
                m_strKeyNodeName = name;
                m_nLine = line;
            }

            public int GetInt(string strName, int nDefault)
            {
                IniValue v;
                if (m_values.TryGetValue(strName, out v))
                {
                    return int.Parse(v.strValue);
                }
                return nDefault;
            }

            public string GetString(string strName, string strDefault)
            {
                IniValue v;
                if (m_values.TryGetValue(strName, out v))
                {
                    return v.strValue;
                }

                return strDefault;
            }

            public void WriteInt(string strName, int nValue, int nLine, List<string> Comments)
            {
                IniValue v;
                if (m_values.TryGetValue(strName, out v))
                {
                    v.strValue = nValue.ToString();
                    if (Comments != null)
                    {
                        v.m_strComment = new List<string>(Comments.ToArray());
                    }
                    m_values[strName] = v;
                }
                else
                {
                    v = new IniValue(nValue.ToString(), nLine);
                    if (Comments != null)
                    {
                        v.m_strComment = new List<string>(Comments.ToArray());
                    }
                    m_values[strName] = v;
                }
            }

            public void WriteString(string strName, string strValue, int nLine, List<string> Comments)
            {
                IniValue v;
                if (m_values.TryGetValue(strName, out v))
                {
                    v.strValue = strValue;
                    if (Comments != null)
                    {
                        v.m_strComment = new List<string>(Comments.ToArray());
                    }
                    m_values[strName] = v;
                }
                else
                {
                    v = new IniValue(strValue, nLine);
                    if (Comments != null)
                    {
                        v.m_strComment = new List<string>(Comments.ToArray());
                    }
                    m_values[strName] = v;
                }
            }

            public void Save(StreamWriter file)
            {
                if (m_strKeyNodeName != "")
                {
                    if (m_strComment != null)
                    {
                        for (int i = 0; i < m_strComment.Count; ++i)
                        {
                            file.WriteLine(m_strComment[i]);
                        }
                    }
                    file.WriteLine("[" + m_strKeyNodeName + "]");
                }

                foreach (KeyValuePair<string, IniValue> v in m_values)
                {
                    if (v.Value.m_strComment != null)
                    {
                        for (int i = 0; i < v.Value.m_strComment.Count; ++i)
                        {
                            file.WriteLine(v.Value.m_strComment[i]);
                        }
                    }
                    string strLines = v.Key + "=" + v.Value.strValue;
                    file.WriteLine(strLines);
                }
            }
        }

        // 数据存储
        private Dictionary<string, IniKey> m_Data = new Dictionary<string, IniKey>();
        // 文件名称
        private string m_strFileName = "";

        // 所有字符串 包括注释 只支持独立行注释 不支持行尾注释 
        private List<string> m_allLines = new List<string>();    // 所有数据

        // 注释行缓存
        private List<string> m_Coment = new List<string>();

        ////////////////////////
        /// 解析过程中数据
        private string m_strKeyNodeName = "";

        // 只读
        private bool m_bReadOnly = false;

        public bool Open(string strIniFile)
        {
            m_strFileName = strIniFile;

            string strIniCotent;
            if (FileUtils.Instance().GetTextFileBuff(strIniFile, out strIniCotent) == 0)
            {
                return false;
            }

            m_Coment.Clear();

            string[] lines = strIniCotent.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int nLineIndex = 1;
            for (int i = 0; i < lines.Length; ++i)
            {
                m_allLines.Add(lines[i]);
                ParseLine(lines[i], nLineIndex);
                nLineIndex++;
            }

            return true;
        }

        // 保存 仅限windows下使用
        public bool Save()
        {
            if (m_bReadOnly)
            {
                return true;
            }

            string strIniFile = Utility.FileUtils.Instance().FullPathFileName(ref m_strFileName, Utility.FileUtils.UnityPathType.UnityPath_CustomPath);

            FileStream fs = null;
            try
            {
                fs = new FileStream(strIniFile, FileMode.Create, FileAccess.Write);
            }
            catch (System.Exception ex)
            {
                Log.Error("打开文件{0}失败:{1}!", strIniFile, ex.ToString());
            }
            finally
            {
                if (fs != null)
                {
                    StreamWriter sw = new StreamWriter(fs);

                    foreach (KeyValuePair<string, IniKey> v in m_Data)
                    {
                        v.Value.Save(sw);
                    }

                    sw.Close();
                    fs.Close();
                }
            }

            return true;
        }

        public int GetInt(string strKey, string strName, int nDefault)
        {
            IniKey key;
            if (m_Data.TryGetValue(strKey, out key))
            {
                return key.GetInt(strName, nDefault);
            }

            return nDefault;
        }

        public string GetString(string strKey, string strName, string strDefault)
        {
            IniKey key;
            if (m_Data.TryGetValue(strKey, out key))
            {
                return key.GetString(strName, strDefault);
            }

            return strDefault;
        }

        public void WriteInt(string strKey, string strName, int nValue)
        {
            if (!m_Data.ContainsKey(strKey))
            {
                m_Data[strKey] = new IniKey(strKey, 0);
            }

            IniKey key;
            if (m_Data.TryGetValue(strKey, out key))
            {
                key.WriteInt(strName, nValue, 0, null);
            }
        }

        public void WriteString(string strKey, string strName, string strValue)
        {
            if (!m_Data.ContainsKey(strKey))
            {
                m_Data[strKey] = new IniKey(strKey, 0);
            }

            IniKey key;
            if (m_Data.TryGetValue(strKey, out key))
            {
                key.WriteString(strName, strValue, 0, null);
            }
        }

        ///////////////////////////////////////////////////
        private void ParseLine(string strLine, int nLine)
        {
            strLine = strLine.Trim();

            int pos = strLine.IndexOf("//");
            if (pos == 0) // 注释行
            {
                m_Coment.Add(strLine);
                return;
            }

            pos = strLine.IndexOf("[");
            if (pos == 0)  // key
            {
                m_strKeyNodeName = "";
                int endPos = strLine.LastIndexOf("]");
                if (endPos == strLine.Length - 1)
                {
                    string strKey = strLine.Substring(1, strLine.Length - 1);
                    strKey = strKey.Substring(0, strKey.Length - 1);

                    IniKey key = new IniKey(strKey, nLine);
                    key.m_strComment = new List<string>(m_Coment.ToArray());
                    m_Coment.Clear();

                    m_Data[strKey] = key;
                    m_strKeyNodeName = strKey;
                }
            }
            else
            {
                int eqPos = strLine.IndexOf("=");
                if (eqPos == -1)
                {
                    return;
                }

                string strName, strValue;
                strName = strLine.Substring(0, eqPos);
                strName = strName.Trim();
                strValue = strLine.Substring(eqPos + 1, strLine.Length - eqPos - 1);
                strValue = strValue.Trim();

                int nValue = 0;
                if (int.TryParse(strValue, out nValue))
                {
                    IniKey key;
                    if (m_Data.TryGetValue(m_strKeyNodeName, out key))
                    {
                        key.WriteInt(strName, nValue, nLine, m_Coment);
                        m_Coment.Clear();
                    }
                }
                else
                {
                    IniKey key;
                    if (m_Data.TryGetValue(m_strKeyNodeName, out key))
                    {
                        key.WriteString(strName, strValue, nLine, m_Coment);
                        m_Coment.Clear();
                    }
                }
            }
        }
    }
}
