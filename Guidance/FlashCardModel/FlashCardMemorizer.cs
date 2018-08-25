using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
namespace Guidance.FlashCardModel
{
    public class FlashCardMemorizer
    {
        public void FlashCardsToMemorize()
        {
            //using (FlashCardDataRepository flashCardDataRepository = new FlashCardDataRepository())
            //{
            //    var allFlashCardsData = flashCardDataRepository.GetAll();
                
            //}

        }
        /// <summary>
        ///  1. Sortowanie wg LastResult: jesli jest jakies na false to musi byc podane w pierwszej kolejnosci
        ///  czyli poczatkowa wartosc fiszki to false dla LastResult oraz gdy sie okaze, ze przy przypominaniu juz tego nie pamietamy
        ///  to nalezy powtorzyc to jak najwczesniej, ale juz nie tego samego dnia
        ///  2. Sortowanie wg SuccessfulAnserws i LastOccurrence:
        ///  wzor : 2^SA - (CD - Today)
        ///  jesli liczba jest ujemna to znaczy, ze juz dawno powinno dojsc do powtorzenia fiszki
        ///  im wartosc blizej 0 tym lepiej
        ///  na topie listy znajda sie fiszki z najmniejszymi wartosciami
        /// </summary>
        /// <param name="flashCardDatas"></param>
        /// <returns></returns>
        public IEnumerable<int> Sort( List<FlashCardData> flashCardDatas)
        {
            var sortedList = flashCardDatas.OrderBy(x => x.LastOccurrence != DateTime.Today);
            sortedList = sortedList.OrderBy(x => x.LastResult == false);
            sortedList = sortedList.OrderBy(x => 2 ^ x.SuccessfulAnserws - ((x.CreationDate - DateTime.Today).Days));
            var results = sortedList.Select(x => x.FlashCardId);
            return results;
        }
    }
}
