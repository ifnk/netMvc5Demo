using System;

namespace netMvcTest.Model.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        //创建时间
        public DateTime CreateTime { get; set; } = DateTime.Now;

        //是否被删除(伪删除)
        public bool IsRemoved { get; set; } = false;
    }
}