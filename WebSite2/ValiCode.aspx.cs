﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Drawing;
using System.Text;
using System.Web.Services;



public partial class ValiCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DoImg(DoCode(5));
    }
    ///<summary>
    ///生成验证码表达式
    ///</summary>
    ///<param name="num">验证码的长度</param>
    ///<returns>验证表达式</returns>
    private string DoCode(int num)
    {
        //要从这个数组中随机获取字符来生成验证码
        string[] Codesource = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n" };
        string code = "";
        Random rd = new Random();
        for (int i = 0; i < num; i++)
        {
            code += Codesource[rd.Next(0, Codesource.Length)];
        }
        return code;
    }
    private void DoImg(string code)
    {
        Random random = new Random();
        Bitmap mypalette = new Bitmap(80, 20);
        Graphics gh = Graphics.FromImage(mypalette);
        using (gh)
        {
            Rectangle rc = new Rectangle(0, 0, 80, 20);
            gh.FillRectangle(new SolidBrush(Color.White), rc);
            for (int i = 0; i < 1; i++)
            {
                int x1 = random.Next(mypalette.Width);
                int x2 = random.Next(mypalette.Width);
                int y1 = random.Next(mypalette.Height);
                int y2 = random.Next(mypalette.Height);
                //在图片上随机画线条
                gh.DrawLine(new Pen(Color.Blue), x1, y1, x2, y2);

            }
            gh.DrawString(code, new Font("宋体", 16), new SolidBrush(Color.Black), rc);
            for (int i = 1; i < 60; i++)
            {
                int x = random.Next(mypalette.Width);
                int y = random.Next(mypalette.Height);
                //在图片中填充随机颜色
                mypalette.SetPixel(x, y, Color.FromArgb(random.Next()));

            }
            //将生成的图像保存到流中
            mypalette.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            Session["ValiCode"] = code;
        }
        Response.Write(code);

    }
}
