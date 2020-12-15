
namespace Library.Business.Abstract
{
    using Entity.Concrete;
    using System.Collections.Generic;
    interface IBookService
    {
        List<Book> GetAll();
        List<Book> EscrowBooks();
        Book Get(int id);
        Book GetBookByName(string name);
        void Add(Book book);
        void Update(Book book);
        void Delete(Book book);
    }
}
