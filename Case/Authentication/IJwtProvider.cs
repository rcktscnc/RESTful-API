using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Authentication
{
    public interface IJwtProvider
    {
        string GetToken();
    }
}
