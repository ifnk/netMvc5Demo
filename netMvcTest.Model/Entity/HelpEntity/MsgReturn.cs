﻿namespace netMvcTest.Model.Entity.HelpEntity
{
    /// <summary>
    /// 返回的 实体  return ok(xx-xx)
    /// </summary>
    public class MsgReturn<T>
    {
        /// 是否成功
        public bool Success { get; set; } = true;
        /// 返回消息
        public string Msg { get; set; } = "成功！";
        /// 返回数据 
        public T Response { get; set; }
    }
}