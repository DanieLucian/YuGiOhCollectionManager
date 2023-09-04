namespace WpfDesktopUI.Library.Models
{
    public abstract class Card
    {

        public string Name { get; }

        public string Description { get; }

        public abstract string[] FrameType { get; }

        public Card()
        {
        }

        public Card(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
