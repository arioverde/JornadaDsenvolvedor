using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepository _repository;
        public FuncionarioService()
        {
            _repository = new FuncionarioRepository();
        }
    }
}