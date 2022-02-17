using System.Collections.Generic;

namespace Whot
{
    public interface IPlayer
    {
        string GetNickname();
        bool Play(int cardPosition);
        void GoMarket();
        List<Card> GetCards();
        int GetTotalValueofCards();
        void DropCards();  
    }
}
