using Library.Business.Abstract;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Concrete
{
    using DataAccess.Abstract;
    using DataAccess.Concrete.Xml;
    public class BookManager : IBookService
    {
        private IBookDal bookDal;
        public int LastId;

        public BookManager()
        {
            bookDal = new XBookDal();
        }
        public BookManager(IBookDal iBookDal)
        {
            bookDal = iBookDal;
        }

        public void Add(Book book)
        {
            int lastId = LastIdentity(GetAll());
            book.Identity = lastId;
            LastId = lastId;
            bookDal.Add(book);
        }

        public void Delete(Book book)
        {
            bookDal.Delete(book);
        }

        private int LastIdentity(List<Book> theList)
        {
            int number = 0;
            foreach (var item in theList)
            {
                if (item.Identity > number)
                    number = item.Identity;
            }

            return number + 1;
        }

        public Book Get(int bookId)
        {
            return bookDal.GetAll().FirstOrDefault(i => i.Identity == bookId);
        }

        public List<Book> GetBooksOfStudent(int studentId)
        {
            return bookDal.GetAll().Where(i => i.BookLocation == studentId).ToList();
        }

        public List<Book> Search(string key)
        {
            List<Book> aramaListesi = new List<Book>();
            if (key == "" || key == null)
            {
                return bookDal.GetAll();
            }
            else
            {
                aramaListesi = bookDal.GetAll().Where(
                 i => i.BookName.ToLower().Contains(
                     key.ToLower())).ToList();

                List<Book> Barkodlu = SearchbyBarcode(key);

                foreach (var item in Barkodlu)
                {
                    aramaListesi.Add(item);
                }

            }
            return aramaListesi;
        }

        public List<Book> SearchbyBarcode(string key)
        {
            return bookDal.GetAll().Where(
                i => i.BookBarcode.ToLower().Contains(
                    key.ToLower())).ToList();
        }

        public List<Book> GetAll()
        {
            return bookDal.GetAll();
        }

        public void Update(Book book)
        {
            bookDal.Update(book);
        }

        public List<Book> EscrowBooks()
        {
            List<Book> Escrows = bookDal.GetAll();
            return Escrows.Where(i => i.BookLocation != 0).ToList();
        }

        public Book GetBookByName(string name)
        {
            return bookDal.GetAll().FirstOrDefault(i => i.BookName == name);
        }
    }
}
