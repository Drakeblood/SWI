using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace CalculatorApp
{
    public class Calculator
    {
        public List<CalcData> Operations = new List<CalcData>();

        private static Tuple<string, CalculatorOperationBase>[] operators =
            new Tuple<string, CalculatorOperationBase>[]
            {
                new ("add", new CalculatorOperation_Add()),
                new ("sub", new CalculatorOperation_Substract()),
                new ("mul", new CalculatorOperation_Multiply()),
                new ("sqrt", new CalculatorOperation_SquareRoot())
            };

        public List<CalcResult> GetResultSafe()
        {
            List<CalcResult> result = new List<CalcResult>();

            foreach (var operation in Operations)
            {
                try
                {
                    result.Add(new CalcResult { name = operation.objectName, result = ExecuteOperation(operation) });
                }
                catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
            }

            return result;
        }

        private double ExecuteOperation(CalcData calcData)
        {
            if(calcData == null) throw new ArgumentException("Null calculation data");

            //With small operators count iterating throught static array will be faster than Dictionary
            for (int i = 0; i < operators.Length; i++)
            {
                if (calcData.@operator == operators[i].Item1)
                {
                    return operators[i].Item2.ExecuteOperation(calcData.value1, calcData.value2);
                }
            }

            throw new ArgumentException($"Operator \"{calcData.@operator}\" doesn't exist ({calcData.objectName})");
        }

        public void SetupOperationsFromJSON(string jsonString)
        {
            JsonNode? jsonNode = JsonNode.Parse(jsonString);
            if (jsonNode == null)
            {
                Console.WriteLine("input.json parsing failed");
                return;
            }

            foreach (var node in jsonNode.AsObject())
            {
                if (node.Value == null)
                {
                    Console.WriteLine($"{node.Key} data doesn't exist");
                    continue;
                }

                CalcData? calcData = JsonConvert.DeserializeObject<CalcData>(node.Value.ToString());
                if (calcData == null)
                {
                    Console.WriteLine($"{node.Key} deserialization process failed. Please check your input.json");
                    continue;
                }
                calcData.objectName = node.Key;
                Operations.Add(calcData);
            }
        }
    }
}
