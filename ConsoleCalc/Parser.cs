using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class Parser
    {
        private Lexem currentLexem;
 //       private MathOperation currentOperation;
        private string inputStr;
        private int pt;
        private readonly Operations operations;

        public Parser()
        {
            currentLexem = new Lexem();
            operations = new Operations();
        }

        public static string CutNumberFromString(string input, int startPt)
        {
            StringBuilder builder = new StringBuilder();
            if (char.IsDigit(input[startPt]))
            {
                bool dotFlag = false;
                while (startPt < input.Length)
                {
                    if (!char.IsDigit(input[startPt]))
                    {
                        if ((input[startPt] == ',') && !dotFlag)
                        {
                            dotFlag = true;
                        }
                        else break;
                    }
                    builder.Append(input[startPt]);
                    startPt++;
                }
                
            }
            return builder.ToString();
        }

        public double CountExpressionFromString(string input)
        {
            inputStr = input;
            pt = 0;
            this.currentLexem.type = LexemType.OPEN;
            GetNextLexem();
            double value = CountLowPriorityCmd();
            if (currentLexem.type != LexemType.END)
                throw new SyntaxException("Can't find the end of expression", pt);
            return value;
        }





        void GetNextLexem() 
        {
            if (pt >= inputStr.Length)
            {
                currentLexem.type = LexemType.END;
                return;
            }

            if (char.IsDigit(inputStr[pt]))
            {
                string numb = CutNumberFromString(inputStr, pt);
                currentLexem.value = double.Parse(numb);
                pt += numb.Length;
                currentLexem.type = LexemType.NUMB;
                return;
            }

            switch (inputStr[pt])
            {
                case '(':
                    currentLexem.type = LexemType.OPEN;
                    break;
                case ')':
                    currentLexem.type = LexemType.CLOSE;
                    break;
                default:
                    currentOperation = operations.GetOperation(inputStr[pt]);
                    if (currentOperation == null)
                    {
                        throw new SyntaxException("Unknown argument", pt);
                    } else if (currentLexem.type == LexemType.CMD)
                    {
                        throw new SyntaxException("Two args in a row", pt);
                    }
                    currentLexem.type = LexemType.CMD;
                    break;
            }
            pt++;
        }

        public double CountLowPriorityCmd()
        {
            double a = CountHightPriorityCmd();
            while ((currentLexem.type == LexemType.CMD) &&(currentOperation.GetPriority()==Priority.LOW))
            {
                GetNextLexem();
                a = currentOperation.Execute(a, CountHightPriorityCmd());
            }
            return a;
        }

        public double CountHightPriorityCmd()
        {
            double a = GetMultiplier();
            while ((currentLexem.type == LexemType.CMD) &&(currentOperation.GetPriority()==Priority.HIGHT))
            {
                GetNextLexem();
                a = currentOperation.Execute(a, CountHightPriorityCmd());
            }
            return a;
        }

        public double GetMultiplier()
        {
            double result = 0;
            switch (currentLexem.type)
            {
                case LexemType.NUMB:
                    result = currentLexem.value;
                    GetNextLexem();
                    break;
                case LexemType.OPEN:
                    GetNextLexem();
                    result = CountLowPriorityCmd();
                    if (currentLexem.type !=LexemType.CLOSE)
                    {
                        throw new SyntaxException("Incomplete expression, expected \")\"", pt);
                    }
                        else GetNextLexem();
                    break;
                case LexemType.CMD:
                    if(typeof(Subtraction).IsInstanceOfType(currentOperation))
                    {
                        GetNextLexem();
                        result = currentOperation.Execute(result, GetMultiplier());
                    }
                        else if(currentOperation.GetNumberOfArgs() == NumberOfArgs.ONE)
                    {
                        GetNextLexem();
                        result = currentOperation.Execute(GetMultiplier());
                    }
                        else throw new SyntaxException("Unexpected command", pt-1);
                    break;
                case LexemType.END:
                    throw new SyntaxException("Incomplete expression", pt - 1);
                default:
                    throw new SyntaxException("Unexpected command", pt-1);
            }
            return result;
        }
    }


    enum LexemType
    {
        OPEN,
        CLOSE,
        CMD,
        NUMB,
        END
    }

    internal class Lexem
    {
        internal char cmd;
        internal double value;
        internal LexemType type;
    }

}
