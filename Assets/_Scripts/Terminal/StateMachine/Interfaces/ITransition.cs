public interface ITransition
{
    IState To {get;} // What state to go to
    IPredicate Condition {get;} // What condition allowing going to state
}
