using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    class Parser
    {
        private Lexem currentLexem;
        private MathOperation currentOperation;
        private String inputStr;
        private int pt;
        private readonly Operations operations;

        public Parser()
        {
            currentLexem = new Lexem();
            operations = new Operations();
        }



        public double countExpressionFromString(String input)
        {
            inputStr = input;
            pt = 0;
            this.currentLexem.type = LexemType.OPEN;
            nextLexem();
            double value = expression();
            if (currentLexem.type != LexemType.END)
                throw new SyntaxException("Can't find the end of expression", pt);
            return value;
        }

        void nextLexem() 
        {
            if (pt >= inputStr.Length)
            {
                currentLexem.type = LexemType.END;
                return;
            }
            if (Char.IsDigit(inputStr[pt]))
            {
                StringBuilder builder = new StringBuilder();
                while (pt<inputStr.Length)
                {
                    if ((!Char.IsDigit(inputStr[pt])) && inputStr[pt] != '.')
                        break;
                    builder.Append(inputStr[pt]);
                    pt++;
                }
                currentLexem.type = LexemType.NUMB;
                currentLexem.value = Double.Parse(builder.ToString());
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
                    if (operations.containsCmd(inputStr[pt]))
                    {
                        if (currentLexem.type == LexemType.CMD)
                        {
                            throw new SyntaxException("Two args in a row", pt);
                        }
                        currentLexem.type = LexemType.CMD;
                        currentOperation = operations.getOperation(inputStr[pt]);
                    } else throw new SyntaxException("Unknown argument", pt);
                    
                    break;
            }
            pt++;
        }

        public double expression()
        {
            double a = item();
            while ((currentLexem.type == LexemType.CMD) &&(currentOperation.GetPriority()==Priority.LOW))
            {
                nextLexem();
                a = currentOperation.Execute(a, item());
            }
            return a;
        }
        public double item()
        {
            double a = mult();
            while ((currentLexem.type == LexemType.CMD) &&(currentOperation.GetPriority()==Priority.HIGHT))
            {
                nextLexem();
                a = currentOperation.Execute(a, item());
            }
            return a;
        }

        public double mult()
        {
        double a = 0;
        switch (currentLexem.type)
        {
            case LexemType.NUMB:
                a = currentLexem.value;
                nextLexem();
                break;
            case LexemType.OPEN:
                nextLexem();
                a = expression();
                if (currentLexem.type !=LexemType.CLOSE)
                {
                    throw new SyntaxException("Incomplete expression, expected \")\"", pt);
                }
                    else nextLexem();
                break;
            case LexemType.CMD:
                if(typeof(Subtraction).IsInstanceOfType(currentOperation))
                {
                    nextLexem();
                    a = currentOperation.Execute(a, mult());
                }
                    else if(currentOperation.GetNumberOfArgs() == NumberOfArgs.ONE)
                {
                    nextLexem();
                    a = currentOperation.Execute(mult());
                }
                    else throw new SyntaxException("Unexpected command", pt-1);
                break;
            default:
                throw new SyntaxException("Unexpected command", pt-1);
        }
        return a;
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
        internal double value;
        internal LexemType type;
    }

}
