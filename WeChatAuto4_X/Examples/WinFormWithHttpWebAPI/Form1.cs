using System.Text.Json;
using WeChatAuto.Client.Api;
using WeChatAuto.Client.Client;
using WeChatAuto.Client.Model;

namespace WinFormWithHttpWebAPI;

public partial class Form1 : Form
{
    // 服务端 App.json 默认端口为 5000；如果改过端口，请同步修改这里。
    private static readonly Configuration Configuration = new()
    {
        BasePath = "http://localhost:5000",
    };

    private readonly GroupsApi _groupsApi = new(Configuration);
    private readonly MessagesApi _messagesApi = new(Configuration);

    public Form1()
    {
        InitializeComponent();
    }

    private async void Button1_Click(object? sender, EventArgs e)
    {
        try
        {
            string clientId = textBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(clientId))
            {
                MessageBox.Show(this, "请先填写客户端ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string groupName = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show(this, "请先填写群名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 同步接口放在后台线程，避免获取群成员时卡住界面。
            ApiResponse<Object> response = await Task.Run(
                () => _groupsApi.GroupsGetMembersWithHttpInfo(clientId, groupName));

            // RawContent 是服务端返回的原始 JSON，例如 ["wxid_xxx", "wxid_yyy"]。
            string? result = response.RawContent;

            listBox1.Items.Clear();
            List<string>? members = string.IsNullOrWhiteSpace(result)
                ? null
                : JsonSerializer.Deserialize<List<string>>(result);

            if (members != null)
            {
                foreach (string member in members)
                {
                    listBox1.Items.Add(member);
                }
            }

            MessageBox.Show(this, $"共 {listBox1.Items.Count} 个好友", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "获取群成员失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void Button2_Click(object? sender, EventArgs e)
    {
        try
        {
            string clientId = textBox5.Text.Trim();
            if (string.IsNullOrWhiteSpace(clientId))
            {
                MessageBox.Show(this, "请先填写客户端ID。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = textBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show(this, "请先填写消息内容。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // who 为空时发送给当前焦点聊天窗口。
            string? who = string.IsNullOrWhiteSpace(textBox2.Text) ? null : textBox2.Text.Trim();

            await _messagesApi.MessagesSendAsync(clientId, new SendMessageRequest
            {
                Who = who!,
                Message = message,
            });

            MessageBox.Show(this, "消息已发送。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "发送消息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
