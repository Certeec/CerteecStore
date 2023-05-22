using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;
using CerteecStore.Application.Database;

namespace CerteecStore.Application.Carts
{
    // Według mnie trochę jeszcze nie czujesz przeznaczenia tych repozytoriów. One mają odpowiadać tylko i wyłącznie za
    // komunikację z bazą danych w miarę prostych metodach. Dodałem w interfejsie ICartRepository metody, które według mnie
    // powinny się tam znaleźć, a reszta logiki, która spełnia Twoje wymagania powinna trafić do CartService.
    public class InMemoryCartRepository :  ICartRepository
    {

        public Cart FindOrCreateCartByUserId(Guid id)
        {
            try
            {
                return InMemoryDatabase.Carts.First(n => n.Key == id).Value;
            }
            catch(Exception e)
            {
                return new Cart();

                /// I tutaj ten sam problem.. Po co.. mi zwracac
                /// ten pusty new Cart.. skoro itak go nie wkladam do bazy..
                /// moge ewentualnie go wlozyc z CartService.. ale w sumie po co?..
                
                // Według mnie ten design jest bez sensu. Ogólnie chcesz zawsze żeby klasy typu "Repository"
                // miały jak najmniej dodatkowej logiki, a jedynie taką która odpowiada za dodawanie/usuwanie elementów z bazy.
                // Funkcjonlaność tworzenia koszyka przeniósłbym do CartServie, bo tam jest logika biznesowa.
            }
        }

        public void UpdateCart(Guid userId, Cart current)
        {
            InMemoryDatabase.Carts[userId] = current;
        }

        public double CountCartValue(Guid userId)
        {
            double value = 0;
            Cart userCart = FindOrCreateCartByUserId(userId);
            for(int i = 0; i < userCart.Products.Count(); i++)
            {
                Product current = userCart.Products.ElementAt(1).Key;
                int multiplyBy = userCart.Products.ElementAt(1).Value;
                value += current.ItemPrice * multiplyBy; 
            }
            
            return value;
        }

        public Cart AddProductToCart(Guid userId, Product productToAdd, int quantity)
        {
            Cart userCart = FindOrCreateCartByUserId(userId);
            userCart.Products.Add(productToAdd, quantity);
            return userCart;
            
        }

        public int TakeProductFromTheCart(Guid userId, Product productToRemove)
        {

            ///This function needs to be Fixed. Throwing expection at line 60.
           /// problem occuring while removing Value by key;
            Cart userCart = FindOrCreateCartByUserId(userId);
            try
            {
                userCart.Products[productToRemove] -= 1;
                if (userCart.Products[productToRemove] < 1)
                {
                    userCart.Products.Remove(productToRemove);
                }
                UpdateCart(userId, userCart); /// I tutaj widze problem
                /// bo w jedyn miejscu wywoluje updateCart z Cartservice
                /// przez co jest bezpieczniej... bo nie pomyli sie ktos w klasach pod interface..
                /// ale wtedy nie zwroce latwo informacji ile zostalo przedmiotow tego rodzaju..
                /// Wydaje mi sie ze powinienem ALbo
                /// A nie zwracac ilosci ile przedmiotow zostalo...
                /// B zwracac ale logike tego zrobic po stronie Service...
                return userCart.Products[productToRemove];
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
          
        }

        /// Ogolnie chce dokonczyc ogarniac ten syf co sie tutaj porobil i zrobic testy
        /// zeby pozniej to jakos sensowniej przepisywac... tylko
        /// obecnie caly czas sie zmienia design jak sobie znajduje nastepne prolbemy..
    }
}
