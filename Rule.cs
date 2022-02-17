namespace Whot
{
    public class Rule
    {
        public static bool IsValidMove(Card card, Card centreCard)
        {
            //check if the card number or symbol is the same
            if (card.GetCardNumber() == centreCard.GetCardNumber() 
                || card.GetCardSymbol() == centreCard.GetCardSymbol())
            {
                return true;
            }
            return false;
  
        }

    }
}
