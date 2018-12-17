using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
//using EditDistanceProject;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liven;


namespace лаба4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); //просто не будем это трогать.
        }

        /// <summary>
        /// Список слов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        List<string> list = new List<string> ();//создали пустой лист

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileD = new OpenFileDialog();
            fileD.Filter = "текстовые файлы|*.txt"; //ограничиваемся текстовыми файлами

            if (fileD.ShowDialog() == DialogResult.OK) 
            {
                Stopwatch t = new Stopwatch(); //штучка для измерения времени
                t.Start(); //начали отсчет

                string text = File.ReadAllText(fileD.FileName); //чтение файла в виде строки
                                
                string[] textArray = text.Split(new char[] { ' ', '.', ',', '!', '?', '-', ';', ':', '/', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries); //разделяем строку на слова


                foreach (string i in textArray)
                {
                    string str = i.Trim(); //поудаляли пробелы перед и после слова
                    if (!list.Contains(str)) list.Add(str); //если в списке такого слова нет, добавляем его
                }

                t.Stop(); //остановили таймер

                this.label4.Text = (t.ElapsedTicks*0.0001).ToString(); //посчитали время
                this.label1.Text = list.Count.ToString();  //посчитали количество записанных слов

            }

            else
            {
                MessageBox.Show("Нужно выбрать файл");
            } 

        }




        private void buttonSeach_Click(object sender, EventArgs e)
        {
           string word = this.textBoxFind.Text.Trim(); //введенное слово для поиска

            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0) //если слово не пустое
            {
                int kol;
                if (!int.TryParse(this.TextBoxKol.Text.Trim(), out kol))
                {
                    MessageBox.Show("Необходимо указать максимальное расстояние");
                    return;
                }

                if (kol<1||kol>5)
                {
                    MessageBox.Show("Масксимальное расстояние должно быть больше 1 и меньше 5");
                    return;
                }

                string wordUp = word.ToUpper();//перевели в верхний регистр
                List<string> tempList = new List<string>();//пустой массив для найденных слов

                Stopwatch t = new Stopwatch();
                t.Start(); //начали отсчет



                foreach (string i in list)
                {
                    if (EditDistance.Distance(i.ToUpper(), wordUp) <=kol) // если растояние меньше указанного, то

                    {
                        tempList.Add(i); //записываем во временный список
                    }

                }



                t.Stop();
                this.labelTime2.Text = (t.ElapsedTicks * 0.0001).ToString();
                this.listBoxResults.Items.Clear(); //очистили список (чтоб не показывал предыдущие результаты)

                this.listBoxResults.BeginUpdate();

                foreach (string i in tempList)
                {
                    this.listBoxResults.Items.Add(i);
                }

                this.listBoxResults.EndUpdate();

            }

            else
            {
                MessageBox.Show("Нужно ввести слово для поиска");
            }


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
