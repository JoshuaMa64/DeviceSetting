using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

namespace DeviceSetting
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private XDocument xdc;

        private readonly List<XmlConfig> deviceList = new List<XmlConfig>();

        //private readonly List<string> nameList = new List<string>();
        private ObservableCollection<string> nameList = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            // 读取 DeviceConfig.xml ，若根目录下不存在就新建一个
            xdc = !File.Exists("DeviceConfig.xml") ? XmlConfig.Create() : XDocument.Load("DeviceConfig.xml");

            // 使用 Linq 读取 DeviceConfig.xml 获取通讯方式种类并添加至 ListBox
            var query = from item
                in xdc.Descendants("Device")
                select new XmlConfig
                {
                    Id = Convert.ToInt32(item.Attribute("id")?.Value),
                    Name = item.Attribute("Name")?.Value,
                    LibarayName = item.Element("LibarayName")?.Value,
                    ClassName = item.Element("Class")?.Value,
                    EntryPoint = item.Element("EntryPoint")?.Value
                };

             var configs = query.ToList();

            foreach (var i in configs)
            {
                deviceList.Add(i);
                nameList.Add(i.Name);
            }
            LbDevice.ItemsSource = nameList;
        }

        // 退出按钮
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // 每次变换 ListBox 选择项目刷新内容
        private void LbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 刷新已选配置的 TextBlock
            TxtConfig.Inlines.Clear();
            TxtConfig.Inlines.Add(new Bold(new Run("通讯方式：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].Name + "\n"));
            TxtConfig.Inlines.Add(new Bold(new Run("库名：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].LibarayName + "\n"));
            TxtConfig.Inlines.Add(new Bold(new Run("类名：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].ClassName + "\n"));
            TxtConfig.Inlines.Add(new Bold(new Run("入口函数：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].EntryPoint + "\n"));

            // 刷新配置参数的 StackPanel
            MainStack.Children.Clear();
            var testQuery = xdc.Descendants("ParamConfigOption").Elements().Select(i => i.Name).ToList();
            
            foreach (var i in testQuery)
            {
                //var query = xdc.Descendants("ParamConfigOption").Elements(i.ToString()).Select(x => x.Value);
                var query = from item in xdc.Descendants("ParamConfigOption").Elements(i.ToString())
                    select new { param = item.Value };
                Debug.WriteLine(i.ToString());

                var stack = new StackPanel { Orientation = Orientation.Horizontal };
                stack.Children.Add(
                    new TextBlock
                    {
                        Text = i.ToString(),
                        Margin = new Thickness(5)
                    }
                );
                stack.Children.Add(
                    new ComboBox
                    {
                        Width = 100,
                        Margin = new Thickness(5),
                        ItemsSource = query.ToList(),
                        SelectedIndex = 0
                    }
                );
                MainStack.Children.Add(stack);
            }

        }

        // TODO:保存当前配置到 DeviceConfig.xml
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
