using System;

namespace Domain
{
    public interface ISoftDelete 
    {
        bool SoftDeleted { get; set; }

   }
}
