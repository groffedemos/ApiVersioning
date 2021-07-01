using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using APIContagem.V3.Models;

namespace APIContagem.V3.Controllers
{
    [ApiController]
    [ApiVersion("3.0")]
    [Route("[controller]")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ContadorController : ControllerBase
    {
        private static readonly Contador _CONTADOR = new Contador();
        private readonly ILogger<ContadorController> _logger;
        private readonly IConfiguration _configuration;

        public ContadorController(ILogger<ContadorController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ResultadoContador GetV3_0()
        {
            int valorAtualContador;
            lock (_CONTADOR)
            {
                _CONTADOR.Incrementar();
                valorAtualContador = _CONTADOR.ValorAtual;
            }

            _logger.LogInformation($"Contador - Valor atual: {valorAtualContador}");

            return new()
            {
                ValorAtual = valorAtualContador,
                Versao = "3.0",
                Local = _CONTADOR.Local,
                Kernel = _CONTADOR.Kernel,
                TargetFramework = _CONTADOR.TargetFramework,
                Mensagem = _configuration["MensagemVariavel"],
                Saudacao = "Bem-vindos a mais uma live"
            };
        }
    }
}