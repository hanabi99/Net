using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson28 : MonoBehaviour
{
    public RawImage image;

    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ WWW�������
        //WWW��Unity�ṩ�����Ǽ򵥵ķ�����ҳ����
        //���ǿ���ͨ���������غ��ϴ�һЩ����
        //��ʹ��httpЭ��ʱ��Ĭ�ϵ�����������Get�������ҪPost�ϴ�����Ҫ����½ڿ�ѧϰ��WWWFrom��ʹ��
        //����Ҫ֧�ֵ�Э��
        //1.http://��https:// ���ı�����Э��
        //2.ftp:// �ļ�����Э�飨���������������أ�
        //3.file:// �����ļ�����Э�飬����ʹ�ø�Э���첽���ر����ļ���PC��IOS��Android��֧�֣�
        //���Ǳ��ڿ���Ҫѧϰ����WWW���������ݵ����ػ����

        //ע�⣺
        //1.����һ�����Эͬ����ʹ��
        //2.�����ڽ���Unity�汾�л���ʾ��ʱ�������Կ���ʹ�ã��°汾���书�����Ͻ���UnityWebRequest�֮ࣨ�󽲽⣩
        #endregion

        #region ֪ʶ��� WWW��ĳ��÷����ͱ���
        #region ���÷���
        //1.WWW�����캯�������ڴ���һ��WWW����
        WWW www = new WWW("http://192.168.50.109:8000/Http_Server/ͼƬ1.jpg");
        //2.GetAudioClip�����������ݷ���һ����Ч��ƬAudioClip����
        //www.GetAudioClip()
        //3.LoadImageIntoTexture�������������е�ͼ�����滻���е�һ��Texture2D����
        //Texture2D tex = new Texture2D(100, 100);
        //www.LoadImageIntoTexture(tex);
        //4.LoadFromCacheOrDownload���ӻ������AB����������ð����ڻ������Զ����ش洢�������У��Ա��Ժ�ֱ�Ӵӱ��ػ����м���
        //WWW.LoadFromCacheOrDownload("http://192.168.50.109:8000/Http_Server/test.assetbundle", 1);
        #endregion

        #region ���ñ���
        //1.assetBundle��������ص�������AB��������ͨ���ñ���ֱ�ӻ�ȡ���ؽ��
        //www.assetBundle
        //2.audioClip��������ص���������Ч��Ƭ�ļ�������ͨ���ñ���ֱ�ӻ�ȡ���ؽ��
        //www.GetAudioClip
        //3.bytes�����ֽ��������ʽ��ȡ���ص�������
        //www.bytes
        //4.bytesDownloaded����ȥ�����ص��ֽ���
        //www.bytesDownloaded
        //5.error������һ��������Ϣ����������ڼ���ִ��󣬿���ͨ������ȡ������Ϣ
        //www.error != null
        //6.isDone���ж������Ƿ��Ѿ����
        //www.isDone
        //7.movie��������ص���Ƶ�����Ի�ȡһ��MovieTexture���ͽ��
        //www.GetMovieTexture()
        //8.progress:���ؽ���
        //www.progress
        //9.text��������ص��������ַ��������ַ�������ʽ��������
        //www.text
        //10.texture��������ص�������ͼƬ����Texture2D����ʽ���ؼ��ؽ��
        //www.texture
        #endregion
        #endregion

        #region ֪ʶ���� ����WWW�����첽���ػ�����ļ�
        #region 1.����HTTP�������ϵ�����
        //StartCoroutine(DownLoadHttp());
        #endregion

        #region 2.����FTP�������ϵ����ݣ�FTP������һ��Ҫ֧�������˻���
        //StartCoroutine(DownLoadFtp());
        #endregion

        #region 3.�������ݼ��أ�һ���ƶ�ƽ̨�������ݶ���ʹ�ø÷�ʽ��
        StartCoroutine(DownLoadHttp());
        #endregion
        #endregion

        #region �ܽ�
        //Unity�е�WWW���ʹ��C#�е�Http�������ӵķ���
        //������ʹ��Unity����Ϊ���Ƿ�װ�õ������������ء���������߼�
        #endregion
    }

    IEnumerator DownLoadHttp()
    {
        //1.����WWW����
        WWW www = new WWW("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fi2.hdslb.com%2Fbfs%2Farchive%2F8cc2b9a7868b266800f98d42fc5d257021e75103.jpg&refer=http%3A%2F%2Fi2.hdslb.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1654745686&t=b7691b6e546367610e4331039d1a10ec");

        //2.���ǵȴ����ؽ���
        while (!www.isDone)
        {
            print(www.bytesDownloaded);
            print(www.progress);
            yield return null;
        }

        print(www.bytesDownloaded);
        print(www.progress);

        //3.ʹ�ü��ؽ��������Դ
        if (www.error == null)
        {
            image.texture = www.texture;
        }
        else
            print(www.error);
    }

    IEnumerator DownLoadFtp()
    {
        //1.����WWW����
        WWW www = new WWW("ftp://127.0.0.1/ʵս��ҵ·��.jpg");

        //2.���ǵȴ����ؽ���
        while (!www.isDone)
        {
            print(www.bytesDownloaded);
            print(www.progress);
            yield return null;
        }

        print(www.bytesDownloaded);
        print(www.progress);

        //3.ʹ�ü��ؽ��������Դ
        if (www.error == null)
        {
            image.texture = www.texture;
        }
        else
            print(www.error);
    }

    IEnumerator DownLoadLocal()
    {
        //1.����WWW����
        WWW www = new WWW("file://" + Application.streamingAssetsPath + "/test.png");

        //2.���ǵȴ����ؽ���
        while (!www.isDone)
        {
            print(www.bytesDownloaded);
            print(www.progress);
            yield return null;
        }

        print(www.bytesDownloaded);
        print(www.progress);

        //3.ʹ�ü��ؽ��������Դ
        if (www.error == null)
        {
            image.texture = www.texture;
        }
        else
            print(www.error);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
