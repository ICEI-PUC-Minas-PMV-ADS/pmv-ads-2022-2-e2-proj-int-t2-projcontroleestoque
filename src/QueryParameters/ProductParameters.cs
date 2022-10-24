using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProjControleEstoque.QueryParameters
{
    public class ProductParameters
    {
        [BindRequired]
        public string? ProductName { get; set; }
    }
}
