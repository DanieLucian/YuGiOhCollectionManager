using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public abstract class CardDisplay
    {

        public string Name { get; }

        public string Description { get; }

        public abstract string[] FrameType { get; }

        public CardDisplay(CardModel card)
        {
            Name = card.Name;
            Description = card.Description;
        }

    }
}
