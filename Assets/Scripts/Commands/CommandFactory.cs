using Command;

public class CommandFactory
{
    public ICommand Build(string name)
    {
        if (name == "Input") {
            return new Input();
        }
        else if (name == "Output") {
            return new Output();
        }

        return null;
    }
}