public interface IAction 
{
    public void Execute();

    public void Undo();

    public AnimalType GetAnimalType();
}
