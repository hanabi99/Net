using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetAsyncMgr : MonoBehaviour
{
    private static NetAsyncMgr instance;

    public Socket socket;

    public static NetAsyncMgr Instance => instance;

    private byte[] cacheBytes = new byte[1024 * 1024];
    private int cacheNum = 0;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public void Connect(string ip, int port)
    {
        //如果是连接状态 直接返回
        if (socket != null && socket.Connected)
            return;
     
        //连接服务端
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        args.RemoteEndPoint = ipPoint;
        args.Completed += (socket, args) =>
        {
            if (args.SocketError == SocketError.Success)
            {
                print("连接成功");
                SocketAsyncEventArgs receiveArgs = new SocketAsyncEventArgs();
                receiveArgs.SetBuffer(cacheBytes, 0, cacheBytes.Length);
                receiveArgs.Completed += ReceiveCallBack;
                this.socket.ReceiveAsync(receiveArgs);
            }
            else
            {
                print("连接失败" + args.SocketError);
            }
        };
        socket.ConnectAsync(args);
    }

    private void ReceiveCallBack(object obj, SocketAsyncEventArgs args)
    {
        if (args.SocketError == SocketError.Success)
        {
            print(Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred));
            //继续去收消息
            args.SetBuffer(0, args.Buffer.Length);
            if (this.socket != null && socket.Connected)
            {
                socket.ReceiveAsync(args);
            }
        }

    }

    public void Close()
    {
        if (socket != null)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Disconnect(false);
            socket.Close();
            socket = null;
        }
    }
    public void Send(string str)
    {
        if (this.socket != null && socket.Connected)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
            sendArgs.SetBuffer(bytes, 0, bytes.Length);
            sendArgs.Completed += (socket, sendArgs) =>
            {
                if (sendArgs.SocketError != SocketError.Success)
                {
                    print("发送消息失败" + sendArgs.SocketError);
                    Close();
                }

            };
            socket.SendAsync(sendArgs);
        }
        else
        {
            Close();
        }
    }
}
