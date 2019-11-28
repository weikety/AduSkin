﻿using AduSkin.Controls;
using AduSkin.Controls.Metro;
using AduSkin.Controls.Metro.AduNotice;
using AduSkin.Demo.Models;
using AduSkin.Demo.UserControls;
using AduSkin.Demo.ViewModel;
using AduSkin.Demo.Views;
using AduSkin.Utility.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AduSkin.Demo
{
   public partial class MainWindow : MetroWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         color1.ColorChange += delegate
         {
               // 不要通过XAML来绑定颜色，无法获取到通知
               BorderBrush = color1.CurrentColor.OpaqueSolidColorBrush;
         };

         ts.IsChecked = true;

         exit.Click += delegate { Close(); };
         //this.SizeChanged += delegate { waterfallFlow.Refresh(); };

         #region 富文本框
         rt1.Clear();

         rt1.AddLine("阅读模式");
         rt1.AddLine();
         rt1.AddLine("添加正常内容");
         rt1.AddLine("添加正常内容可点击", delegate { MessageBox.Show("你点击了我！"); });
         rt1.AddLine("添加自定义颜色内容", new RgbaColor(255, 0, 0, 255));
         rt1.AddLine("添加自定义颜色内容可点击", new RgbaColor(255, 0, 0, 255), delegate { MessageBox.Show("你点击了我！"); });

         rt3.Clear();

         rt3.AddLine("内容追加测试（不换行添加）");
         rt3.AddLine("http://www.baidu.com", "http://www.baidu.com");
         rt3.AddLine("中间的间距是Add(\"  \");方法添加的两个空格");
         rt3.AddLine();

         rt3.AddLine("追加正常内容");
         rt3.AddLine();
         rt3.Add("正常1");
         rt3.Add("   ");
         rt3.Add("正常2");
         rt3.Add("   ");
         rt3.Add("正常3");
         rt3.AddLine();

         rt3.AddLine("追加正常内容可点击");
         rt3.AddLine();
         rt3.Add("正常1", delegate { MessageBox.Show("你点击了我！"); });
         rt3.Add("   ");
         rt3.Add("正常2", delegate { MessageBox.Show("你点击了我！"); });
         rt3.Add("   ");
         rt3.Add("正常3", delegate { MessageBox.Show("你点击了我！"); });
         rt3.AddLine();

         rt3.AddLine("追加自定义颜色内容");
         rt3.AddLine();
         rt3.Add("颜色1", new RgbaColor(255, 0, 0, 255));
         rt3.Add("   ");
         rt3.Add("颜色2", new RgbaColor(0, 255, 0, 255));
         rt3.Add("   ");
         rt3.Add("颜色3", new RgbaColor(0, 0, 255, 255));
         rt3.AddLine();

         rt3.AddLine("追加自定义颜色内容可点击");
         rt3.AddLine();
         rt3.Add("颜色1", new RgbaColor(255, 0, 0, 255), delegate { MessageBox.Show("你点击了我！"); });
         rt3.Add("   ");
         rt3.Add("颜色2", new RgbaColor(0, 255, 0, 255), delegate { MessageBox.Show("你点击了我！"); });
         rt3.Add("   ");
         rt3.Add("颜色3", new RgbaColor(0, 0, 255, 255), delegate { MessageBox.Show("你点击了我！"); });
         rt3.AddLine();
         #endregion

         #region 进度条
         DispatcherTimer timer = new DispatcherTimer();
         timer.Tick += delegate
         {
            pb1.Value = pb1.Value + 1 > pb1.Maximum ? 0 : pb1.Value + 1;
            pb2.Value = pb2.Value + 1 > pb2.Maximum ? 0 : pb2.Value + 1;
            pb2.Title = pb2.Hint;
            pb2.Hint = null;
         };
         timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
         timer.Start();
         #endregion

         ContentRendered += delegate
         {
               // 手动加载指定HTML
               //web1.Document = ResObj.GetString(Assembly.GetExecutingAssembly(), "Resources.about.html");

               //// 导航到指定网页
               //web2.Source = new Uri("https://weibo.com/2576515331/profile?topnav=1&wvr=6&is_all=1");
         };

         #region 折叠容器
         
         #endregion

         #region 日期控件
         Calendar1.SelectedDate= Date3.SelectedDate = Date2.SelectedDate = Date1.SelectedDate = DateTime.Now;
         Date4.SelectedDateStart = Date5.SelectedDateStart = Date6.SelectedDateStart = DateTime.Now;
         Date4.SelectedDateEnd = Date5.SelectedDateEnd = Date6.SelectedDateEnd = DateTime.Now.AddDays(10);
         #endregion

         

         

         
      }

      #region Demo
      private void BtnShowAdu_Click(object sender, RoutedEventArgs e)
      {
         new AduVideoDemo().Show();
      }
      private void About_Click(object sender, RoutedEventArgs e)
      {
         AduSkinDemo asd = new AduSkinDemo();
         asd.Show();
      }
      #endregion

      #region 消息弹框
      private ObservableCollection<NoticeInfo> _NoticeList;

      public ObservableCollection<NoticeInfo> NoticeList
      {
         get
         {
            if (_NoticeList == null)
            {
               _NoticeList = new ObservableCollection<NoticeInfo>();
            }
            return _NoticeList;
         }
         set { _NoticeList = value; }
      }
      private Notifiaction _NotifiactionWin;
      public Notifiaction NotifiactionWin
      {
         get
         {
            if (_NotifiactionWin == null)
            {
               _NotifiactionWin = new Notifiaction();
               _NotifiactionWin.Owner = this;
            }
            return _NotifiactionWin;
         }
      }
      private void ErroMsg_Click(object sender, RoutedEventArgs e)
      {
         NotifiactionWin.AddNotifiaction(new NotifiactionModel()
         {
            Title = "这是Error通知标题",
            Content = "这条通知不会自动关闭，需要点击关闭按钮",
            NotifiactionType = EnumPromptType.Error
         });
      }

      private void SuccessMsg_Click(object sender, RoutedEventArgs e)
      {
         NotifiactionWin.AddNotifiaction(new NotifiactionModel()
         {
            Title = "这是Success通知标题",
            Content = "这条通知不会自动关闭，需要点击关闭按钮这条通知不会自动关闭，需要点击关闭按钮这条通知不会自动关闭，需要点击关闭按钮",
            NotifiactionType = EnumPromptType.Success
         });
      }

      private void WarmMsg_Click(object sender, RoutedEventArgs e)
      {
         NotifiactionWin.AddNotifiaction(new NotifiactionModel()
         {
            Title = "这是Warn通知标题",
            Content = "这条通知不会自动关闭，需要点击关闭按钮",
            NotifiactionType = EnumPromptType.Warn
         });
      }

      private void InfoMsg_Click(object sender, RoutedEventArgs e)
      {
         NotifiactionWin.AddNotifiaction(new NotifiactionModel()
         {
            Title = "这是Info通知标题",
            Content = "这条通知不会自动关闭"
         });
      }


      #endregion

      private void UIElement_OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
      {
         var sample = (ControlModel)((Border)sender).DataContext;
         var hvm = (MainViewModel)DataContext;
         hvm.Content = (UserControl)Activator.CreateInstance(sample.Content);
         hvm.Title = sample.Title;
      }
   }
}