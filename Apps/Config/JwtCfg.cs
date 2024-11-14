using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Config
{
    public class JwtCfg
    {
        public string Secret { get; set; } = string.Empty;
        public int AccessTokenTtl { get; set; }
        public int RefreshTokenTtl { get; set; }
    }
}