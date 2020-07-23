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
					WriteLine("ID : " + jObject["id"].ToString());
					WriteLine("Nome : " + jObject["nome"].ToString());

					var endereco = jObject["endereco"];
					WriteLine("Rua : " + endereco["rua"].ToString());
					WriteLine("Cidade : " + endereco["cidade"].ToString());
					WriteLine("Cep : " + endereco["cep"].ToString());

					JArray arrayExperiencias = (JArray)jObject["experiencias"];

                    if(arrayExperiencias != null)
					{
						WriteLine("Empresas");
						foreach(var item in arrayExperiencias)
                        {
							WriteLine("\tId :" + item["empresaid"]);
							WriteLine("\tEmpresa :" + item["empresanome"].ToString());
                        }
                    }
					WriteLine("Telefone : " + jObject["telefone"].ToString());
					WriteLine("Cargo : " + jObject["cargo"].ToString());
				}
            }
            catch(Exception)
            {
				throw;
            }
		}

		// Adicionando Empresa (CREATE)
		public void AdicionarEmpresa(string arquivoJson)
        {
            WriteLine("Informe o Id da Empresa: ");
			var empresaId = Console.ReadLine();
			WriteLine("Informe o Nome da Empresa: ");
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
				File.WriteAllText(arquivoJson, novoJsonResult);

            }
            catch (Exception ex)
            {
				WriteLine("Erro ao adicionar: " + ex.Message.ToString());
            }
		}

		// Deletando Empresa (DELEATE)
		public void DeletarEmpresa(string arquivoJson)
        {
			var json = File.ReadAllText(arquivoJson);
            try
            {
				var jObject = jObject.Parse(json);
				JArray arrayExperiencias = (JArray)jObject["experiencias"];
				Write("Informe o ID da empresa a deletar: ");
				var empresaId = Convert.ToInt32(Console.ReadLine());

				if(empresaId > 0)
                {
					var nomeEmpresa = string.Empty;
					var empresaADeletar = arrayExperiencias.FirstOrDefault(obj =>
					   obj["empresaid"].Value<int>() == empresaId);

					arrayExperiencias.Remove(empresaADeletar);

					string saida = Newtonsoft.Json.JsonConvert.SerializeObject(jObject,
								   Newtonsoft.Json.Formatting.Indented);

					File.WriteAllText(arquivoJson, saida);

                }
                else
                {
					WriteLine("O ID da empresa é inválido ou não existe, tente novamente!");
					AtualizarEmpresa(arquivoJson);
                }
            }
            catch (Exception)
            {
				throw;
            }
        }
	}

}


