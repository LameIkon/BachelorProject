using System;

public class FuncPredicate : IPredicate
{
    private readonly Func<bool> _function; // Prediction of code. Assigned 1 time

    public FuncPredicate(Func<bool> function) // For other classes to assign their bool function to this 
    {
        _function = function;
    }

    public bool Evaluate() => _function.Invoke(); // Predicate code
}
