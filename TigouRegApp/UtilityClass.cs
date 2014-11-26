using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TigouRegApp
{
    public class UtilityClass
    {
        public int TypeIndex = 0;


        [DllImport("Sunday.dll")]
        public static extern int LoadLibFromFile(string LibFilePath, string nSecret);



        [DllImport("Sunday.dll")]
        public static extern bool GetCodeFromFile(int LibFileIndex, string FilePath, StringBuilder Code);//从文件识别  参数一，字库索引；参数二，图片路径；参数三，返回的识别结果；

        [DllImport("Sunday.dll")]
        public static extern bool GetCodeFromBuffer(int LibFileIndex, byte[] FileBuffer, int ImgBufLen, StringBuilder Code);//从流识别 参数一，字库索引；参数二，图片数据；参数三 数据长度；参数四，返回的识别结果；

        public static string GenPinCode()
        {

            System.Random rnd;
            string[] _crabodistrict = new string[] { "350201", "350202", "350203", "350204", "350205", "350206", "350211", "350205", "350213" };


            rnd = new Random(System.DateTime.Now.Millisecond);

            //PIN = District + Year(50-92) + Month(01-12) + Date(01-30) + Seq(001-600)
            string _pinCode = string.Format("{0}19{1}{2:00}{3:00}{4:000}", _crabodistrict[rnd.Next(0, 8)], rnd.Next(50, 92), rnd.Next(1, 12), rnd.Next(1, 30), rnd.Next(1, 600));
            #region Verify
            char[] _chrPinCode = _pinCode.ToCharArray();
            //校验码字符值
            char[] _chrVerify = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            //i----表示号码字符从由至左包括校验码在内的位置序号；
            //ai----表示第i位置上的号码字符值；
            //Wi----示第i位置上的加权因子，其数值依据公式intWeight=2（n-1）(mod 11)计算得出。
            int[] _intWeight = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            int _craboWeight = 0;
            for (int i = 0; i < 17; i++)//从1 到 17 位,18为要生成的验证码
            {
                _craboWeight = _craboWeight + Convert.ToUInt16(_chrPinCode[i].ToString()) * _intWeight[i];
            }
            _craboWeight = _craboWeight % 11;
            _pinCode += _chrVerify[_craboWeight];
            #endregion
            return _pinCode;
        }
    }

    public class RandomPassword
    {
        /// <summary>
        /// 生成各类随机密码,包括纯字母,纯数字,带特殊字符等,除非字母大写密码类型,其余方式都将采用小写密码
        /// </summary>
        /// <param name="pwdType">密码类型 大写为"UPPER",小写为"LOWER",数字为"NUMBER",字母与数字为"NUMCHAR",数字字母字符都包括为"ALL" </param>
        /// <param name="length">密码长度,最小为6位</param>
        /// <returns></returns>
        public static string MakeRandomPassword(string pwdType, int length)
        {
            //定义密码字符的范围,小写,大写字母,数字以及特殊字符
            string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numnberChars = "0123456789";
            string specialCahrs = "~!@#$%^&*()_+|-=,./[]{}:;':";   //"\" 转义字符不添加 "号不添加

            string tmpStr = "";

            int iRandNum;
            Random rnd = new Random();

            length = (length < 6) ? 6 : length; //密码长度必须大于6,否则自动取6

            // LOWER为小写 UPPER为大写 NUMBER为数字 NUMCHAR为数字和字母 ALL全部包含 五种方式
            //只有当选择UPPER才会有大写字母产生,其余方式中的字母都为小写,避免有些时候字母不区分大小写
            if (pwdType == "LOWER")
            {
                for (int i = 0; i < length; i++)
                {
                    iRandNum = rnd.Next(lowerChars.Length);
                    tmpStr += lowerChars[iRandNum];
                }
            }
            else if (pwdType == "UPPER")
            {
                for (int i = 0; i < length; i++)
                {
                    iRandNum = rnd.Next(upperChars.Length);
                    tmpStr += upperChars[iRandNum];
                }
            }
            else if (pwdType == "NUMBER")
            {
                for (int i = 0; i < length; i++)
                {
                    iRandNum = rnd.Next(numnberChars.Length);
                    tmpStr += numnberChars[iRandNum];
                }
            }
            else if (pwdType == "NUMCHAR")
            {
                int numLength = rnd.Next(length);
                //去掉随机数为0的情况
                if (numLength == 0)
                {
                    numLength = 1;
                }
                int charLength = length - numLength;
                string rndStr = "";
                for (int i = 0; i < numLength; i++)
                {
                    iRandNum = rnd.Next(numnberChars.Length);
                    tmpStr += numnberChars[iRandNum];
                }
                for (int i = 0; i < charLength; i++)
                {
                    iRandNum = rnd.Next(lowerChars.Length);
                    tmpStr += lowerChars[iRandNum];
                }
                //将取得的字符串随机打乱
                for (int i = 0; i < length; i++)
                {
                    int n = rnd.Next(tmpStr.Length);
                    //去除n随机为0的情况
                    //n = (n == 0) ? 1 : n;
                    rndStr += tmpStr[n];
                    tmpStr = tmpStr.Remove(n, 1);
                }
                tmpStr = rndStr;
            }
            else if (pwdType == "ALL")
            {
                int numLength = rnd.Next(length - 1);
                //去掉随机数为0的情况
                if (numLength == 0)
                {
                    numLength = 1;
                }
                int charLength = rnd.Next(length - numLength);
                if (charLength == 0)
                {
                    charLength = 1;
                }
                int specCharLength = length - numLength - charLength;
                string rndStr = "";
                for (int i = 0; i < numLength; i++)
                {
                    iRandNum = rnd.Next(numnberChars.Length);
                    tmpStr += numnberChars[iRandNum];
                }
                for (int i = 0; i < charLength; i++)
                {
                    iRandNum = rnd.Next(lowerChars.Length);
                    tmpStr += lowerChars[iRandNum];
                }
                for (int i = 0; i < specCharLength; i++)
                {
                    iRandNum = rnd.Next(specialCahrs.Length);
                    tmpStr += specialCahrs[iRandNum];
                }
                //将取得的字符串随机打乱
                for (int i = 0; i < length; i++)
                {
                    int n = rnd.Next(tmpStr.Length);
                    //去除n随机为0的情况
                    //n = (n == 0) ? 1 : n;
                    rndStr += tmpStr[n];
                    tmpStr = tmpStr.Remove(n, 1);
                }
                tmpStr = rndStr;
            }
            //默认将返回数字类型的密码
            else
            {
                for (int i = 0; i < length; i++)
                {
                    iRandNum = rnd.Next(numnberChars.Length);
                    tmpStr += numnberChars[iRandNum];
                }
            }
            return tmpStr;
        }
    }
}
