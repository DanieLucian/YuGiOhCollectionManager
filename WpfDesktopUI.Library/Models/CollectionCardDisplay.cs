using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public class CollectionCardDisplay
    {

        public int SetId { get; set; }

        public string SetName { get; set; }

        public string SetCode { get; set; }

        public int CardId { get; set; }

        public string CardName { get; set; }

        public string RarityName { get; set; }

        public string RarityCode { get; set; }

        public int Quantity { get; set; }

        public CollectionCardDisplay(CollectionCardModel collectionCardModel)
        {
            SetId = collectionCardModel.SetId;
            SetName = collectionCardModel.SetName;
            SetCode = collectionCardModel.SetCode;
            CardId = collectionCardModel.CardId;
            CardName = collectionCardModel.CardName;
            RarityName = collectionCardModel.RarityName;
            RarityCode = collectionCardModel.RarityCode == "" ?
                         collectionCardModel.RarityName :
                         collectionCardModel.RarityCode.Replace("(", "").Replace(")", "");
            Quantity = collectionCardModel.Quantity;
        }

    }
}
