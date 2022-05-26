# PruebasMeli_NET
Contiene el api en .net para insertar y obtener las estadisticas de los adn analizados.
----------------------------------------------------
Realizado por: Sebastian Tobon Carvajal
Correo: sebastiantc19@gmail.com
----------------------------------------------------
La pagina principal del api esta montada en el servidor de pruebas soome con url http://pruebasmeli.somee.com/
Para obtener ayuda en la documentacion del api visitar http://pruebasmeli.somee.com/Help
----------------------------------------------------
Metodos principales: Se pueden consultar por insomnia o por postman.

GET api/estadisticas
	- Descripcion: Desvuelve json con estadisitcas.
	- Parametros de entrada: Ninguno.
	- Ejemplo de respuesta: {"count_mutant_dna": 8,"cont_human_dna": 1}
	
POST api/guardarAdn
	- Descripcion: Inserta cadena de adn en BD.
	- Parametros de entrada: Json.
	- Ejemplo parametros de entrada: {"secuenciaAdn":"humano 4","es_mutante": false}
	- Ejemplo de respuesta: {"response": {"codigo": "0","respuesta": "Ok","mensaje": "El suario ya existe en la bd"},"status": "200","exception": ""}


