using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Web.Services.Base
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T, T2>(string url, T2 data);
        Task<T> PutAsync<T, T2>(string url, T2 data);
        Task DeleteAsync<T>(string url);
    }
}
