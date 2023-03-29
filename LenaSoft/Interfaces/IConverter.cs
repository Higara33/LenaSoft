using LenaSoft.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenaSoft.Interfaces
{
    public interface IConverter<T>
    {
        public List<T> Convert(DataTable data);
    }
}
