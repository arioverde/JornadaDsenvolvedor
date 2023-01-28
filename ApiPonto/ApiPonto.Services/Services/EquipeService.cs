using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class EquipeService
    {
        private readonly EquipeRepository _repository;
        public EquipeService()
        {
            _repository = new EquipeRepository();
        }
    }
}