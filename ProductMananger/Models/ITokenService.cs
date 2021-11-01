using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMananger.Models
{
    public interface ITokenService
    {
        string FetchToken();
        void SetCacheToken(string token, DateTime expr);
    }
}
