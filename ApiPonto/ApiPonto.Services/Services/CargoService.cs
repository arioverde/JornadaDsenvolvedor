using ApiPonto.Models.Models;
using ApiPonto.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPonto.Services.Services
{
    public class CargoService
    {
        private readonly CargoRepository _repository;
        public CargoService()
        {
            _repository = new CargoRepository();
        }
        public List<Cargo> Listar(string? descricao)
        {
            try
            {
                _repository.AbrirConexao();
                return _repository.ListarCargos(descricao);
            }
            finally
            {
                _repository.FecharConexao();
            }
        }

    }
}
