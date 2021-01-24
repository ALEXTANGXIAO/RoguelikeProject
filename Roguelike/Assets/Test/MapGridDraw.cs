
using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// 挂在主摄像机上
/// </summary>
public class MapGridDraw : MonoBehaviour
{
    /// <summary>
    /// 线的材质
    /// </summary>
    public Material lineMat;

    /// <summary>
    /// 线的颜色
    /// </summary>
    public Color lineColor = new Color(1, 1, 1, 0.5f);

    /// <summary>
    /// 鼠标放在的方格的颜色
    /// </summary>
    public Color mouseOverColor = new Color(0, 1, 0, 1);

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void OnPostRender()
    {
        DrawGrid();
        DrawMouseOver();
    }

    /// <summary>
    /// 把一个世界坐标转换为格子的坐标
    /// </summary>
    /// <param name="pos"></param>
    public static Vector2 WorldPosToCellPos(Vector2 pos)
    {
        int x = (int)(pos.x > 0 ? pos.x + 0.5 : pos.x - 0.5);
        int y = (int)(pos.y > 0 ? pos.y + 0.5 : pos.y - 0.5);
        return new Vector2(x, y);
    }

    /// <summary>
    /// 得到鼠标所在的格子的坐标
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetMouseCellPos()
    {
        return WorldPosToCellPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }


    /// <summary>
    /// 绘制网格
    /// </summary>
    void DrawGrid()
    {
        if (lineMat == null)
        {
            throw new Exception("lineMat is null");
        }
        Vector2 cameraPos = _camera.transform.position;

        //相机的宽高
        int width = _camera.pixelWidth;
        int height = _camera.pixelHeight;

        //视口在世界空间的四个点
        Vector2 leftUp = _camera.ScreenToWorldPoint(new Vector2(0, height));
        Vector2 rightUp = _camera.ScreenToWorldPoint(new Vector2(width, height));
        Vector2 leftDown = _camera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 rightDown = _camera.ScreenToWorldPoint(new Vector3(width, 0));

        //相机视口在世界坐标中的宽高
        float viewWidth = rightUp.x - leftDown.x;
        float viewHeight = rightUp.y - leftDown.y;

        //cell在横轴和纵轴上的个数
        int x = (int)viewWidth + 1;
        int y = (int)viewHeight + 1;

        //x y变为奇数
        x = x / 2 * 2 + 1;
        y = y / 2 * 2 + 1;

        //中间的方块位置
        float centerX = x / 2 + 1;
        float centerY = y / 2 + 1;

        //偏移
        float offsetX = cameraPos.x % 1;
        float offsetY = cameraPos.y % 1;

        //竖线
        for (int i = 0; i <= x; i++)
        {
            //初始位置
            float posX = i - centerX + 0.5f;
            //线跟随摄像机移动
            posX += cameraPos.x;
            //产生偏移
            posX -= offsetX;

            DrawLine(new Vector2(posX, leftUp.y), new Vector2(posX, leftDown.y), lineColor);
        }
        //横线
        for (int j = 0; j <= y; j++)
        {
            //线跟随摄像机移动
            float posY = j - centerY + 0.5f;
            //线跟随摄像机移动
            posY += cameraPos.y;
            //产生偏移
            posY -= offsetY;

            DrawLine(new Vector2(leftUp.x, posY), new Vector2(rightUp.x, posY), lineColor);
        }
    }

    /// <summary>
    /// 绘制鼠标放在某个格子上的效果
    /// </summary>
    void DrawMouseOver()
    {
        DrawSquare(GetMouseCellPos(), mouseOverColor);
    }

    /// <summary>
    /// 画一条线 世界坐标
    /// </summary>
    /// <param name="posA"></param>
    /// <param name="posB"></param>
    /// /// <param name="color"></param>
    void DrawLine(Vector2 posA, Vector2 posB, Color color)
    {
        GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(color);
        GL.Vertex3(posA.x, posA.y, 0);
        GL.Vertex3(posB.x, posB.y, 0);
        GL.End();
    }

    /// <summary>
    /// 画一个方形 世界坐标
    /// </summary>
    /// <param name="center"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="color"></param>
    void DrawSquare(Vector2 center, float width, float height, Color color)
    {
        Vector2 leftUp = new Vector2(center.x - width / 2f, center.y + height / 2f);
        Vector2 rightUp = new Vector2(center.x + width / 2f, center.y + height / 2f);
        Vector2 leftDown = new Vector2(center.x - width / 2f, center.y - height / 2f);
        Vector2 rightDown = new Vector2(center.x + width / 2f, center.y - height / 2f);

        DrawLine(rightUp, leftUp, color);
        DrawLine(leftUp, leftDown, color);
        DrawLine(leftDown, rightDown, color);
        DrawLine(rightDown, rightUp, color);
    }

    /// <summary>
    /// 画一个 1x1 正方形 世界坐标
    /// </summary>
    /// <param name="center"></param>
    /// <param name="color"></param>
    void DrawSquare(Vector2 center, Color color)
    {
        DrawSquare(center, 1, 1, color);
    }
}