using Microsoft.AspNetCore.Mvc;
    [ApiController]
    public class FacturacionController : ControllerBase
    {
        //Array
        public List<Cliente> clientes = new List<Cliente>
        {
            new Cliente
            {
                Nombre = "Sebastian",
                Apellidos = "Diaz",
                Edad = 30,
                Rut = "11111111-1",
                NombreEmpresa = "Empresa",
                RutEmpresa = "12345678-9",
                GiroEmpresa = "primergiro",
                TotalVentas = 300f,
                MontoVentas = 3000000f
            },
            new Cliente
            {
                Nombre = "Juan",
                Apellidos = "Diaz",
                Edad = 20,
                Rut = "22222222-2",
                NombreEmpresa = "Empresa2",
                RutEmpresa = "14345678-9",
                GiroEmpresa = "segundogiro",
                TotalVentas = 300f,
                MontoVentas = 3000000f
            },
            new Cliente
            {
                Nombre = "Vicente",
                Apellidos = "Diaz",
                Edad = 26,
                Rut = "19442520-1",
                NombreEmpresa = "Empresa3",
                RutEmpresa = "12765983-9",
                GiroEmpresa = "tercergiro",
                TotalVentas = 300f,
                MontoVentas = 3000000f
            },
            // Aqui se puede agregar mas clientes de ser necesario
        };
        //metodo get para traer a los clientes
        [HttpGet("clientes")]
        public IActionResult GetClientes()
        {
            return Ok(clientes);
        }
        //metodo GET para llamar tres empresas distintas
        [HttpGet("tres-empresas")]
        public IActionResult GetTresEmpresas()
        {
            // Selecciona tres empresas diferentes de la lista
            var tresEmpresas = clientes.Take(3).ToList();
            return Ok(tresEmpresas);
        }

        //Método GET para listar todos los datos de una empresa en particular por su Rut
        [HttpGet("empresas/{rutEmpresa}")]
        public IActionResult GetEmpresa(string rutEmpresa)
        {
            var empresa = clientes.Find(c => c.RutEmpresa == rutEmpresa);
            if (empresa == null)
            {
                return NotFound("Empresa no encontrada");
            }
            return Ok(empresa);
        }

        //Método POST para crear y guardar una nueva empresa
        [HttpPost("empresas")]
        public IActionResult CrearEmpresa([FromBody] Cliente empresa)
        {
            clientes.Add(empresa);
            return CreatedAtAction("GetEmpresa", new { rutEmpresa = empresa.RutEmpresa }, empresa);
        }

        //Método PUT para editar y guardar cambios a una empresa seleccionada
        [HttpPut("empresas/{rutEmpresa}")]
        public IActionResult EditarEmpresa(string rutEmpresa, [FromBody] Cliente empresa)
        {
            var empresaExistente = clientes.Find(c => c.RutEmpresa == rutEmpresa);
            if (empresaExistente == null)
            {
                return NotFound("Empresa no encontrada");
            }
            return NoContent();
        }

        //Método DELETE para eliminar una empresa creada
        [HttpDelete("empresas/{rutEmpresa}")]
        public IActionResult EliminarEmpresa(string rutEmpresa)
        {
            var empresaExistente = clientes.Find(c => c.RutEmpresa == rutEmpresa);
            if (empresaExistente == null)
            {
                return NotFound("Empresa no encontrada");
            }
            clientes.Remove(empresaExistente);
            return NoContent();
        }
        //metodo para calcular Iva
        [HttpPost("calcular-iva")]
        public IActionResult CalcularIva([FromBody] Cliente cliente)
        {
            float montoIva = cliente.MontoVentas * 0.19f; // Suponiendo una tasa de IVA del 19%.
            cliente.MontoIva = montoIva;
            return Ok(cliente);
        }
        //metodo para calcular utilidades de la empresa
        [HttpPost("calcular-utilidades")]
        public IActionResult CalcularUtilidades([FromBody] Cliente cliente)
        {
            float montoIva = cliente.MontoVentas * 0.19f; // Suponiendo una tasa de IVA del 19%.
            float montoUtilidades = cliente.MontoVentas - montoIva;
            cliente.MontoUtilidades = montoUtilidades;
            return Ok(cliente);
        }
    }

