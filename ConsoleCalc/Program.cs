using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            string input;
            double output;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter the expression (press 'q' for exit):");
                input = Console.ReadLine().Replace(" ", "");
                if (input[0] == 'q')
                    Environment.Exit(0);
                try
                {
                    output = parser.CountExpressionFromString(input);
                    Console.WriteLine("\nAnswer is " + output + ".");
                }
                catch (SyntaxException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArithmeticException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                Console.Write("\nPress any key to continue... ");
                Console.ReadLine();
            }
        }
    }
}
