using System;

namespace NuevoInterView.CMS.Repository.Interface
{
    public interface IDbEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}