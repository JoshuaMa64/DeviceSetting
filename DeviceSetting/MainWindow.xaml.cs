using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        private ObservableCollection<string> nameList = new ObservableCollection<string>();
        private TextBlock TbInfo = new TextBlock {Margin=new Thickness(5), Text = "设置成功保存至 DeviceConfig.xml" };

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
            var currentDevice = deviceList[LbDevice.SelectedIndex].Name;
            // LINQ 查询 XML 获取选中项可配置的参数列表
            var paramsQuery = from i in xdc.Descendants("ParamConfigOption").Elements()
                              where (string) i.Parent.Parent.Attribute("Name") == currentDevice
                              select i.Name;
            var paramsQueryList = paramsQuery.ToList();

            // 获取预设选项
            var queryParam = from item in xdc.Descendants("Param").Elements()
                where (string)item.Parent.Parent.Attribute("Name") == currentDevice
                select new { item.Value };
            var paramList = queryParam.Select(a => a.Value).ToList();

            // 根据参数列表生成 StackPanel 内的界面元素
            for (int i = 0; i < paramsQueryList.Count(); i++)
            {
                var paramName = paramsQueryList[i].ToString();
                // 分别获取不同项目下的可选择项
                var queryOption = from item in xdc.Descendants("ParamConfigOption").Elements(paramName).Elements()
                            select item.Value;
                var optionList = queryOption.ToList();
                var stack = new StackPanel { Orientation = Orientation.Horizontal };
                stack.Children.Add(
                    new TextBlock
                    {
                        Text = paramName,
                        Margin = new Thickness(5),
                        Width = 80
                    }
                );
                stack.Children.Add(
                    new ComboBox
                    {
                        Width = 100,
                        Margin = new Thickness(5),
                        ItemsSource = optionList,
                        SelectedIndex = optionList.IndexOf(paramList[i])
                    }
                );
                MainStack.Children.Add(stack);
            }
        }

        // TODO:保存当前配置到 DeviceConfig.xml
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!MainStack.Children.Contains(TbInfo))
            {
                MainStack.Children.Add(TbInfo);
            }
        }

        // 读取默认使用设备编号
        private void LbDevice_Loaded(object sender, RoutedEventArgs e)
        {
            LbDevice.SelectedIndex = (int) xdc.Root.Element("CurrentDevice").Attribute("id") - 1;
        }
    }
}
