using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LengthConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            HuaZeClass huaze = new HuaZeClass();
            huaze.ReadInputFile(@"C:\Users\v0cn213\Desktop\input.txt");
            huaze.GenerateAnswers(@"C:\Users\v0cn213\Desktop\output.txt");
        }
    }

    //My GF Named this class -_-!
    public class HuaZeClass
    {

        public HuaZeClass()
        {
            m_Results.AppendLine("21488256@qq.com");
            m_Results.AppendLine();
        }

        Dictionary<string, double> m_Formulars = new Dictionary<string, double>();
        IList<string> m_Questions = new List<string>();
        //IList<double> m_Results = new List<double>();

        StringBuilder m_Results = new StringBuilder();

        public void ReadInputFile(string path)
        {
            using (StreamReader inputReader = new StreamReader(path))
            {
                while (!inputReader.EndOfStream)
                {
                    string line = inputReader.ReadLine();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("="))
                        GenerateFormulars(line);
                    else
                        Calculate(line.Trim());
                }
            }
        }

        public void Calculate(string question)
        {
            double result = 0.00;
            string[] expression = question.Split(' ');
            for (int i = 1; i < expression.Length; i += 3)
            {
                foreach (KeyValuePair<string, double> formular in m_Formulars)
                {
                    //无语啊，不写死难到要弄个词典？？
                    if (expression[i].Equals("feet"))
                    {
                        expression[i] = "foot";
                    }


                    if (expression[i].Contains(formular.Key))
                    {
                        if (i - 3 > 0)
                        {
                            switch (expression[i - 2])
                            {
                                case "+":
                                    result += formular.Value * double.Parse(expression[i - 1]);
                                    break;
                                case "-":
                                    result -= formular.Value * double.Parse(expression[i - 1]);
                                    break;
                            }
                            break;
                        }
                        result += formular.Value * double.Parse(expression[i - 1]);
                        break;
                    }
                }
            }
            m_Results.AppendLine(Math.Round(result, 2).ToString() + " m");
        }


        public void GenerateAnswers(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(m_Results.ToString());
            }
        }
        private void GenerateFormulars(string line)
        {
            if (!line.Contains("="))
                throw new ArgumentException("input value is not a valid formular");
            string[] values = line.Split('=');
            m_Formulars.Add(values[0].Replace("1", string.Empty).Trim(), double.Parse(values[1].Replace("m", string.Empty).Trim()));
        }
    }


}





