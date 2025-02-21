namespace Fanex.Affiliate.User.Core.DTO
{
    using System.Collections.Generic;

    public class EntityResult<T> where T : class
    {
        public IEnumerable<T> MainDataList { get; set; }

        public T Total { get; set; }

        public int TotalItem { get; set; }
    }
}
