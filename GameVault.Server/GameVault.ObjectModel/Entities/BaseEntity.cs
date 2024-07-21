using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameVault.ObjectModel.Entities
{
    public class BaseEntity
    {
        public Guid Guid { get; set; }
    }
}
