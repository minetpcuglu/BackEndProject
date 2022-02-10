using EntityLayer.Entities.Interface;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities.Concrete
{
    public abstract class BaseEntity<T> : IBaseEntity
    {
        public T Id { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => value = _status; }

    }
}
