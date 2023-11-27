using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class UDPLearn : MonoBehaviour
{
    private void Start()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //�뱾�ص�ַ�������ӣ��󶨣�
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8080);
        socket.Bind(iPEndPoint);
        //����ָ��Ŀ��
        IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
        //������Ϣ
        socket.SendTo(Encoding.UTF8.GetBytes("thcsasdasdasdasdasd"), remoteIpPoint);
        //������Ϣ
        byte[] bytes = new byte[512];
        EndPoint remoteIpPoint2 = new IPEndPoint(IPAddress.Any, 0);
        int length = socket.ReceiveFrom(bytes,ref remoteIpPoint2);//��¼˭�����ҵ�
        print((remoteIpPoint2 as IPEndPoint).Address.ToString() + "����" + Encoding.UTF8.GetString(bytes, 0, length));

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
