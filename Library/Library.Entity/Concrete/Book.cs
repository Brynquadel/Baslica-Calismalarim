
namespace Library.Entity.Concrete
{
    using Library.Entity.Abstract;
    using System;

    public class Book : IEntity
    {
        public int Identity { get; set; }
        public string BookName { get; set; }
        public string BookType { get; set; }
        public string BookBarcode { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfCommitment { get; set; } 
        public int BookLocation { get; set; }
        public DateTime BookReleaseDate { get; set; }
    }

}
