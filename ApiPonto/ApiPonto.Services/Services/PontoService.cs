using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class PontoService
    {
        private readonly PontoRepository _repository;
        public PontoService()
        {
            _repository = new PontoRepository();
        }
    }
}