using Libra.Database.Interfaces;
using Libra.Models.BookModels;
using Libra.Services.Interfaces;

namespace Libra.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IPictureService _pictureService;

    public BookService(IBookRepository repository, IPictureService pictureService)
    {
        _repository = repository;
        _pictureService = pictureService;
    }

    public List<Book> GetAvailableBooks()
    {
        return _repository.GetAll()
            .Where(x => x.IsAvailable)
            .ToList();
    }

    public Book GetById(Guid id)
    {
        var book = _repository.GetById(id);

        if (book == null)
            throw new Exception($"Book with {id} not found");

        return book;
    }

    public List<Book> GetByOwnerId(Guid id)
    {
        return _repository.GetByOwnerId(id);
    }

    public List<Book> GetByBookName(string bookName)
    {
        return _repository.GetByBookName(bookName);
    }

    public List<Book> GetByBookAuthor(string author)
    {
        return _repository.GetByBookAuthor(author);
    }

    public Book AddBook(AddBookModel model)
    {
        if (model.Author == string.Empty)
            throw new Exception("Book author can't be empty");
        if (model.OwnerId == string.Empty)
            throw new Exception("Book owner can't be empty");
        if (model.BookName == string.Empty)
            throw new Exception("Book name can't be empty");

        var bookPictures = new List<BookPicture>();

        var book = new Book
        {
            OwnerId = new Guid(model.OwnerId),
            BookName = model.BookName,
            Description = model.Description,
            Author = model.Author,
            PublishingDate = model.PublishingDate
        };
        
        var newBook = _repository.AddBook(book);
        
        foreach (var bookPicture in model.Pictures)
        {
            var pic = new BookPicture
            {
                FilePath = $"{model.OwnerId}/{newBook.Id}/{Guid.NewGuid() + Path.GetExtension(bookPicture.FileName)}",
                BookId = newBook.Id,
            };

            _pictureService.AddPicture(pic);
            _pictureService.SavePictureToStorage(pic, bookPicture);

            bookPictures.Add(pic);
        }

        newBook.Pictures = bookPictures;
        
        return newBook;
    }

    public List<Book> GetBooksGroup(List<Guid> bookIds)
    {
        return _repository.GetBooksGroup(bookIds);
    }
}