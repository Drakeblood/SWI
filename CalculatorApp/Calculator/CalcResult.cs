namespace CalculatorApp
{
    public class CalcResult
    {
        public string name;
        public double result;

        public override string ToString()
        {
            return $"{name}: {result}";
        }
    }
}
