using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class RegisterDBContext
    {
        public static implicit operator RegisterDBContext(DBContext v)
        {
            throw new NotImplementedException();
        }
    }
}