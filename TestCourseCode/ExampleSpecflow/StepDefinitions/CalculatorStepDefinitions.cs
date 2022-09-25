namespace ExampleSpecflow.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private int _sum;

        private int _secondNumber;

        private int _firstNumber;

        private Calculator.Calculator _calculator = new Calculator.Calculator();
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _firstNumber = number;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _secondNumber = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _sum = _calculator.sum(_firstNumber, _secondNumber);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.Equal(result, _sum);
        }
    }


}