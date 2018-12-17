using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liven
{
    public class EditDistance //посчитать расстояние
    {
        public static int Distance(string str1, string str2)
        {
            if ((str1==null)||(str2==null)) return -1; //если обе строки пустые, конец

            int len1 = str1.Length;
            int len2 = str2.Length;

            if ((len1 == 0) && (len2 == 0)) return 0; //если есть нулевые строки, все просто
            if (len1 == 0) return len2;
            if (len2 == 0) return len1;

            string strUp1 = str1.ToUpper();
            string strUp2 = str2.ToUpper(); //приводим к верхнему регистру

            int[,] matrix = new int[len1 + 1, len2 + 1];//создали матрицу

            for (int i = 0; i < len1; i++) matrix[i, 0] = i;
            for (int j = 0; j < len2; j++) matrix[0, j] = j; //пронумеровали столбики и строчки


            for (int i=1; i<len1+1; i++)
            {
                for (int j=1; j<len2+1; j++)
                {
                    int sEqual = ((strUp1.Substring(i - 1, 1) == strUp2.Substring(j - 1, 1)) ? 0 : 1);
                    //если соответствующие символы равны, присваиваем 0, иначе 1
                    int oI = matrix[i, j - 1] + 1; //insert
                    int oD = matrix[i - 1, j] + 1; //delete
                    int oR = matrix[i - 1, j - 1] + sEqual; //replace

                    matrix[i, j] = Math.Min(Math.Min(oI, oD), oR); //минимум

                    if ((i>1)&&(j>1)&&  //если элемент не первый (не номер столбика)
                        (strUp1.Substring(i-1,1) == strUp2.Substring(j-2, 1))&&  //если при перестановке символы равны
                        (strUp1.Substring(i - 2, 1) == strUp2.Substring(j - 1, 1)))
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + sEqual);
                    }
                }
            }
            return matrix[len1, len2]; //вернули нижний правый
        }

    }
}
