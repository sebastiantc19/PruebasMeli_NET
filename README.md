# PruebasMeli_NET
----------------------------------------------------
1.	Descripción del proyecto: Api capaz interactuar con la base de datos que almacena los registros de los ADN analizados, su fecha y su resultado del análisis.

2.	Tecnologías: 
	.NET framework 4.5.
	Lenguaje C#.

3.	Publicación: El desarrollo fue publicado en un servidor somee: http://somee.com/

4.	Preparación de ambiente de desarrollo:
	Clonar el proyecto.
	Abrir Visual Studio y cargar el proyecto.
	Correr el proyecto.
	En caso de necesitar componentes adicionales hacer uso de la terminal de Visual Studio para su instalacion.

5.	Endpoints del api:
	Get: / api/estadisticas
		Descripción: Permite obtener de la base de datos la cantidad total de humanos y mutantes hasta la fecha Respuesta: Retorna objeto JSON.
		Parámetros de la respuesta: 
			count_mutant_dna: Cantidad de mutantes identificados hasta la fecha. (Integer).
			cont_human_dna: Cantidad de humanos identificados hasta la fecha. (Integer).
		Códigos de respuesta:
			200: Se retorna cuando se hizo el análisis correctamente.
			400: Se retorna cuando hay errores en la petición. 
	Post: /api/guardarAdn
		Descripción: Permite insertar en la base de datos el ADN, el resultado y la fecha de procesamiento; asociado a un id autoincrementable. Si una secuencia ya fue insertada previamente el método no la vuelve a insertar en el BD.
		Entrada: Recibe un body tipo JSON.
		Parámetros del body:
			secuenciaAdn: Contiene las bases nitrogenadas de la secuencia de ADN. (String).
			es_mutante: Valor a almacenar con el resultado del análisis y que determina si es mutante o no. (Boolean).
		Headers: Content-Type = application/json.
		Respuesta: Retorna objeto JSON.
		Parámetros de la respuesta: 
			response.codigo: 0 si todo esta ok o 1 si hay un error. (Integer).
			response.respuesta: Mensaje de respuesta de la operación. Contiene un ok o la tipificación del error. (String).
			response.mensaje: Retorna si el usuario pudo ser insertado o no en la base de datos. En caso de que el usuario ya exista no lo inserta nuevamente. (String).
			status: Devuelve el código de respuesta de la operación. (Integer).
			exeption: Retorna la descripción de la posible excepción. (String).
		Códigos de respuesta:
			200: Se retorna cuando se encontró un mutante.
			400: Se retorna cuando hay errores en la petición. 

6.	Url base: http://pruebasmeli.somee.com

7.	Datos del autor:
	Nombre: Sebastián Tobón Carvajal.
	Correo: sebastiantc19@gmail.com
	Contacto: 3207523478.

8. Base de datos:
	Publicación: Servidor http://somee.com/
	Nombre BD: PruebasMeli
	Nombre tabla: ResultadosPruebasAdn
	Columnas tabla: 
		id: int. Llave primaria y autoincrementadle.
		secuencia_adn: varchar(MAX).
		es_mutante: bit.
		fecha: datetime
----------------------------------------------------