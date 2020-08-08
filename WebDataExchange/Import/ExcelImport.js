
/**
 * 导入
 * @param {any} $ jquery
 * @param {any} func 回调函数 function(data){}
 * @param {any} compid 扩展构件id
 */
function ImportExcel($, func, compid) {
    $('<div id="table"></div>').appendTo($('body'));
    $('#xxxx').dialog({
        title: '上传文件', //标题
        width: 280, //宽
        height: 240, //高
        align: 'center',
        closed: false, //是否包含关闭按钮
        cache: false, //是否启用缓存
        resizable: true, //是否可以拉伸
        modal: true, //是否模态窗口
        content: '<input type="file" id="excel" class="upload" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-excel"/><br />是否存表<select id="ifSave"><option value="1">是</option><option value="0">否</option></select><br/>sheet页名称<input type="text" id="tableName"/><br/>起始行<input type="text" id="beginRow"/><br/>结束行<input type="text" id="endRow"/>',
        buttons: [{
            text: '上传',
            iconCls: '', //按钮图标
            handler: function () {
                $('#xxxx').dialog('close');
                var formData = new FormData();
                formData.append("excel", document.getElementById("excel").files[0]);
                // var ifSave=document.getElementById("ifSave");
                // var index=ifSave.selectedIndex;
                // var sValue=ifSave.options[index].value;
                var v = $("#ifSave").val();
                formData.append("ifSave", v);
                formData.append("tableName", $("#tableName").val());
                formData.append("compID", compid);
                formData.append("beginRow", $("#beginRow").val());
                formData.append("endRow", $("#endRow").val());
                $.ajax({
                    url: "/cwbase/web/session/WD/Handler/ExcelImportHandler.ashx",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        var data = JSON.parse(result);
                        if (data.status == "success") {
                            $.notify.info("提示", "上传成功<br/>名称为" + data.name + "<br/>");
                            func(data);
                        } else {
                            $.notify.info("提示", "上传失败<br/>原因为" + data.data);
                            func(data);
                        }
                    },
                    beforeSend: function (XHR) {
                        var state = gsp.rtf.context.state();
                        XHR.setRequestHeader("GSPStateCount", "1");
                        XHR.setRequestHeader("GSPState0", escape(state).replace(/\+/g, "%2b"))
                    },
                    error: function (result) {
                        $.notify.info("提示", "上传失败" + result);
                        func(data);
                    }
                });
            }
        }],
        onClose: function () {
        }
    });
}