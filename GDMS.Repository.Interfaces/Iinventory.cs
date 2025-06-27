using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
    public interface Iinventory<T> where T : class
    {
        IEnumerable<T> Get();
    }
}