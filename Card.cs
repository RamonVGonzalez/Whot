namespace Whot
{
    public class Card
    {
        private CardSymbol cardSymbol;
        private int cardNumber;
        public Card(CardSymbol cardSymbol, int cardNumber)
        {
            this.cardSymbol = cardSymbol;
            this.cardNumber = cardNumber;
        }

        public int GetCardNumber() => cardNumber;
        public CardSymbol GetCardSymbol() => cardSymbol;

        public override string ToString()
        {
            return string.Format("".PadRight(3)+"|{0,-4} {1,10} {0,8}|".PadRight(3),cardNumber,cardSymbol);
        }

        public override bool Equals(object obj)
        {
            Card otherCard = (Card)obj;
            return cardNumber == otherCard.cardNumber && cardSymbol == otherCard.cardSymbol;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public enum CardSymbol
    {
        CIRCLE, TRIANGLE, CROSS, SQUARE, STAR
    }
}
