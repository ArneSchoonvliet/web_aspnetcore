﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Digipolis.Web.Headers
{
    public interface IHeaderHandler
    {
        void Handle(StringValues values);
    }
}
