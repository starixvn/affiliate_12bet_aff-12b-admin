using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IDataBox
    {
       IObjectDb ObjectDb { get; }
    }
}
