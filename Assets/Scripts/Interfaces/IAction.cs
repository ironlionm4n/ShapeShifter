public interface IAction 
{
    public void Execute();

    public void Undo();

    public string GetAnimalType();
}
