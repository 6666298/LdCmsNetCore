(function ($) {
    $(function () {
        var uploader = WebUploader.create({
            auto: true,
            swf: '~/admin/lib/webuploader/0.1.5/Uploader.swf',
            // 文件接收服务端。
            server: '/upload/file',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: {
                id: '#picker',
                multiple: true,
            },
            fileNumLimit: 9,
            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
            resize: false,
            // 只允许选择图片文件。
            accept: {
                title: 'Images',
                extensions: 'gif,jpg,jpeg,bmp,png',
                mimeTypes: 'image/*'
            }
        });
        uploader.on('fileQueued', function (file) {
            console.log(file);
            $list = $("#filelist")
            var $li = $(
                '<li id="' + file.id + '">' +
                '<p class="title">' + file.name + '</p>' +
                '<p class="imgWrap"><img width="100" height="100"/></p>' +
                '<div class="img-del-base">' +
                '    <div class="img-del-bj hide"><a href="javasccript:;" data-id="' + file.id + '">删除</a></div>' +
                '    <div class="up-result hide">成功</div>' +
                '</div>' +
                '</li>'
            );
            var $img = $li.find('img');
            $list.prepend($li);

            // 创建缩略图
            // 如果为非图片文件，可以不用调用此方法。
            // thumbnailWidth x thumbnailHeight 为 100 x 100
            var thumbnailWidth = 100;
            var thumbnailHeight = 100;
            uploader.makeThumb(file, function (error, src) {
                if (error) {
                    $img.replaceWith('<img width="100" height="100" src="~/admin/lib/webuploader/0.1.5/images/bg.png" />');
                    return;
                }
                $img.attr('src', src);
            }, thumbnailWidth, thumbnailHeight);

            var fileQueuedList = $list;
            var fileQueuedQuantity = parseInt(fileQueuedList.attr("data-QueuedQuantity")) + 1;
            fileQueuedList.attr({ "data-QueuedQuantity": fileQueuedQuantity });
            if (fileQueuedQuantity >= 9) {
                $("#picker").hide();
            } 

            $li.find(".imgWrap").on("mouseenter", function () {
                console.log("a1");
                var $this = $(this).parent("li");
                $this.find(".img-del-base").find(".img-del-bj").removeClass("hide");
            });
            $li.find(".img-del-base .img-del-bj").on("mouseleave", function () {
                console.log("a2");
                $(this).addClass("hide");
            });
            $li.find(".img-del-bj").find("a").on("click", function () {
                console.log("a3");
                cancelFile(file);
                $("#uploadSuccessList").find("#" + file.id).remove();
                var fileTotalNum = parseInt($list.attr("data-QueuedQuantity")) - 1;
                $list.attr({ "data-QueuedQuantity": fileTotalNum });
                if ((fileTotalNum) < 9) {
                    $("#picker").show();
                }
                console.log(fileTotalNum);
            });
        });
        // 文件上传过程中创建进度条实时显示。
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = $('#' + file.id).find(".img-del-base"),
                $percent = $li.find('.progress-box .sr-only');
            // 避免重复创建
            if (!$percent.length) {
                $percent = $('<div class="progress-box"><span class="progress-bar radius"><span class="sr-only" style="width:0%"></span></span></div>').appendTo($li).find('.sr-only');
            }
            $li.find(".state").text("上传中");
            $percent.css('width', percentage * 100 + '%');
        });
        // 文件上传成功，给item添加成功class, 用样式标记上传成功。
        uploader.on('uploadSuccess', function (file, json) {
            console.log(json);
            var $this = $('#' + file.id).find('.img-del-base').find(".up-result");
            $this.removeClass("hide");
            $this.text("已上传");
            $("#uploadSuccessList").append("<li id='" + file.id + "'>" + json.data.file.src + "</li>");
        });
        // 文件上传失败，显示上传出错。
        uploader.on('uploadError', function (file, json) {
            var $this = $('#' + file.id).find('.img-del-base').find(".up-result");
            $this.removeClass("hide");
            $this.text("上传出错");
        });
        // 完成上传完了，成功或者失败，先删除进度条。
        uploader.on('uploadComplete', function (file) {
            $('#' + file.id).find('.img-del-base').find(".up-result").fadeOut();
            $('#' + file.id).find('.img-del-base').find(".progress-box").fadeOut();
        });

        function cancelFile(file) {
            console.log("aa4");
            uploader.cancelFile(file);
            var $li = $('#' + file.id);
            $li.off().find('.file-panel').off().end().remove();
        };
    });
})(jQuery);