namespace WpfDesktopUI.Library.Models
{
    public class CollectionCard
    {

        public int SetId { get; set; }

        public string SetName { get; set; }

        public string SetCode { get; set; }

        public int CardId { get; set; }

        public string CardName { get; set; }

        public string RarityName { get; set; }

        public string ActualRarityCode =>
        RarityCode == string.Empty ? RarityName : RarityCode.Replace("(", string.Empty).Replace(")", string.Empty);

        private string RarityCode { get; set; }

        public int Quantity { get; set; }

        public CollectionCard()
        {

        }

    }
}
