function Redirect(url) {
    try {
        window.frameElement.setAttribute("src", url);
        //$("#iframe1", window.parent.document).attr("src", url);
    }
    catch (e) {

        if (url.indexOf('SPHostUrl') < 0) {
            var spHostUrl = getParameterByName('SPHostUrl');
            url += "&SPHostUrl=" + spHostUrl;
        }
        location.href = url;
    }
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function submitSuccessFromIFrame(data, reloadElement) {
    if (data.Error) {
        alert(data.Error || data.Message);
        $("#btn_save,form>button[type='submit']").prop("disabled", false);
    }
    else try {
        //перезагружает 1й iframe, если текущее содержимое во 2м
        //window.parent.document.getElementById('iframe1').contentDocument.location.reload(true);
        if (window.self !== window.top) {
            window.parent.document.location.reload(true);
        }
        else if (data.Redirect) document.location.href = data.Redirect;
        else document.location.href = 'about:blank';
    }
    catch (e) {
        //кроссдоменный запрос
        var msg = data.postMessage || { reload: true };
        msg['reloadElement'] = data.ReloadElement | reloadElement;
        window.parent.postMessage(msg, '*');
    }
}
function compareDates(elem1, elem2) {
    var date1 = toDate($(elem1).val());
    var date2 = toDate($(elem2).val());
    var IsValid = true;
    if (date1 > date2) {
        IsValid = false;
    }
    return IsValid;
}

function toDate(dateStr) {
    var date_time = dateStr.split(' ');
    var d = date_time[0].split('.');
    if (date_time.length == 1) { return new Date(d[2], d[1] - 1, d[0]); }
    else {
        var t = date_time[1].split(':');
        return new Date(d[2], d[1] - 1, d[0], t[0], t[1]);
    }
}

//new 17.12.2018
function selectItem(item) {
    $(item).addClass('bg-info').siblings().removeClass('bg-info');
}
function setIndicator(itemId, color) {
    $("#tr_" + itemId).css("border-left", "4px solid " + color + "'");
}

//new 12.07.2018 upadated 24.09.2018
function UTC2Local() {
    var localTimeOffset = new Date().getTimezoneOffset() * 60000;
    $("[UTCvalue]").each(function () {
        var UTCvalue = $(this).attr('UTCvalue');
        try {
            var dateUTCValue = new Date(UTCvalue);
            if (!isNaN(dateUTCValue.getTime())) {
                var dateLocalValue = new Date(dateUTCValue - localTimeOffset).toLocaleString();
                $(this).val(dateLocalValue);
                $(this).text(dateLocalValue);
            }
        }
        catch (e) { console.log(e); }
    });
}

//Храним последние измененные данные формы в sessionStorage
function saveLastFormData(addKeyValue) {
    var LastFormDataJSON = { 'UniqueId': $('#UniqueId').val() };
    $('input,textarea,select').each(function (i, el) {
        if (el.id && el.name) LastFormDataJSON[el.id] = el.value;
    });
    if (addKeyValue) LastFormDataJSON[addKeyValue.id] = addKeyValue.value;
    sessionStorage.setItem('LastFormData', JSON.stringify(LastFormDataJSON));
}

//Извлекаем последние измененные данные формы по элементу UniqueId
function loadLastFormData() {
    var LastFormData = sessionStorage.getItem('LastFormData');
    if (LastFormData) {
        var LastFormDataJSON = JSON.parse(LastFormData);
        if ($('#UniqueId').val() == LastFormDataJSON['UniqueId']) {
            $('input,textarea,select').each(function (i, el) {
                var value = LastFormDataJSON[el.id];
                if (value) $(el).val(value);
            });
        }
    }
}
function getLastFormDataItemValue(itemId) {
    var LastFormData = sessionStorage.getItem('LastFormData');
    if (LastFormData) {
        var LastFormDataJSON = JSON.parse(LastFormData);
        return LastFormDataJSON[itemId];
    }
    return null;
}

function clearLastFormData() {
    sessionStorage.removeItem('LastFormData');
}

function UploadFileAjax(folderGuid, files, spHostUrl, CommId) {
    if (!files) return false;
    if (spHostUrl) spHostUrl = getParameterByName('SPHostUrl');
    var requests = [];
    for (i = 0; i < files.length; i++) {
        var formData = new FormData();
        formData.append("folderGuid", folderGuid);
        formData.append("CommId", CommId);
        formData.append("fileInput", files[i]);
        requests.push(
            $.ajax({
                url: "/Home/UploadFile?SPHostUrl=" + spHostUrl,
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                // Custom XMLHttpRequest
                //xhr: function () {
                //    var myXhr = $.ajaxSettings.xhr();
                //    if (myXhr.upload) {
                //        // For handling the progress of the upload
                //        myXhr.upload.addEventListener('progress', function (e) {
                //            if (e.lengthComputable) {
                //                $('#div_progress>.progress-bar').css('width', (100 * e.loaded / e.total) + '%');
                //            }
                //        }, false);
                //    }
                //    return myXhr;
                //}
                success: function (result) { $('#div_pv_Files').html(result) },
            }));
    }
    $.when.apply(undefined, requests).then(
        function () {
            //location.reload();
        },
        function (r) {
            //$("#div_loader").hide();
            console.log(r);
            alert("При добавлении файла возникла ошибка");
        });
}

function CountComments() { //new 29.03.2018
    var c_Comments = $("#div_Comments .media").length;
    if (c_Comments) $("#chat_c").text(c_Comments);
}

function CountRowTables(tableNames, resultHolder, indicatorLine, indicatorIcon) {
    var count = 0;
    var t_names = tableNames.split(",");
    for (var i = 0; i < t_names.length; i++) {
        count = count + $("#" + t_names[i] + " tbody tr").length;
    }
    if (count != 0) {
        $("#" + resultHolder).text(count);
        $("#" + indicatorLine).attr("class", "m_line");
        $("#" + indicatorIcon).attr("class", "m_icon icomoon icon-circle-checked");
    }
    else {
        $("#" + indicatorLine).attr("class", "m_line_c");
        $("#" + indicatorIcon).attr("class", "m_icon_unchecked");
    }
}

function toggleModalBackground() {
    $(".modal-backdrop").toggle();
}

function createPeoplePickers(items = '.grouped_dropdown,.grouped_multiselect,.select2-here') {
    $(items).select2({
        language: 'ru',
        width: '100%',
        escapeMarkup: markup => markup,
        templateSelection: state => {
            if (!state.id) {
                return state.text;
            }
            var text = state.text.replace(/<\/?[^>]+(>|$)/g, "");
            var $state = $("<span>", { title: text }).html(state.text);
            return $state;
        }
    }).on('select2:open', () => {
        $('input.select2-search__field').prop('placeholder', '  Введите текст для поиска').addClass('form-control');
    })
        .on("select2:close", function () {
            console.log("ddd");
            setTimeout(function () {
                $('.select2-container-active').removeClass('select2-container-active');
                $('.select2-container--focus').removeClass('select2-container--focus');
                $(':focus').blur();
            }, 1);
        });
}

function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}
