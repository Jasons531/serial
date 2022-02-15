using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTest
{
    class FiFo
    {
        private byte[] g_Buff = null;
        private int g_MaxLen;
        private int g_DataLen;
        private int g_Head;
        private int g_Tail;

        public FiFo(int MaxLen)
        {
            Init(MaxLen);
        }

        public void Init(int MaxLen)
        {
            g_Buff = new byte[MaxLen];
            g_MaxLen = MaxLen;
            g_DataLen = 0;
            g_Head = 0;
            g_Tail = 0;
        }

        public void Clear()
        {
            g_DataLen = 0;
            g_Head = 0;
            g_Tail = 0;
        }

        public bool Push(byte[] Data)
        {
            if (GetFreeLen() < Data.Length)
            {
                return false;
            }

            for (int i = 0; i < Data.Length; i++)
            {
                g_Buff[g_Tail] = Data[i];
                g_Tail++;
                g_Tail = g_Tail % g_MaxLen;
            }
            g_DataLen += Data.Length;
            return true;
        }

        public byte[] Pop(int Len)
        {
            if (g_DataLen < Len)
            {
                return null;
            }
            else
            {
                byte[] ret = new byte[Len];
                for (int i = 0; i < Len; i++)
                {
                    ret[i] = g_Buff[g_Head];
                    g_Head++;
                    g_Head = g_Head % g_MaxLen;
                }
                g_DataLen -= Len;
                return ret;
            }
        }

        public string PopLine()
        {
            for (int i = g_Head; i != g_Tail; i++)
            {
                i = i % g_MaxLen;
                if (g_Buff[i] == '\n')
                {
                    int Len = 0;
                    /* 注意是大于等于 */
                    if (i >= g_Head)
                    {
                        Len = i - g_Head + 1;
                    }
                    else
                    {
                        Len = i + g_MaxLen - g_Head + 1;
                    }
                    byte[] ret = Pop(Len);
                    if (ret != null)
                    {
                        return Encoding.GetEncoding("GB2312").GetString(ret).Replace("\r", "").Replace("\n", "");
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        public byte[] PopAll()
        {
            return Pop(g_DataLen);
        }

        public int GetFreeLen()
        {
            return g_MaxLen - g_DataLen;
        }
    }
}
