using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Lesson12 : MonoBehaviour
{
    private byte[] resultBytes = new byte[1024];
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �첽������ͬ������������
        //ͬ��������
        //�������߼�ִ����Ϻ��ټ���ִ�к���ķ���
        //�첽������
        //�������߼����ܻ�û��ִ����ϣ��ͼ���ִ�к��������

        //�첽�����ı���
        //�����첽�������ж���ʹ�ö��߳�ִ��ĳ�����߼�
        //��Ϊ���ǲ���Ҫ�ȴ��������߼�ִ����ϾͿ��Լ���ִ��������߼���

        //ע�⣺Unity�е�Эͬ�����е�ĳЩ�첽�������е�ʹ�õ��Ƕ��߳��е�ʹ�õ��ǵ������ֲ�ִ��
        //����Эͬ������Իع�Unity�������н���Эͬ����ԭ���֪ʶ��
        #endregion

        #region ֪ʶ��� ����˵���첽����ԭ��
        //������һ���첽����ʱ��������
        //1.�̻߳ص�
        //CountDownAsync(5, ()=> {
        //    print("����ʱ����");
        //});
        //print("�첽ִ�к���߼�");

        //2.async��await ��ȴ��߳�ִ����� ����ִ�к�����߼�
        //��Ե�һ�ַ�ʽ �����ú����ֲ�ִ��
        CountDownAsync(5);
        print("�첽ִ�к���߼�2");
        #endregion

        #region ֪ʶ���� Socket TCPͨ���е��첽������Begin��ͷ������
        //�ص���������IAsyncResult
        //AsyncState �����첽����ʱ����Ĳ��� ��Ҫת��
        //AsyncWaitHandle ����ͬ���ȴ�

        Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //���������
        //BeginAccept
        //EndAccept
        socketTcp.BeginAccept(AcceptCallBack, socketTcp);

        //�ͻ������
        //BeginConnect
        //EndConnect
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        socketTcp.BeginConnect(ipPoint, (result) =>
        {
            Socket s = result.AsyncState as Socket;
            try
            {
                s.EndConnect(result);
                print("���ӳɹ�");
            }
            catch (SocketException e)
            {
                print("���ӳ���" + e.SocketErrorCode + e.Message);
            }

        }, socketTcp);


        //�������ͻ���ͨ��
        //������Ϣ
        //BeginReceive
        //EndReceive
        socketTcp.BeginReceive(resultBytes, 0, resultBytes.Length, SocketFlags.None, ReceiveCallBack, socketTcp);

        //������Ϣ
        //BeginSend
        //EndSend
        byte[] bytes = Encoding.UTF8.GetBytes("1231231231223123123");
        socketTcp.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, (result) =>
        {
            try
            {
                socketTcp.EndSend(result);
                print("���ͳɹ�");
            }
            catch (SocketException e)
            {
                print("���ʹ���" + e.SocketErrorCode + e.Message);
            }
        }, socketTcp);
        #endregion

        #region ֪ʶ���� Socket TCPͨ���е��첽����2��Async��β������
        //�ؼ���������
        //SocketAsyncEventArgs
        //������ΪAsync�첽�����Ĵ���ֵ
        //������Ҫͨ��������һЩ�ؼ������ĸ�ֵ

        //��������
        //AcceptAsync
        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
        e.Completed += (socket, args) =>
        {
            //�����ж��Ƿ�ɹ�
            if (args.SocketError == SocketError.Success)
            {
                //��ȡ����Ŀͻ���socket
                Socket clientSocket = args.AcceptSocket;

                (socket as Socket).AcceptAsync(args);
            }
            else
            {
                print("����ͻ���ʧ��" + args.SocketError);
            }
        };
        socketTcp.AcceptAsync(e);

        //�ͻ���
        //ConnectAsync
        SocketAsyncEventArgs e2 = new SocketAsyncEventArgs();
        e2.Completed += (socket, args) =>
        {
            if (args.SocketError == SocketError.Success)
            {
                //���ӳɹ�
            }
            else
            {
                //����ʧ��
                print(args.SocketError);
            }
        };
        socketTcp.ConnectAsync(e2);

        //����˺Ϳͻ���
        //������Ϣ
        //SendAsync
        SocketAsyncEventArgs e3 = new SocketAsyncEventArgs();
        byte[] bytes2 = Encoding.UTF8.GetBytes("123123�ľ����������������ط־�");
        e3.SetBuffer(bytes2, 0, bytes2.Length);
        e3.Completed += (socket, args) =>
        {
            if (args.SocketError == SocketError.Success)
            {
                print("���ͳɹ�");
            }
            else
            {

            }
        };
        socketTcp.SendAsync(e3);

        //������Ϣ
        //ReceiveAsync
        SocketAsyncEventArgs e4 = new SocketAsyncEventArgs();
        //���ý������ݵ�������ƫ��λ�ã�����
        e4.SetBuffer(new byte[1024 * 1024], 0, 1024 * 1024);
        e4.Completed += (socket, args) =>
        {
            if(args.SocketError == SocketError.Success)
            {
                //��ȡ�洢���������е��ֽ�
                //Buffer������
                //BytesTransferred����ȡ�˶��ٸ��ֽ�
                Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred);

                args.SetBuffer(0, args.Buffer.Length);
                //��������Ϣ �ٽ�����һ��
                (socket as Socket).ReceiveAsync(args);
            }
            else
            {

            }
        };
        socketTcp.ReceiveAsync(e4);
        #endregion

        #region �ܽ�
        //C#������ͨ�� �첽������ ��Ҫ�ṩ�����ַ���
        //1.Begin��ͷ��API
        //�ڲ������̣߳�ͨ���ص���ʽ���ؽ������Ҫ��End��ط��� ���ʹ��

        //2.Async��β��API
        //�ڲ������̣߳�ͨ���ص���ʽ���ؽ��������SocketAsyncEventArgs�������ʹ��
        //���������Ǹ��ӷ���Ľ��в���
        #endregion
    }

    private void AcceptCallBack(IAsyncResult result)
    {
        try
        {
            //��ȡ����Ĳ���
            Socket s = result.AsyncState as Socket;
            //ͨ������EndAccept�Ϳ��Եõ�����Ŀͻ���Socket
            Socket clientSocket = s.EndAccept(result);

            s.BeginAccept(AcceptCallBack, s);
        }
        catch (SocketException e)
        {
            print(e.SocketErrorCode);
        }
    }

    private void ReceiveCallBack(IAsyncResult result)
    {
        try
        {
            Socket s = result.AsyncState as Socket;
            //�������ֵ�����ܵ��˶��ٸ��ֽ�
            int num = s.EndReceive(result);
            //������Ϣ����
            Encoding.UTF8.GetString(resultBytes, 0, num);

            //�һ�Ҫ��������
            s.BeginReceive(resultBytes, 0, resultBytes.Length, SocketFlags.None, ReceiveCallBack, s);
        }
        catch (SocketException e)
        {
            print("������Ϣ������" + e.SocketErrorCode + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountDownAsync(int second, UnityAction callBack)
    {
        Thread t = new Thread(() =>
        {
            while (true)
            {
                print(second);
                Thread.Sleep(1000);
                --second;
                if (second == 0)
                    break;
            }
            callBack?.Invoke();
        });
        t.Start();

        print("��ʼ����ʱ");
    }

    public async void CountDownAsync(int second)
    {
        print("����ʱ��ʼ");

        await Task.Run(() =>
        {
            while (true)
            {
                print(second);
                Thread.Sleep(1000);
                --second;
                if (second == 0)
                    break;
            }
        });

        print("����ʱ����");
    }
}
