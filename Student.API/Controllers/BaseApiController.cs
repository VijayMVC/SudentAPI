using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Student.API.Helpers;

namespace Student.API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected const Int32 MaxPageSize = 10;

        public IEnumerable<T> HandlePaging<T>(IEnumerable<T> modelList, String sort, String fields = null, Int32 page = 1, Int32 pageSize = 10, Int32 maxPageSize = MaxPageSize)
        {
            var enumerable = modelList as IList<T> ?? modelList.ToList();
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var totalCount = enumerable.Count();
            var totalPages = Convert.ToInt32(Math.Ceiling((Double)totalCount / pageSize));

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 1
                ? urlHelper.Link("StudentsList", new
                {
                    fields = fields,
                    page = page - 1,
                    pageSize = pageSize,
                    sort = sort
                })
                : "";
            var nextLink = page < totalPages
                ? urlHelper.Link("StudentsList", new
                {
                    fields = fields,
                    page = page + 1,
                    pageSize = pageSize,
                    sort = sort
                })
                : "";

            var paginationHeader = new
            {
                currentPage = page,
                pageSize = pageSize,
                totalCount = totalCount,
                totalPages = totalPages,
                previousPageLink = prevLink,
                nextPageLink = nextLink
            };

            HttpContext.Current.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            var results = enumerable
                .ApplySort(sort)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();

            return results;
        }
    }
}
