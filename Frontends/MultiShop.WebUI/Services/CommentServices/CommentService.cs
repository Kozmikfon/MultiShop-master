using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // --- Mevcut Metotlar (Aynı Kalabilir) ---
        public async Task CreateCommentAsync(CreateCommentDto createCommentDto) => await _httpClient.PostAsJsonAsync("comments", createCommentDto);
        public async Task DeleteCommentAsync(string id) => await _httpClient.DeleteAsync("comments?id=" + id);
        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto) => await _httpClient.PutAsJsonAsync("comments", updateCommentDto);

        public async Task<UpdateCommentDto> GetByIdCommentAsync(string id)
        {
            var response = await _httpClient.GetAsync("comments/" + id);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<UpdateCommentDto>() : null;
        }

        public async Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            var response = await _httpClient.GetAsync("comments");
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
        }

        public async Task<List<ResultCommentDto>> CommentListByProductId(string id)
        {
            var response = await _httpClient.GetAsync($"comments/CommentListByProductId/{id}");
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
        }

        // --- KRİTİK İSTATİSTİK METOTLARI (GÜVENLİ HALE GETİRİLDİ) ---

        public async Task<int> GetTotalCommentCount()
        {
            // API'de rota "GetTotalCommentCount" mı yoksa sadece "GetTotalCommentCount" mı kontrol et
            var response = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(content) ? 0 : int.Parse(content);
            }
            return 0;
        }

        public async Task<int> GetActiveCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetActiveCommentCount");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(content) ? 0 : int.Parse(content);
            }
            return 0;
        }

        public async Task<int> GetPAssiveCommentCount() // İsimdeki 'A' harfine dikkat (API ile aynı olmalı)
        {
            // API tarafında 'GetPassiveCommentCount' ise burayı da öyle yapmalısın!
            var response = await _httpClient.GetAsync("comments/GetPassiveCommentCount");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(content) ? 0 : int.Parse(content);
            }
            return 0;
        }
    }
}