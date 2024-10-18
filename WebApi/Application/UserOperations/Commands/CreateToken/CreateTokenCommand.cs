using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CreateTokenCommand(IBookStoreDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public Token Handle()
        {
            var user = _context.Users.SingleOrDefault(user => user.Email == Model.Email && user.Password == Model.Password);
            if (user is null)
                throw new InvalidOperationException("Kullanıcı Adı - Şifre Hatalı!");

            TokenHandler tokenHandler = new TokenHandler(_configuration);

            Token token = tokenHandler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5); // 5 dakika daha ekle

            _context.SaveChanges();
            return token;
        }
    }
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}