using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using static System.Console;

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

		public void AdicionarEmpresa(string arquivoJson)
        {
            Console.WriteLine("Informe o Id da Empresa: ");
			var empresaId = Console.ReadLine();
			Console.WriteLine("Informe o Nome da Empresa: ");
			var nomeEmpresa = Console.ReadLine();

			var novaEmpresaMembro = "{ 'empresaid': " + empresaId + ", 'empresanome': '" + nomeEmpresa + "'}";
            try
            {
				var json = File.ReadAllText(arquivoJson);
				var jsonObj = JObject.Parse(json);
				var arrayExperiencias = jsonObj.GetValue("experiencias") as JArray;

				var novaEmpresa = JObject.Parse(novaEmpresaMembro);
				arrayExperiencias.Add(novaEmpresa);

				jsonObj["experiencias"] = arrayExperiencias;
				string novoJsonResult = Newtonsoft.Json.JsonCovert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            }
            catch (Exception)
            {
				throw;
            }

		}
	}

}


