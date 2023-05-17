using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public interface ICartRepository
    {
        public Cart FindCartByUserId(Guid userId); // zwykle w interfejsach nie pisze się "public", ale jak Ci wygodniej to możesz zostawić
        public Product FindProductById(int productId); // ta metoda jest chyba do usunięcia? To samo masz w IProductRepository
        public void UpdateCartToDatabase(Guid userId, Cart current); // ta metoda lepiej jak się nazywa "UpdateCart". Nie musisz jawnie pisać, że robisz update bazy danych. Zauważ, że to równie dobrze mógłby być plik.
    }
}
