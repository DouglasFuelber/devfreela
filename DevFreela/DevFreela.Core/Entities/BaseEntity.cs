using System;

namespace DevFreela.Core
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { }

        public int Id { get; private set; }
    }
}
