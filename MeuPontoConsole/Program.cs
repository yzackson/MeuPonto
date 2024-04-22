using Microsoft.Data.Sqlite;

int response;

Console.WriteLine("Escolha uma opção: [0] sair  [1] Registrar ponto  [2] Ver pontos");
response = int.Parse(Console.ReadLine() ?? "3");

var connection = new SqliteConnection($"Data Source=PontosDB.db;Mode=ReadWriteCreate");

connection.Open();

var command = connection.CreateCommand();

if(response == 1) {
    Console.WriteLine("Insira o tipo: [1] Entrada  [2] Saida Almoco  [3] Retorno Almoco  [4] Saida");
    var tipo = Console.ReadLine() ?? "0";

    Ponto NovoPonto = new Ponto(tipo);
    NovoPonto.ResgistrarPonto(command);

    
    Console.WriteLine("Ponto registrado com sucesso!");
} else if(response == 2) {
    Ponto.ListaDePontos(command);
}
connection.Close();



class Ponto {
    private DateTime datetime;
    private string tipo;
    private string motivo;

    public Ponto(string _tipo) {
        datetime = DateTime.Now;
        tipo = _tipo;
        motivo = "N/A";
    }

    public void ResgistrarPonto(SqliteCommand command) {
        if(HorarioErrado()){
            Console.WriteLine("Você está registrando seu ponto fora do horário programado. Por favor, insira o motivo da alteração: ");
            motivo = Console.ReadLine() ?? motivo;
        }
        
        command.CommandText = "INSERT INTO PONTO VALUES ($DT_REGISTRO, $TIPO, $MOTIVO)";
        command.Parameters.AddWithValue("$DT_REGISTRO", datetime);
        command.Parameters.AddWithValue("$TIPO", tipo);
        command.Parameters.AddWithValue("$MOTIVO", motivo);
        command.ExecuteScalar();
    }

    private bool HorarioErrado() {
        if((datetime.Hour > 8 && tipo == "1") ||
        (((datetime.Hour < 12) || (datetime.Hour >13 && datetime.Minute > 30)) && tipo == "2") ||
        (datetime.Hour > 8 && tipo == "4")) {
            return true;
        }
        return false;
    }

    public static void ListaDePontos(SqliteCommand command) {
        command.CommandText = "SELECT * FROM PONTO";
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string[] columns = new string[3];
            reader.GetValues(columns);
            for(int i=0; i<columns.Length;i++){
                Console.Write(columns[i] + " | ");
            }
            Console.WriteLine();
        }
        Console.ReadLine();
    }
}