using APIGOTinforcavado.Repositories;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Services
{
    public class NewsLetterService
    {
        private readonly NewsLetterRepository _newsLetterRepository;

        public NewsLetterService(NewsLetterRepository newsLetterRepository)
        {
            _newsLetterRepository = newsLetterRepository;
        }

        public Task<List<NewsLetter>> GetAllAsync() => _newsLetterRepository.GetAllAsync();

        public Task AddAsync(NewsLetter newsLetter) => _newsLetterRepository.CreateAsync(newsLetter);

        public Task<bool> DeleteAsync(string email) => _newsLetterRepository.DeleteAsync(email);

    }
}
