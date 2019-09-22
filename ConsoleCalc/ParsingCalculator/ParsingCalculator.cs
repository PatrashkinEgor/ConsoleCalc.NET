using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class ParsingCalculator
    {
        private Lexem currentLexem;
        private MathOperation currentOperation;
        private string inputStr;
        private int pt;
        private readonly Operations operations;

        public ParsingCalculator()
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
            currentLexem.type = LexemType.OPEN;
            GetNextLexem();
            double value = CountLowPriorityCmd();
            if (currentLexem.type != LexemType.END)
                throw new SyntaxException("Can't find the end of expression", pt);
            return value;
        }





        private void GetNextLexem()
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
                    GetNextCmdLexem();
                    break;
            }
            pt++;
        }

        private void GetNextCmdLexem()
        {
            currentOperation = operations.GetOperation(inputStr[pt]);
            if (currentOperation == null)
            {
                throw new SyntaxException("Unknown argument", pt);
            }
            else if (currentLexem.type == LexemType.CMD)
            {
                throw new SyntaxException("Two args in a row", pt);
            }
            currentLexem.type = LexemType.CMD;
        }

        private double CountLowPriorityCmd()
        {
            double a = CountHightPriorityCmd();
            while (CurrentOperationActiveAndPriorityIs(Priority.LOW))
            {
                if (CurrentOperationHaveOneArg())
                    break;
                GetNextLexem();
                a = currentOperation.Execute(a, CountHightPriorityCmd());
            }
            return a;
        }

        private double CountHightPriorityCmd()
        {
            double a = GetMultiplier();
            while (CurrentOperationActiveAndPriorityIs(Priority.HIGHT))
            {
                if (CurrentOperationHaveOneArg())
                    break;
                GetNextLexem();
                a = currentOperation.Execute(a, CountHightPriorityCmd());
            }
            return a;
        }


        private double GetMultiplier()
        {
            double result;
            switch (currentLexem.type)
            {
                case LexemType.NUMB:
                    result = currentLexem.value;
                    GetNextLexem();
                    break;
                case LexemType.OPEN:
                    result = CalcBraketsContents();
                    break;
                case LexemType.CMD:
                    result = CalcUnaryOperations();
                    break;
                case LexemType.END:
                    throw new SyntaxException("Incomplete expression", pt - 1);
                default:
                    throw new SyntaxException("Unexpected command", pt - 1);
            }
            return result;
        }

        private double CalcBraketsContents()
        {
            double result = 0;
            GetNextLexem();
            result = CountLowPriorityCmd();
            if (currentLexem.type != LexemType.CLOSE)
            {
                throw new SyntaxException("Incomplete expression, expected \")\"", pt);
            }
            else GetNextLexem();
            return result;
        }

        private double CalcUnaryOperations()
        {
            double result = 0;
            if (CurrentOperationIs(typeof(Subtraction)))
            {
                GetNextLexem();
                result = currentOperation.Execute(result, GetMultiplier());
            }
            else if (CurrentOperationHaveOneArg())
            {
                GetNextLexem();
                result = currentOperation.Execute(GetMultiplier());
            }
            else throw new SyntaxException("Unexpected command", pt - 1);
            return result;
        }



        private bool CurrentOperationActiveAndPriorityIs(Priority priority)
        {
            return ((currentLexem.type == LexemType.CMD) && (currentOperation.GetPriority() == priority));
        }

        private bool CurrentOperationIs<T>(T requstedType) where T : Type
        {
            return requstedType.IsInstanceOfType(currentOperation);
        }

        private bool CurrentOperationHaveOneArg()
        {
            return currentOperation.GetNumberOfArgs() == NumberOfArgs.ONE;
        }


    }

    internal enum LexemType
    {
        OPEN,
        CLOSE,
        CMD,
        NUMB,
        END
    }

    internal class Lexem
    {
        internal double value;
        internal LexemType type;
    }

}
