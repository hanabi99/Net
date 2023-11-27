using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPAsync : MonoBehaviour
{
    private byte[] cacheBytes = new byte[512];
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ Socket UDPͨ���е��첽����
        //ͨ��֮ǰ��ѧϰ��UDP�õ���ͨ����ط�����Ҫ����
        //SendTo��ReceiveFrom
        //�����ڽ���UDP�첽ͨ��ʱҲ��Ҫ��Χ�����շ���Ϣ��ط���������
        #endregion

        #region ֪ʶ��� UDPͨ����Begin����첽����
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //BeginSendTo
        byte[] bytes = Encoding.UTF8.GetBytes("123123lkdsajlfjas");
        EndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        socket.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, ipPoint, SendToOver, socket);

        //BeginReceiveFrom
        socket.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref ipPoint, ReceiveFromOver, (socket, ipPoint));
        #endregion

        #region ֪ʶ���� UDPͨ����Async����첽����
        //SendToAsync
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        //����Ҫ���͵����� 
        args.SetBuffer(bytes, 0, bytes.Length);
        //��������¼�
        args.Completed += SendToAsync;
        socket.SendToAsync(args);

        //ReceiveFromAsync
        SocketAsyncEventArgs args2 = new SocketAsyncEventArgs();
        //�������ý�����Ϣ������
        args2.SetBuffer(cacheBytes, 0, cacheBytes.Length);
        args2.Completed += ReceiveFromAsync;
        socket.ReceiveFromAsync(args2);
        #endregion

        #region �ܽ�
        //����ѧϰ��TCP��ص�֪ʶ��
        //����UDP��������ݵ�ѧϰ�ͱ�ü���
        //�����첽ͨ�ŵ�Ψһ���������API��ͬ��ʹ�ù�����һ�µ�
        #endregion
    }

    private void SendToOver(IAsyncResult result)
    {
        try
        {
            Socket s = result.AsyncState as Socket;
            s.EndSendTo(result);
            print("���ͳɹ�");
        }
        catch (SocketException s)
        {
            print("����ʧ��" + s.SocketErrorCode + s.Message);
        }
    }

    private void ReceiveFromOver(IAsyncResult result)
    {
        try
        {
            (Socket s, EndPoint ipPoint) info = ((Socket, EndPoint))result.AsyncState;
            //����ֵ ���ǽ����˶��ٸ� �ֽ���
            int num = info.s.EndReceiveFrom(result, ref info.ipPoint);
            //������Ϣ

            //��������Ϣ �ּ���������Ϣ
            info.s.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref info.ipPoint, ReceiveFromOver, info);
        }
        catch (SocketException s)
        {
            print("������Ϣ������" + s.SocketErrorCode + s.Message);
        }
    }


    private void SendToAsync(object s, SocketAsyncEventArgs args)
    {
        if(args.SocketError == SocketError.Success)
        {
            print("���ͳɹ�");
        }
        else
        {
            print("����ʧ��");
        }
    }

    private void ReceiveFromAsync(object s, SocketAsyncEventArgs args)
    {
        if (args.SocketError == SocketError.Success)
        {
            print("���ճɹ�");
            //�������˶��ٸ��ֽ�
            //args.BytesTransferred
            //����ͨ���������ַ�ʽ��ȡ���յ����ֽ���������
            //args.Buffer
            //cacheBytes
            //������Ϣ

            Socket socket = s as Socket;
            //ֻ��Ҫ���� �ӵڼ���λ�ÿ�ʼ�� �ܽӶ���
            args.SetBuffer(0, cacheBytes.Length);
            socket.ReceiveFromAsync(args);
        }
        else
        {
            print("����ʧ��");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
