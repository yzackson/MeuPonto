using Microsoft.Data.Sqlite;

int response;
DateTime datetime;
string tipo;
string motivo = "N/A";

do {
    Console.WriteLine("Escolha uma opção: [0] sair  [1] escrever");
    response = int.Parse(Console.ReadLine() ?? "3");

    using (var connection = new SqliteConnection("Data Source=PontosDB.db;Mode=ReadWriteCreate"))
    {
        connection.Open();

        var command = connection.CreateCommand();
        if(response == 1) {
            datetime = DateTime.Now;
            Console.WriteLine("Insira o tipo: [1] Entrada  [2] Saida Almoco  [3] Retorno Almoco  [4] Saida");
            tipo = Console.ReadLine() ?? "0";
            switch (tipo){
                case "1":
                    if(datetime.Hour > 8 ) {
                        Console.WriteLine("Você está chegando atrasdo. Insira o motivo: ");
                        motivo = Console.ReadLine() ?? "0";
                    }
                    tipo = "Entrada";
                    break;
                
                case "2":
                    if((datetime.Hour < 12) || (datetime.Hour >13 && datetime.Minute > 30) ) {
                        Console.WriteLine("Você está saindo para almoçar fora do seu horário. Insira o motivo: ");
                        motivo = Console.ReadLine() ?? "0";
                    }
                    tipo = "Saida Almoco";
                    break;
                
                case "3":
                //Para futura implantação: Verificar o tempo de duração do almoço é maior ou menos que 1h12
                    /*if(datetime.Hour > 8 ) {
                        Console.WriteLine("Você está chegando atrasdo. Insira o motivo: ");
                        motivo = Console.ReadLine() ?? "0";
                    }*/
                    tipo = "Retorno Almoco";
                    break;
                
                case "4":
                    if(datetime.Hour > 8 ) {
                        Console.WriteLine("Você está saindo fora do seu horário. Insira o motivo: ");
                        motivo = Console.ReadLine() ?? "0";
                    }
                    tipo = "Saida";
                    break;
            }
            
            

            command.CommandText = "INSERT INTO PONTO VALUES ($DT_REGISTRO, $TIPO, $MOTIVO)";
            command.Parameters.AddWithValue("$DT_REGISTRO", datetime);
            command.Parameters.AddWithValue("$TIPO", tipo);
            command.Parameters.AddWithValue("$MOTIVO", motivo);

            command.ExecuteScalar();
        } else if(response == 2) {
            command.CommandText = "SELECT * FROM PONTO";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var name = reader.GetString(0);

                    Console.WriteLine($"Hello, {name}!");
                }
            }
        }
        connection.Close();
    }
} while (response != 0);
