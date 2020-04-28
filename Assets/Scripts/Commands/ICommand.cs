using Player;

namespace Command
{
    public interface ICommand
    {
        string name {get; set;}
        int[] parameters {get; set;}
        PlayerController player {get; set;}

        bool Run();
    }
}