using System;
using System.IO;
using Console;

namespace CRUD_Json
{
	public class JsonCrud
	{
		// Ler Dados do Usuário (READ)
		public void DetalhesdoUsuario(string arquivoJson)
		{
			var json = File.ReadAllText(arquivoJson);
            try
            {
				var jObject = jObject.Parse(json);

				if(jObject != null)
                {
					Console.WriteLine("ID : " + jObject["id"].ToString());
					Console.WriteLine("Nome : " + jObject["nome"].ToString());

					var endereco = jObject["endereco"];
					Console.WriteLine("Rua : " + endereco["rua"].ToString());
					Console.WriteLine("Cidade : " + endereco["cidade"].ToString());
					Console.WriteLine("Cep : " + endereco["cep"].ToString());

					JArray arrayExperiencias = (JArray)jObject["experiencias"];

                    if(arrayExperiencias != null)
					{
						Console.WriteLine("Empresas");
						foreach(var item in arrayExperiencias)
                        {
							Console.WriteLine("\tId :" + item["empresaid"]);
							Console.WriteLine("\tEmpresa :" + item["empresanome"].ToString());
                        }
                    }
					Console.WriteLine("Telefone : " + jObject["telefone"].ToString());
					Console.WriteLine("Cargo : " + jObject["cargo"].ToString());
				}
            }
            catch(Exception)
            {
				throw;
            }
		}
	}

}


