using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeAutoCommon.Models;
using WeChatAuto.Components;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 聊天上下文
    /// </summary>
    public class ChatContext
    {
        /// <summary>
        /// 标题对象
        /// 具体请参考:<seealso cref="HeaderInfo"/>
        /// </summary>
        public HeaderInfo TitleInfo { get; set; }
        /// <summary>
        /// 依赖注入提供者，可以通过ServiceProvider从依赖注入容器获取注入的对象.
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// 某微信的主窗口对象，通过MainWindow可以获取其他的POM对象
        /// 具体请参考:<seealso cref="WeChatMainWindow"/>
        /// </summary>
        public WeChatMainWindow MainWindow { get; set; }

        public override string ToString()
        {
            return this.TitleInfo.ToString();
        }
    }
}