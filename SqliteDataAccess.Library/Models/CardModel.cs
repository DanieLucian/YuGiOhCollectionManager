
namespace SqliteDataAccess.Library.Models
{
    public abstract class CardModel
    {

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class FullCardModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
