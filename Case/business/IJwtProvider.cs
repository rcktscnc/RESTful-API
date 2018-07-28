using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Auth
{
    public interface IJwtProvider
    {
        string GetToken();
    }
}
