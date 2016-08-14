<%@ Page Language="C#" AutoEventWireup="true"  %>

<div style="padding: 15px; overflow: hidden">
    <form id="ui_daily_dailyaddform" runat="server">
        <table class="tableForm" width="100%">
             <tr>
            <td>
                用户名：
            </td>
            <td>
                <input type="text" name="ui_daily_shopname_add" id="Text1" autocomplete="off" />
            </td>
        </tr>
            <tr>
                <td>
                    上传图片：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
       
        </table>
    </form>
</div>
