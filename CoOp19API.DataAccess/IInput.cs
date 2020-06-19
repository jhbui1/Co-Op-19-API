using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoOp19API.DataAccess
{
    public interface IInput
    {
        Task<T> Add<T>(T item) where T : class;
    }
}
