namespace CalculatorApp
{
    public class CalculatorOperation_SquareRoot : CalculatorOperationBase
    {
        public override double ExecuteOperation(params double[] args)
        {
            if (args.Length < 1) throw new ArgumentException("Function should take 1 argument");

            return Math.Sqrt(args[0]);
        }
    }
}
