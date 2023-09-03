using SqliteDataAccess.Library.DTOs;

namespace WpfDesktopUI.Library.Models
{
    public abstract class Card
    {

        public string Name { get; }

        public string Description { get; }

        public abstract string[] FrameType { get; }

        public Card(CardDTO card)
        {
            Name = card.Name;
            Description = card.Description;
        }

    }
}
