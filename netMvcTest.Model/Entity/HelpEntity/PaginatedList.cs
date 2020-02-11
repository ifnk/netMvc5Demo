﻿using System;
using System.Collections.Generic;
 using System.Data.Entity;
 using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Api.Entities
{
    public class PaginatedList<T> : List<T> where T : class // 本身是一个集合 ，在集合 的基础上加了一些分页信息 
    {
        public int PageSize { get; private set; } //一页显示多少 数量
        public int PageIndex { get; private set; } // 当前页 加 private 避免 值在 类型 外被设置 

        public int TotalCount { get; private set; } //总数量
        public int TotalPages { get; private set; } //总共 有几页


        //构造函数 
        public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>>  CreatePaginateQueryAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}