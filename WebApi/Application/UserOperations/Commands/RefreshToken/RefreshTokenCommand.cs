using AutoMapper;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.SingleOrDefault(user => user.RefreshToken == RefreshToken && user.RefreshTokenExpireDate > DateTime.Now);
            if (user is null)
                throw new InvalidOperationException("Valid bir Refresh Token bulunamadı");

            TokenHandler tokenHandler = new TokenHandler(_configuration);

            Token token = tokenHandler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5); // 5 dakika daha ekle

            _context.SaveChanges();
            return token;
        }
    }
}