using System;
using System.Text;

namespace DSELib.Language
{
    public enum Language_Enum
    {
        Language_DE = 0,
        Language_EN = 1
    }

    public class Dictionary
    {
        private System.Collections.Generic.Dictionary<string, string> m_Dict = new System.Collections.Generic.Dictionary<string, string>();

        public void Add(Language_Enum loLanguage, string lsKey, string lsText)
        {
            m_Dict.Add(loLanguage.ToString() + '_' + lsKey, lsText);
        }
        public string Get(Language_Enum loLanguage, string lsKey)
        {
            if (m_Dict.ContainsKey(loLanguage.ToString() + '_' + lsKey))
            {
                return m_Dict[loLanguage.ToString() + '_' + lsKey];
            }
            else
            {
                return "Key: " + loLanguage.ToString() + '_' + lsKey;
            }
        }
        public void Clear()
        {
            m_Dict.Clear();
        }

        private System.Collections.Generic.List<string> GetLanguageKeyList(Language_Enum loLanguage)
        {
            System.Collections.Generic.List<string> loList = new System.Collections.Generic.List<string>();

            foreach(string lsKey in m_Dict.Keys)
            {
                if (lsKey.IndexOf(loLanguage.ToString() + "_") == 0)
                    loList.Add(lsKey);
            }
            return loList;
        }

        public void Clear(Language_Enum loLanguage)
        {
            System.Collections.Generic.List<string> loList = GetLanguageKeyList(loLanguage);

            foreach (string lsKey in loList)
            {
                m_Dict.Remove(lsKey);
            }
            loList.Clear();
            loList = null;
        }
        public void Remove(Language_Enum loLanguage, string lsKey)
        {
            if (m_Dict.ContainsKey(loLanguage.ToString() + '_' + lsKey))
                m_Dict.Remove(loLanguage.ToString() + '_' + lsKey);
        }
        public int Count()
        {
            return m_Dict.Count;
        }
        public int Count(Language_Enum loLanguage)
        {
            System.Collections.Generic.List<string> loList = GetLanguageKeyList(loLanguage);

            int liCount = loList.Count;
            loList.Clear();
            loList = null;
            return liCount;
        }
    }

    public class Language
    {
        private static Dictionary m_Msg = new Dictionary();
        private static Dictionary m_Error = new Dictionary();

        public static Dictionary Message
        {
            get
            {
                return m_Msg;
            }
        }
        public static Dictionary Error
        {
            get
            {
                return m_Error;
            }
        }
    }
}
