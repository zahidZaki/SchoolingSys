using System.Net.Http.Json;
using SchoolSysMvc.Dto;
using Microsoft.Extensions.Configuration;
using SchoolSysMvc.Interfaces;

namespace SchoolSysMvc.Services
{


    public class TeacherService :ITeacherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _gatewayBaseUrl;

        public TeacherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _gatewayBaseUrl = configuration["ApiGateway:BaseUrl"];
        }

        // 📘 Get all teachers
        public async Task<List<TeacherDto>> GetAllTeachersAsync()
        {
            var response = await _httpClient.PostAsJsonAsync($"{_gatewayBaseUrl}/tch/teacher/getAllTeacher", new { });
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<TeacherDto>>>();
            if (result != null && result.Status == (int)ApiResponseStatus.Success)
                return result.Data ?? new List<TeacherDto>();

            return new List<TeacherDto>();
        }

        // 📘 Get single teacher by Id
        public async Task<TeacherDto?> GetTeacherByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_gatewayBaseUrl}/tch/teacher/getTeacherById?id={id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TeacherDto>>();
            if (result != null && result.Status == (int)ApiResponseStatus.Success)
                return result.Data;

            return null;
        }

        // 📘 Add new teacher
        public async Task<bool> AddTeacherAsync(TeacherDto teacher)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_gatewayBaseUrl}/tch/teacher/addTeacher", teacher);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result?.Status == (int)ApiResponseStatus.Success;
        }

        // 📘 Update existing teacher
        public async Task<bool> UpdateTeacherAsync(TeacherDto teacher)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_gatewayBaseUrl}/tch/teacher/updateTeacher", teacher);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result?.Status == (int)ApiResponseStatus.Success;
        }

        // 📘 Delete teacher
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_gatewayBaseUrl}/tch/teacher/deleteTeacher?id={id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result?.Status == (int)ApiResponseStatus.Success;
        }
    }

}
