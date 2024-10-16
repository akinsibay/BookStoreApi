using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commmands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDBContext _context;

        public UpdateGenreCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Bu ID de bir tür bulunamadı");

            if (_context.Genres.Any(genre => genre.Name.ToLower() == Model.Name.ToLower() && genre.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli bir kitap zaten mevcut");

            genre.Name = Model.Name.Trim() == default ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;

            _context.SaveChanges();
        }

    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}