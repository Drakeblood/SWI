namespace CalculatorApp
{
    public class CalculatorOperation_Add : CalculatorOperationBase
    {
        public override double ExecuteOperation(params double[] args)
        {
            if (args.Length < 2) throw new ArgumentException("Function should take at least 2 arguments");

            double result = args[0];

            for (int i = 1; i < args.Length; i++)
            {
                result += args[i];
            }

            return result;
        }
    }
}
