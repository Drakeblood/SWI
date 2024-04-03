using System.Text;

using CalculatorApp;

internal class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists("input.json"))
        {
            Console.WriteLine("Cannot find input.json");
            return;
        }

        string input = File.ReadAllText("input.json");

        Calculator calculator = new Calculator();

        try
        {
            calculator.SetupOperationsFromJSON(input);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message + "\n Please check your input.json file"); }

        List<CalcResult> result = calculator.GetResultSafe();
        result.Sort((x, y) => x.result.CompareTo(y.result));

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < result.Count; i++)
        {
            stringBuilder.Append(result[i].ToString() + "\n");
        }

        File.WriteAllText("output.txt", stringBuilder.ToString());
    }
}