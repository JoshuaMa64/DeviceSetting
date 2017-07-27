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
        private  ObservableCollection<string> nameList = new ObservableCollection<string>();

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
            //DgParam.ItemsSource = deviceList;
        }

        // 退出按钮
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // 动态展示
        private void LbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtConfig.Inlines.Clear();
            TxtConfig.Inlines.Add(new Bold(new Run("通讯方式：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].Name + "\n"));
            TxtConfig.Inlines.Add(new Bold(new Run("库名：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].LibarayName + "\n"));
            TxtConfig.Inlines.Add(new Bold(new Run("类名：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].ClassName + "\n"));
            TxtConfig.Inlines.Add(new Bold(new Run("入口函数：")));
            TxtConfig.Inlines.Add(new Run(deviceList[LbDevice.SelectedIndex].EntryPoint + "\n"));
        }

        // TODO:保存当前配置到 DeviceConfig.xml
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DgParam_Loaded(object sender, RoutedEventArgs e)
        {
            //var doc = new XmlDocument();
            //doc.Load("DeviceConfig.xml");

            //var xdp = new XmlDataProvider
            //{
            //    Document = doc,
            //    XPath= @"/ROOT/Devices/Device/ParamConfigOption"
            //};

            //DgParam.DataContext = xdp;
            //DgParam.SetBinding(ItemsControl.ItemsSourceProperty, new Binding());

            var xdoc = XDocument.Load("DeviceConfig.xml");
            var query =
                from ele in xdoc.Descendants().Elements("DeviceType").Elements("Type")
                select new
                {
                    type = ele.Value
                };
            foreach (var i in query)
            {
                Debug.WriteLine(i.type);
            }
        }
    }
}
