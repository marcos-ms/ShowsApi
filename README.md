# Shows API

Cliente de windows forms en el repositorio [ShowsApiCliente ](SolucionApi/images/ArquitecturaShows.png)

---

## <u>Diagrama de Arquitectura de la API de Shows</u>
![](D:\Users\marco\Pictures\ArquitecturaShows.png)

La API de Shows ha sido diseñada utilizando una arquitectura por capas para facilitar el mantenimiento, la escalabilidad y la separación de responsabilidades. A continuación se describe brevemente la función de cada módulo:

#### 1. Shows API

- **Descripción**: Punto de entrada principal para todas las solicitudes de los clientes (aplicaciones web, móviles, etc.).
- **Función**: Gestiona las operaciones CRUD y las importaciones de datos desde la API de TvMaze.

#### 2. DataBase Service

- **Descripción**: Capa de servicio que interactúa con la base de datos.
- **Función**: Realiza operaciones CRUD sobre los datos de los shows en la base de datos SQL.

#### 3. TvMaze Service

- **Descripción**: Capa de servicio que interactúa con la API externa de TvMaze.
- **Función**: Importa y sincroniza datos de shows desde TvMaze con la base de datos local.

#### 4. Base de Datos SQL

- **Descripción**: Almacenamiento de datos relacional.
- **Función**: Guarda toda la información sobre los shows, gestionada por el DataBase Service.

#### 5. TvMaze API

- **Descripción**: API externa que proporciona datos de shows.
- **Función**: Fuente de datos utilizada por el TvMaze Service para obtener información sobre los shows.

### Resumen

Esta arquitectura por capas asegura una clara separación de responsabilidades, lo que mejora la mantenibilidad y escalabilidad del sistema. Cada módulo tiene una función específica, permitiendo una gestión eficiente y un desarrollo modular.



---



## <u>Puesta en Marcha de la Aplicación</u>

Para poner en marcha esta aplicación ASP.NET Core con Entity Framework Core, sigue estos pasos para configurar y actualizar la base de datos.

### Requisitos Previos

- .NET SDK
- SQL Server u otro proveedor de bases de datos compatible
- Visual Studio o Visual Studio Code (opcional, pero recomendado)

### Pasos para Configurar y Actualizar la Base de Datos

#### 1. Configurar la Cadena de Conexión

Asegúrate de que la cadena de conexión a la base de datos esté configurada correctamente en el archivo `appsettings.json`.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SolucionApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
#### 2. Crear la Migración Inicial
Abre una terminal o consola de comandos y navega hasta el directorio raíz de tu proyecto. Luego, ejecuta el siguiente comando para crear la migración inicial:
```
dotnet ef migrations add InitialCreate
```

#### 3. Aplicar las Migraciones y Actualizar la Base de Datos
Después de crear la migración inicial, aplica las migraciones y actualiza la base de datos ejecutando el siguiente comando:
```
dotnet ef database update
```
#### 4. Verificar la Base de Datos
Una vez que la base de datos se ha actualizado, verifica que se hayan creado las tablas y otras estructuras necesarias en la base de datos. Puedes hacerlo utilizando una herramienta de gestión de bases de datos como SQL Server Management Studio (SSMS).

#### 5. Ejecutar la Aplicación
Finalmente, ejecuta la aplicación para asegurarte de que todo esté configurado correctamente. Puedes hacerlo desde Visual Studio, Visual Studio Code, o desde la terminal con el siguiente comando:
```
dotnet run
```

---



## <u>Configuración de la API</u>

La configuración de la API se especifica en el archivo `appsettings.json`. A continuación se describe cada una de las secciones importantes del archivo de configuración.

#### ServicesApiUrls

Esta sección define las URLs de las APIs externas que la aplicación consume.

```json
"ServicesApiUrls": {
  "TvMazeApi": "http://api.tvmaze.com/"
}
```

#### User-ApiKey

Esta sección define las claves API para los usuarios que están autorizados a interactuar con la API. *(usuario: clave)*

```json
"User-ApiKey": {
  "user1@correo.es": "CLAVE_SEGURA_1",
  "user2@correo.es": "CLAVE_SEGURA_2",
  "user3@correo.es": "CLAVE_SEGURA_3"
}
```



#### Swagger

Esta sección configura la documentación generada automáticamente para la API utilizando Swagger.

```json
"Swagger": {
  "title": "SHOWS",
  "description": "API que importa programas de TvMaze, la guarda en una base de datos y hace CRUD sobre ella",
  "license": {
    "name": "MIT"
  },
  "contact": {
    "name": "Marcos Martínez",
    "email": "mmartinez66@hotmail.com",
    "url": "https://marcosmartinez.nom.es/"
  }
}
```

Ejemplo Completo de `appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SolucionApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "ServicesApiUrls": {
    "TvMazeApi": "http://api.tvmaze.com/"
  },
  "User-ApiKey": {
    "user1@correo.es": "CLAVE_SEGURA_1",
    "user2@correo.es": "CLAVE_SEGURA_2",
    "user3@correo.es": "CLAVE_SEGURA_3"
  },
  "Swagger": {
    "title": "SHOWS",
    "description": "API que importa programas de TvMaze, la guarda en una base de datos y hace CRUD sobre ella",
    "license": {
      "name": "MIT"
    },
    "contact": {
      "name": "Marcos Martínez",
      "email": "mmartinez66@hotmail.com",
      "url": "https://marcosmartinez.nom.es/"
    }
  }
}
```

## Observaciones

#### Autenticación y Seguridad

- **Filtros de Autenticación**: Hemos utilizado un filtro en los endpoints donde se necesita autenticación mediante los headers `Shows-User` y `Shows-ApiKey`.
- **Middleware de Validación**: La validación de estos encabezados se realiza mediante un middleware para garantizar que todas las solicitudes autenticadas incluyan los encabezados necesarios.

#### Generación de IDs en la Base de Datos

Para evitar conflictos con los IDs que se importan desde la API de TvMaze y para permitir la realización de operaciones CRUD en la base de datos sin problemas, se ha decidido que los IDs (claves primarias) en la base de datos sean de tipo texto. Esto asegura que los nuevos IDs no tienen que seguir un orden numérico y se pueden generar de manera única y segura.



---



## <u>Endpoints</u>

### GET /api/shows
Obtiene una lista de todos los shows.

**Headers:**

Ninguno

**Responses:**

- `200 OK`: Devuelve la lista de shows.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### GET /api/shows/{id}
Obtiene un show específico por su id.

**Headers:**

Ninguno

**Parameters:**

- `id` (string, required): El ID del show.

**Responses:**

- `200 OK`: Devuelve el show solicitado.
- `404 Not Found`: Si no se encuentra el show con el ID proporcionado.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### GET /api/shows/Search
Busca shows según los filtros proporcionados.

**Headers:**

Ninguno

**Parameters (query):**

- `filter` (ShowFilter, required): Criterios de búsqueda.

**Responses:**

- `200 OK`: Devuelve la lista de shows que coinciden con los criterios de búsqueda.
- `400 Bad Request`: Si los criterios de búsqueda no son válidos.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### POST /api/shows
Crea un nuevo show. **Requiere autorización.**

**Headers:**

- `Shows-ApiKey` (string, required): API Key para la autenticación.
- `Shows-User` (string, required): Usuario para la autenticación.

**Body:**

- `show` (ShowDto, required): Los detalles del show a crear.

**Responses:**

- `201 Created`: El show fue creado exitosamente.
- `400 Bad Request`: Error en la solicitud.
- `401 Unauthorized`: No autorizado.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### PUT /api/shows/{id}
Actualiza un show existente. **Requiere autorización.**

**Headers:**

- `Shows-ApiKey` (string, required): API Key para la autenticación.
- `Shows-User` (string, required): Usuario para la autenticación.

**Parameters:**

- `id` (string, required): El ID del show a actualizar.

**Body:**

- `show` (ShowDto, required): Los detalles del show a actualizar.

**Responses:**

- `204 No Content`: El show fue actualizado exitosamente.
- `400 Bad Request`: Error en la solicitud.
- `401 Unauthorized`: No autorizado.
- `404 Not Found`: No se encontró el show con el ID proporcionado.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### DELETE /api/shows/{id}
Elimina un show existente. **Requiere autorización.**

**Headers:**

- `Shows-ApiKey` (string, required): API Key para la autenticación.
- `Shows-User` (string, required): Usuario para la autenticación.

**Parameters:**

- `id` (string, required): El ID del show a eliminar.

**Responses:**

- `204 No Content`: El show fue eliminado exitosamente.
- `401 Unauthorized`: No autorizado.
- `404 Not Found`: No se encontró el show con el ID proporcionado.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### POST /api/shows/clear
Elimina todos los shows de la base de datos. **Requiere autorización.**

**Headers:**

- `Shows-ApiKey` (string, required): API Key para la autenticación.
- `Shows-User` (string, required): Usuario para la autenticación.

**Responses:**

- `204 No Content`: La base de datos fue limpiada exitosamente.
- `401 Unauthorized`: No autorizado.
- `500 Internal Server Error`: Si hay un error interno del servidor.

---

### POST /api/shows/ImportTvMaze
Importa shows desde TvMaze y los guarda en la base de datos. **Requiere autorización.**

**Headers:**

- `Shows-ApiKey` (string, required): API Key para la autenticación.
- `Shows-User` (string, required): Usuario para la autenticación.

**Responses:**

- `200 OK`: Los shows fueron importados y guardados exitosamente.
- `400 Bad Request`: Error en la solicitud.
- `401 Unauthorized`: No autorizado.
- `500 Internal Server Error`: Si hay un error interno del servidor.
