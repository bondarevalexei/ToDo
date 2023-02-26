namespace Shared.RequestFeatures
{
    public class ToDoTaskParameters : RequestParameters
    {
        public ToDoTaskParameters() =>
            OrderBy = "title";
    }
}
