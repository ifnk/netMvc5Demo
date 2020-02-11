namespace netMvcTest.Model.Entity.HelpEntity
{
    // 将 查询参数 封装 成 一个类
    public class QueryParameter
    {
        private const int MaxPageSize = 100; // 设置最大 页数，如果 不加限制 如果 pageSize传进来 10000也会对数据库造成压力 

        //当前 页数 
        public int PageIndex { get; set; } = 1; // 如果不传 设置默认值

        //每页 数量
        public string Key { get; set; }

        private int _pageSize=5;

        // 设置pageSize 和 MaxPageSize 之间 的关系 
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize =
                (value > MaxPageSize)
                    ? MaxPageSize
                    : value; // 如果大于maxPageSize 就 取 maxPageSize (最大值设定的是100) ,小于20 的话就取  pageSize 
        }
    }
}