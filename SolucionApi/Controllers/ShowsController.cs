using Microsoft.AspNetCore.Mvc;
using SolucionApi.ActionFilters;
using SolucionApi.Dtos;
using SolucionApi.Filters;
using SolucionApi.Services;
using SolucionApi.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SolucionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBaseApi
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ITvMazeService _tvMazeService;

        public ShowsController(IDataBaseService dataBaseService, ITvMazeService tvMazeService)
        {
            _dataBaseService = dataBaseService;
            _tvMazeService = tvMazeService;
        }

        /// <summary>
        /// Obtiene una lista de todos los shows.
        /// </summary>
        /// <returns>Una lista de ShowDto</returns>
        /// <response code="200">Devuelve la lista de shows</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // GET: api/shows
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await HandleRequestAsync(async () =>
            {
                return Ok(await _dataBaseService.GetShowsAsync());
            });
        }

        /// <summary>
        /// Obtiene un show específico por su id.
        /// </summary>
        /// <param name="id">El ID del show</param>
        /// <returns>El ShowDto solicitado</returns>
        /// <response code="200">Devuelve el show solicitado</response>
        /// <response code="404">Si no se encuentra el show con el ID proporcionado</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // GET: api/shows/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await HandleRequestAsync(async () =>
            {

                var show = await _dataBaseService.GetShowAsync(id);
                if (show is null)
                    return NotFound(MessagesControllers.NotFound);

                return Ok(show);
            });
        }

        /// <summary>
        /// Busca shows según los filtros proporcionados.
        /// </summary>
        /// <param name="filter">Criterios de búsqueda</param>
        /// <returns>Una lista de ShowDto que coinciden con los criterios de búsqueda</returns>
        /// <response code="200">Devuelve la lista de shows que coinciden con los criterios de búsqueda</response>
        /// <response code="400">Si los criterios de búsqueda no son válidos</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // GET: api/shows/Search
        [HttpGet("Search")]
        public async Task<IActionResult> GetShows([FromQuery] ShowFilter filter)
        {
            return await HandleRequestAsync(async () =>
            {
                if (!filter.IsValid(out var errorMessage))
                    return BadRequest(errorMessage);

                var shows = await _dataBaseService.GetShowsAsync(filter);

                return Ok(shows);
            });
        }

        /// <summary>
        /// Crea un nuevo show. Requiere autorización.
        /// </summary>
        /// <param name="show">Los detalles del show a crear</param>
        /// <returns>El ShowDto creado</returns>
        /// <response code="201">El show fue creado exitosamente</response>
        /// <response code="400">Error en la solicitud</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // POST: api/shows
        [HttpPost]
        [ApiKeyAuthHeader(true)]
        [ApiKeyAndUserAuth]
        public async Task<IActionResult> Post(ShowDto show)
        {
            return await HandleRequestAsync(async () =>
            {
                var result = await _dataBaseService.SaveShowsAsync(show);
                if (!result.Success)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { id = result.Data?.Id }, result.Data);
            });

        }

        /// <summary>
        /// Actualiza un show existente. Requiere autorización.
        /// </summary>
        /// <param name="show">Los detalles del show a actualizar</param>
        /// <param name="id">El ID del show a actualizar</param>
        /// <returns>Respuesta HTTP</returns>
        /// <response code="204">El show fue actualizado exitosamente</response>
        /// <response code="400">Error en la solicitud</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">No se encontró el show con el ID proporcionado</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // PUT: api/shows/5
        [HttpPut("{id}")]
        [ApiKeyAuthHeader(true)]
        [ApiKeyAndUserAuth]
        public async Task<IActionResult> Put([FromBody]ShowDto show, string id)
        {
            return await HandleRequestAsync(async () =>
            {
                if (id != show.Id)
                    return BadRequest();

                var result = await _dataBaseService.UpdateShowsAsync(show);
                if(result.Success)
                    return NoContent();

                return NotFound(result.Message);
            });
        }

        /// <summary>
        /// Elimina un show existente. Requiere autorización.
        /// </summary>
        /// <param name="id">El ID del show a eliminar</param>
        /// <returns>Respuesta HTTP</returns>
        /// <response code="204">El show fue eliminado exitosamente</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">No se encontró el show con el ID proporcionado</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // DELETE: api/shows/5
        [HttpDelete("{id}")]
        [ApiKeyAuthHeader(true)]
        [ApiKeyAndUserAuth]
        public async Task<IActionResult> Delete(string id)
        {
            return await HandleRequestAsync(async () =>
            {
                var result = await _dataBaseService.DeleteShowAsync(id);
                if (result.Success)
                    return NoContent();

                return NotFound(result.Message);
            });
        }

        /// <summary>
        /// Elimina todos los shows de la base de datos. Requiere autorización.
        /// </summary>
        /// <returns>Respuesta HTTP</returns>
        /// <response code="204">La base de datos fue limpiada exitosamente</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // POST: api/shows/clear
        [HttpPost("clear")]
        [ApiKeyAuthHeader(true)]
        [ApiKeyAndUserAuth]
        public async Task<IActionResult> ClearDataBase()
        {
            return await HandleRequestAsync(async () =>
            {
                await _dataBaseService.ClearDataBase();
                return NoContent();
            });
        }

        /// <summary>
        /// Importa shows desde TvMaze y los guarda en la base de datos. Requiere autorización.
        /// </summary>
        /// <returns>Respuesta HTTP</returns>
        /// <response code="200">Los shows fueron importados y guardados exitosamente</response>
        /// <response code="400">Error en la solicitud</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Si hay un error interno del servidor</response>
        // POST: api/shows/importtvmaze
        [HttpPost("ImportTvMaze")]
        [ApiKeyAuthHeader(true)]
        [ApiKeyAndUserAuth]
        public async Task<IActionResult> SaveShows()
        {
            return await HandleRequestAsync(async () =>
            {
                var result = await SaveShowsToDataBase();
                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        private async Task<ResultValue> SaveShowsToDataBase()
        {
            try
            {
                var shows = await _tvMazeService.GetShowsAsync();
                if (!shows.Any())
                    return new ResultValue { Success = false, Message = MessagesControllers.NotFound };

                return await _dataBaseService.SaveShowImportsAsync(shows);
            }
            catch (Exception)
            {
                return new ResultValue { Success = false, Message = MessagesControllers.Unexpected };
            }
        }

    }
}
