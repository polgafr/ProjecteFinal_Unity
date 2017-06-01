using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;

public class RegiSocket : MonoBehaviour {

    public GameObject noti;
	public Text msj;
    public InputField email;
	public InputField usuario;
	public InputField contra;
	

	internal Boolean socketReady = false;
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter salida;
	StreamReader entrada;
	String Host = "localhost";
	Int32 Port = 9999;


	//nuevo codigo
	Socket clientSocket;

	void Start () {
		//iniciar ();
	}
	void Update () {
	}
	// **********************************************
	public void iniciar() {		
		//do {
			try {
			//	mySocket = new TcpClient(Host, Port);
				mySocket = new TcpClient();
				mySocket.Connect(Host,Port);
				theStream = mySocket.GetStream();
			salida = new StreamWriter(theStream,System.Text.Encoding.UTF8);
				entrada = new StreamReader(theStream);
				socketReady = true;
            //noti.text = "Conectado";
            mostrar("Conectado");
			}
			catch (Exception e) {
				Debug.Log("Socket error: " + e);
            //noti.text = "Error de conexion con el servidor";
            StartCoroutine(mostrar("Error de conexion con el servidor"));
			}	
	//	} while(!socketReady);

	}

    IEnumerator mostrar(string mensaje)
    {

        noti.SetActive(true);
        
        msj.text = mensaje;
        yield return new WaitForSeconds(3);
        noti.SetActive(false);

    }

	public void iniciar2() {		
		
		try {
			IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);

			clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			clientSocket.Connect(serverAddress);

		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e);
            //noti.text = "Error de conexion con el servidor";
            StartCoroutine(mostrar("Error de conexion con el servidor"));
        }	
		//	} while(!socketReady);

	}

	public void loguear(){
		iniciar2 ();
		enviar ("1");
		enviar (usuario.text);
		enviar (hash(contra.text));
		String msj = recivir();
        abrir(msj);		
		apagar ();
	}

    public void registrar()
    {
        iniciar2();
        enviar("2");
        enviar(email.text);
        enviar(usuario.text);
        enviar(hash(contra.text));
        String msj = recivir();
        abrir(msj);
        apagar();
    }

    private string hash(String contra)
    {
        String hashString = null;
        byte[] contraBytes = Encoding.UTF8.GetBytes(contra);
        SHA1 sha = new SHA1CryptoServiceProvider();
         
        byte[] hash = sha.ComputeHash(contraBytes);
        hashString = HexStringFromBytes(hash);
        return hashString;
    }

    private static string HexStringFromBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }

    private void abrir(string cadena)
    {
        if (cadena == "true")
        {
            //noti.text = "Correcto";
            StartCoroutine(mostrar("Correcto"));
            SceneManager.LoadScene(0);
        }
        else
        {
            if (cadena == "excorreo")
            {

                StartCoroutine(mostrar("Correo ya existente"));
            }
            else
            {
                if (cadena == "exusuario")
                {

                    StartCoroutine(mostrar("Usuario ya existente"));

                }                
                    
            }
        }
    }

	public void enviar(string cadena) {
		enviar10 (cadena);
	}

	public void enviar10(string cadena){
		// Sending
		string toSend = cadena;
		int toSendLen = System.Text.Encoding.ASCII.GetByteCount(toSend);
		byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(toSend);
		byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
		clientSocket.Send(toSendLenBytes);
		clientSocket.Send(toSendBytes);
	}
		


	public void enviar1(string cadena) {
	//	if (!socketReady)
	//		return;	
		Console.WriteLine(cadena);
		salida.Write(cadena/* + "\r\n"*/);

		salida.Flush();
	}

	public void enviar2(string cadena){
	//	NetworkStream serverStream = mySocket.GetStream();
	//	byte[] outStream = System.Text.Encoding.ASCII.GetBytes(cadena);
		byte[] outStream = 	System.Text.Encoding.UTF8.GetBytes(cadena);

		theStream.Write(outStream, 0, outStream.Length);
		//theStream.Flush;
	}



	public String recivir() {
		string cadena = recivir10 ();
		return cadena;
	}

	public String recivir1() {
		if (!socketReady)
			return "";
		if (theStream.DataAvailable)
			return entrada.ReadLine();
		return "";
	}

	private string recivir10(){
		byte[] rcvLenBytes = new byte[4];
		clientSocket.Receive(rcvLenBytes);
		int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
		byte[] rcvBytes = new byte[rcvLen];
		clientSocket.Receive(rcvBytes);
		String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
		return rcv;
	}

	public void apagar() {
		if (!socketReady)
			return;
		salida.Close();
		entrada.Close();
		mySocket.Close();
		socketReady = false;
	}

	private byte[] cifrar(byte[] bytes){
		return null;
	}

} // end class s_TCP