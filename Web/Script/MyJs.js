/// <reference path="jquery-2.2.4.min.js" />
/// <reference path="bootstrap-3.3.7-dist/js/bootstrap.js" />
/// <reference path="jquery-ui.min.js" />
/// <reference path="Chart.js-2.7.2/dist/Chart.bundle.js" />
/// <reference path="Chart.js-2.7.2/dist/Chart.js" />
/// <reference path="JsonExportExcel.min.js" />

//变量声明区
var map;
var zoom = 6;
var points;
var Menustate = false;
var ShowFarmState = false;      //是否已经显示农田
var IsEnteringDataLegal = false; //输入数据是否合法
var FarmInfoCountyId;
var isLog = false;              //判断用户是否登录
var markers = null;          //聚合的标记   
 var arrayObj = null ;            //标记数组 
 
$(function () {

    setTimeout(function(){
        $(".input-group ").css("float", "left");
    }, 3000);

    //设置map的高度
    boxheight();
    //窗口或框架被调整大小时执行
    window.onresize = boxheight; 
    map = new T.Map('mapDiv');
    map.centerAndZoom(new T.LngLat(102.54019600044, 31.16666400034), zoom);
    //添加放大缩小按钮，比例尺按钮
    addControl();
    PaintEdge();
    //为模态对话框添加拖拽
    $(".modal").draggable();
    //禁止模态对话框的半透明背景滚动
    //$(".modal").css("overflow", "hidden"); 
    $(".FarmInfoMenuDiv").hide();
    $("#ShowFarmInfodiv1").show();
    //----------------------------------------------------------------显示农田信息开始
    $("#ShowFarmBtn").click(function(){
     $('[data-toggle="offcanvas"]').click();
        if(ShowFarmState == true){
            ShowFarmState = false;
            $("#ShowFarmSpan").text('显示农田');
            //删除标记
            var AllOverlays = map.getOverlays();
            $.each(AllOverlays, function(AllOverlays_Index, item){
                if(item.getType() == 2){
                    map.removeOverLay(item);
                }   
            });
            //如果存在聚合的标记，则删除
            if(arrayObj!=null){
                //删除聚合标记
                if(markers.removeMarkers(arrayObj)){
                    //alert("删除标记成功");
                    arrayObj==null;
                }
            }
        }
        else{
            ShowFarmState = true;
            ShowFarmState = true;
            $("#ShowFarmSpan").text('隐藏农田');
            var data_info = [];
            $.ajax({
                url:"../Ashx/LoadBriefInformation.ashx",
                type:"Post",
                dataType:"Json",
                success:function(BackData){
                    //将数据装入二位数组中
                    for (var i = 0; i < BackData.length; i++) {
                        data_info[i] = [];  //js中二维数组必须进行重复的声明，否则会undefind  
                        data_info[i][0] = BackData[i].Lon;
                        data_info[i][1] = BackData[i].Lat;
                        data_info[i][2] = "户主姓名:" + BackData[i].householaer_name + "<br>" + "联系电话:" + BackData[i].Phone_number + "<br>" + "采样点:" + BackData[i].sample_name;
                        data_info[i][3] = BackData[i].idNum;
                    }

                     arrayObj = new Array();
                    //添加标记
                    for (var j = 0; j < data_info.length; j++) {
                         // 创建标注
                        var marker = new T.Marker(new T.LngLat(data_info[j][0], data_info[j][1])); 
                         arrayObj.push(marker);
                        //获取标记文本
                        var content = data_info[j][2];
                        // 将标注添加到地图中
                        map.addOverLay(marker);    
                        //注册标记的鼠标触摸,移开事件           
                        addClickHandler(content, marker, data_info[j][3]);
                    }
                     markers = new T.MarkerClusterer(map, {markers: arrayObj});
                }
            });
        }
    });
    //删除
    $("#ShowInfoDeleteBtn").click(function(){
        //判断是否登录
        if(isLog==false){
           swal({ 
                title: "请先登录",
                type: "warning",
                timer:1500
                });
           openLoginModal();
        }else{
            check(function(){
                 var tableid = $("#hiddenIdInMarkeWindow").val();
            $.ajax({
                    url:"../Ashx/DeleteData.ashx",
                    type:"POST",
                    data: {Id:tableid},
                    success:function(Backdata){
                        if(Backdata == "ok"){
                           swal({ title: "删除成功！",
                            type: "success",
                            timer:1500
                              });
                            $("#ShowFarmBtn").click();
                            $("#ShowFarmBtn").click();
                            $("#ShowInfoCloseBtn").click();
                        }
                        else{
                            swal({ title: "删除失败！",
                            type: "error",
                            timer:1500
                        });
                        }
                    }
                });
            });
        }
    });
    //保存
    $("#ShowInfoConFirmSubmitBtn").click(function(){
        //判断是否登录
        if(isLog==false){
           swal({ 
                title: "请先登录",
                type: "warning",
                timer:1500
                });
           openLoginModal();
        }else{
             if(IsEnteringDataLegal){
                 save(function(){
             //将表单整体序列化成一个数组提交到后台
            var postDataInMarkerForm = $("#dataInMarkerForm").serializeArray();
            $.ajax({
                    url:"../Ashx/ProcessUpdateAllData.ashx",
                    type:"POST",
                    data: postDataInMarkerForm,
                    success:function(Backdata){
                        if(Backdata == "ok"){
                             swal({ title: "保存成功！",
                            type: "success",
                            timer:1500
                              });
                            //连续调用两次click相当于刷新标记
                             $("#ShowFarmBtn").click();
                            $("#ShowFarmBtn").click();
                            $("#ShowInfoCloseBtn").click();
                             
                        }
                        else{
                           swal({ title: "保存失败！",
                            type: "success",
                            timer:1500
                              });
                        }
                    }
                });
        });
                }else{
                 swal({ 
                     title: "请检查数据格式是否正确！",
                    type: "warning",
                    timer:1500
                     });
                }
         }
    });

    //注册信息点触碰、移开、点击事件 
    // content 标记的文字信息  
    //marker 标记对象 
    //dataId 标记对象对应的id
    function addClickHandler(content, marker,dataId) {
        //鼠标触碰事件
        marker.addEventListener("mouseover", function (e) {
            openInfo(content, e);
        }
        );
        //鼠标移开事件
        marker.addEventListener("mouseout", function (e) {
            closeInfo();
        }
        );
        //鼠标单击事件
        marker.addEventListener("click", function (e) {
            clickOpenWindow(dataId);
            
        }
        );
    }
    //----------------------------------------------------------------显示农田信息结束

    //----------------------------------------------------------------农田详细管理开始
    //注册点击此菜单按钮事件
    $("#FarmManagementBtn").click(function(){
     //点击"农田详情管理"时，将IsEnteringDataLegal设置为true
        IsEnteringDataLegal= true;
     $('[data-toggle="offcanvas"]').click();
        //清空窗体数据
        ClearThisWindowData();  
        //将展示图片的窗口恢复原状
        RestitutionShowWind("DetailOl","DetailImgDiv","DetailImgOutDiv","DetailLI","DetailImgNearDiv","DetailDefaultImg");
        //点击按钮时加载tree结构的数据
        getTree();
        $("#divShowInfor").modal('show');
       
    });
    //删除
    $("#FarmDetailDeleteBtn").click(function(){
        //判断是否登录
        if(isLog==false){
           swal({ 
                title: "请先登录",
                type: "warning",
                timer:1500
                });
           openLoginModal();
        }else{
        //获取隐藏域id
         var hiddenId = $("#hiddenIdInMarkeWindow1").val();
         if (hiddenId.length<=0) {
           //表示没有选中节点
           swal({ 
                    title: "请先选中数据！",
                     type: "error",
                     timer:1500
                        });
        }else{
        //调用弹出框
        check(function(){
               var tableid = $("#hiddenIdInMarkeWindow1").val();
            $.ajax({
                    url:"../Ashx/DeleteData.ashx",
                    type:"POST",
                    data: {Id:tableid},
                    success:function(Backdata){
                        if(Backdata == "ok"){
                            swal({ title: "删除成功！",
                                type: "success",
                                timer:1500
                              });
                            //删除后，将重新刷新树状结构
                             getTree();
                            //连续调用两次click相当于刷新标记
                             $("#ShowFarmBtn").click();
                            $("#ShowFarmBtn").click();

                             //清空窗体数据
                            ClearThisWindowData();  
                            //将展示图片的窗口恢复原状
                            RestitutionShowWind("DetailOl","DetailImgDiv","DetailImgOutDiv","DetailLI","DetailImgNearDiv","DetailDefaultImg");
                        }
                        else{
                           swal({ title: "删除失败！",
                            type: "error",
                            timer:1500
                        });
                        }
                    }
            });
        });
        }
       }
    });
     //保存
    $("#FarmDetailSaveBtn").click(function(){
        //判断是否登录
        if(isLog==false){
           swal({ 
                title: "请先登录",
                type: "warning",
                timer:1500
                });
           openLoginModal();
        }else{
         //获取隐藏域id
         var hiddenId = $("#hiddenIdInMarkeWindow1").val();
         if (hiddenId.length<=0) {
           //表示没有选中节点
           swal({ 
                    title: "请先选中数据！",
                     type: "error",
                     timer:1500
                        });
        }else{
            if(IsEnteringDataLegal){
                     save(function(){
            //将表单整体序列化成一个数组提交到后台
            var postData = $("#dataInDetailForm").serializeArray();
            $.ajax({
                    url:"../Ashx/ProcessUpdateAllData.ashx",
                    type:"POST",
                    data: postData,
                    success:function(Backdata){
                        if(Backdata == "ok"){
                           swal({ title: "保存成功！",
                            type: "success",
                            timer:1500
                              });
                            //保存后，将重新刷新树状结构
                               getTree();
                             //连续调用两次click相当于刷新标记
                             $("#ShowFarmBtn").click();
                            $("#ShowFarmBtn").click();
                        }
                        else{
                             swal({ title: "保存失败！",
                            type: "success",
                            timer:1500
                              });
                        }
                    }
            });
        });
                }else{
                swal({ 
                     title: "请检查数据格式是否正确！",
                    type: "warning",
                    timer:1500
                     });
                }
            }
        }
    });
    //----------------------------------------------------------------农田详细管理结束
    //----------------------------------------------------------------综合查询开始
    //综合查询按钮点击事件
    $("#MultipleSearchBtn").click(function(){
     $('[data-toggle="offcanvas"]').click();
      //手动清空上次查询的图
    $('#myChart').remove();
    $('#container').append( '<canvas class="my-4" id="myChart"></canvas>');

        $("#MultipleSearchModal").modal('show');
       
       
        //绑定重置按钮事件
        MultiPleClickReset();

        //初始化市
        initSeoCitySelect(); 
    });

    //综合查询中的查询按钮
     //查询按钮
     $("#selectSeo").click(function(){
        var postData = $("#MultileSearchForm").serializeArray();
        $.getJSON("../Ashx/ProcessSeoSelect.ashx",postData,function(data){
            if(data.length==0){
               swal({ 
                    title: "所选地区无样本点！",
                    type: "error",
                    timer:1500
                     });
            }else{
                //横轴的名称
                var ChartLabels = [];
                //数据点
                var ChartDataN = [];
                var ChartDataP = [];
                var ChartDataK = [];
                var ChartDataHydrolyticN = [];
                var ChartDataQuickP = [];
                var ChartDataQUicK = [];
                var ChartDataOrganicMatter = [];
                var ChartDataPH = [];

                for (var i = 0; i < data.length; i++) {
                    ChartLabels[i] = data[i].sampleName;
                    ChartDataN[i] = data[i].N;
                    ChartDataP[i] = data[i].P;
                    ChartDataK[i] = data[i].K;
                    ChartDataHydrolyticN[i] = data[i].HydrolyticN;
                    ChartDataQuickP[i] = data[i].QuickP;
                    ChartDataQUicK[i] = data[i].QUicK;
                    ChartDataOrganicMatter[i] = data[i].OrganicMatter;
                    ChartDataPH[i] = data[i].PH;
                }
                //画图
                 AddChart(ChartLabels,ChartDataN,ChartDataP,ChartDataK,ChartDataHydrolyticN,ChartDataQuickP,ChartDataQUicK,ChartDataOrganicMatter,ChartDataPH);
            }
               
        });
        });

    //导出excel按钮
    $("#exportToExcelBtn").click(function() {
        var postData = $("#MultileSearchForm").serializeArray();
        $.getJSON("../Ashx/ProcessJsonExportExcel.ashx", postData, function (data) {
            if (data.length == 0) {
                swal({
                    title: "所选地区无样本点！",
                    type: "error",
                    timer: 1500
                });
            } else {
                var option = {};

                option.fileName = 'excel';
                option.datas = [
                    {
                        //把返回的序列化信息给sheetData
                        sheetData: data,
                        sheetName: 'sheet',
                        sheetFilter: [
                            'FarmInfo', 'CropsMegCity', 'CropsMegCounty', 'CropsMegTown', 'CropsMegVillage',
                            'CropsMegSampleName', 'CropsMegLon', 'CropsMegLat', 'CropsMegNameOfHouseholder',
                            'CropsMegPhoneNumber', 'CropsMegIrrigationConditions', 'CropsMegAcreage',
                            'CropsMegFertility', 'CropsMegWeeds', 'SoilNutrientInfo', 'SoilNutrientN', 'SoilNutrientP',
                            'SoilNutrientK', 'SoilNutrientHydrolyticN', 'SoilNutrientQuickP', 'SoilNutrientQuickK',
                            'SoilNutrientOrganicMatter', 'SoilNutrientPh', 'CropData', 'CropsMegCropType',
                            'CropMegVarieties', 'CropMegYield', 'CropMegUrea', 'CropMegAn', 'CropMegK',
                            'CropMegOrganicManure', 'CropMegOthers', 'CropMegIrrigationTimes', 'FarmSug',
                            'MSugCropType', 'MSugVarieties', 'MSugTargetYield', 'MSugUrea', 'MSugAn', 'MSugK',
                            'MSugOrganicManure', 'MSugOthers', 'MSugIrrigationTimes', 'MSugSowingAmout',
                            'MSugSowingMethod', 'MSugWeedControl', 'MSugPestControl', 'MSugFieldManagement',
                            'MSugRemarkers'
                        ],
                        sheetHeader: [
                            '农田信息',
                            '市级', '县级', '乡/镇', '村', '采样点名称', '经度', '纬度', '户主姓名', '联系电话', '灌溉条件', '土地面积', '土地肥力', '杂草情况',
                            '土壤养分信息',
                            '氮(mg/kg)', '磷(mg/kg)', '钾(mg/kg)', '水解性氮(mg/kg)', '速效磷(mg/kg)', '速效钾(mg/kg)', '有机质(mg/kg)',
                            'PH',
                            '作物数据',
                            '作物类型', '品种', '产量(kg/亩)', '尿素(kg/亩)', '二胺(kg/亩)', '钾肥(kg/亩)', '有机肥(kg/亩)', '其他(kg/亩)',
                            '灌溉次数(次)',
                            '农田建议',
                            '作物类型', '品种', '目标常量(kg/亩)', '尿素(kg/亩)', '二胺(kg/亩)', '钾肥(kg/亩)', '有机肥(kg/亩)', '其他(kg/亩)',
                            '灌溉次数(次)', '播种量(kg/亩)', '播种方式', '杂草控制', '病虫害控制', '田间管理', '备注'
                        ]
                    },
                    {
                        sheetData: [['编码','含义'], ['1', '成都市'], ['2', '自贡市'],['3','攀枝花市'],['4','泸州市'],['5','绵阳市'],['6','绵阳市'],['7','广元市'],['8','遂宁市'],['9','内江市'],['10','乐山市'],['11','南充市'],['12','眉山市'],['13','宜宾市'],['14','广安市'],['15','达州市'],['16','雅安市'],['17','巴中市'],['18','资阳市'],['19','阿坝藏族羌族自治州'],['20','甘孜藏族自治州'],['21','凉山彝族自治州'],['22','成华区'],['23','武侯区'],['24','青羊区'],['25','锦江区'],['26','金牛区'],['27','龙泉驿区'],['28','青白江区'],['29','新都区'],['30','双流区'],['31','郫都区'],['32','温江区'],['33','大邑县'],['34','金堂县'],['35','蒲江县'],['36','新津县'],['37','都江堰市'],['38','彭州市'],['39','崇州市'],['40','邛崃市'],['41','自流井区'],['42','贡井区'],['43','大安区'],['44','沿滩区'],['45','荣县'],['46','富顺县'],['47','东区'],['48','西区'],['49','仁和区'],['50','米易县'],['51','盐边县'],['52','江阳区'],['53','纳溪区'],['54','龙马潭区'],['55','泸县'],['56','合江县'],['57','叙永县'],['58','古蔺县'],['59','旌阳区'],['60','中江县'],['61','罗江县'],['62','广汉市'],['63','绵竹市'],['64','什邡市'],['65','涪城区'],['66','游仙区'],['67','江油市'],['68','三台县'],['69','安县'],['70','平武县'],['71','盐亭县'],['72','北川羌族自治县'],['73','利州区'],['74','利州区'],['75','朝天区'],['76','旺苍县'],['77','青川县'],['78','剑阁县'],['79','苍溪县'],['80','船山区'],['81','安居区'],['82','蓬溪县'],['83','大英县'],['84','射洪县'],['85','市中区'],['86','东兴区'],['87','资中县'],['88','隆昌县'],['89','威远县'],['90','市中区'],['91','五通桥区'],['92','沙湾区'],['93','金口河区'],['94','犍为县'],['95','井研县'],['96','夹江县'],['97','沐川县'],['98','峨边彝族自治县'],['99','马边彝族自治县'],['100', '顺庆区'], ['102', '高坪区'],['103','嘉陵区'],['104','南部县'],['105','仪陇县'],['106','西充县'],['107','蓬安县'],['108','营山县'],['109','阆中市'],['110','东坡区'],['111','仁寿县'],['112','彭山县'],['113','洪雅县'],['114','丹棱县'],['115','青神县'],['116','翠屏区'],['117','宜宾县'],['118','南溪县'],['119','江安县'],['120','长宁县'],['121','高县'],['122','珙县'],['123','筠连县'],['124','兴文县'],['125','屏山县'],['126','广安区'],['127','岳池县'],['128','武胜县'],['129','邻水县'],['130','华蓥市'],['131','通川区'],['132','达川区'],['133','宣汉县'],['134','开江县'],['135','大竹县'],['136','渠县'],['137','万源市'],['138','雨城区'],['139','芦山县'],['140','名山县'],['141','天全县'],['142','荥经县'],['143','宝兴县'],['144','汉源县'],['145','石棉县'],['146','巴州区'],['147','通江县'],['148','南江县'],['149','平昌县'],['150','雁江区'],['151','安岳县'],['152','乐至县'],['153','简阳市'],['154','汶川县'],['155','理县'],['156','茂县'],['157','松潘县'],['158','金川县'],['159','黑水县'],['160','马尔康县'],['161','壤塘县'],['162','阿坝县'],['163','若尔盖县'],['164','红原县'],['165','小金县'],['166','九寨沟县'],['167','康定县'],['168','丹巴县'],['169','炉霍县'],['170','九龙县'],['171','甘孜县'],['172','雅江县'],['173','新龙县'],['174','道孚县'],['175','白玉县'],['176','理塘县'],['177','德格县'],['178','乡城县'],['179','石渠县'],['180','稻城县'],['181','色达县'],['182','巴塘县'],['183','泸定县'],['184','得荣县'],['185','西昌市'],['186','木里县'],['187','盐源县'],['188','德昌县'],['189','会理县'],['190','会东县'],['191','宁南县'],['192','普格县'],['193','布拖县'],['194','金阳县'],['195','昭觉县'],['196','喜德县'],['197','冕宁县'],['198','越西县'],['199','甘洛县'],['200','美姑县'],['201','雷波县']]
                    }
                ];
                var toExcel = new ExportJsonExcel(option);
                toExcel.saveExcel();
            }
        });
    });
    //----------------------------------------------------------------综合查询结束
    //----------------------------------------------------------------录入农田信息开始
    $("#EnteringInfoBtn").click(function () {
        //清空窗体数据
        ClearThisWindowData();
            //让功能按钮隐藏
         $('[data-toggle="offcanvas"]').click();
         //上传图片的窗口恢复原状
        RestitutionUpLoadWind();
        $("#EnterFarmInfoModal").modal({
            //resizable: true,
            height: "auto",
            width: "auto",
            //定义是否将窗体显示为模式化窗口。
            modal: true,
            //定义是否显示可折叠按钮。
            collapsible: true,
            //定义窗体边框的样式
            border: 'thick',
            minimizable: true,
            maximizable: true,
            //定义是否可以改变对话框窗口大小。
            resizable: true,
            closable: true,
            closed: false,
            //在窗体显示的时候显示阴影。
            shadow: true,
            cache: false,
            //href: '示例.html',
            modal: true,
            backdrop: "static"
        });
        //给默认值
//        $(":text").val("1");
//        $("#PhoneNumberText").val("15667898675");
    });
    //录入农田信息Moda 提交绑定事件
    $("#EnteringConFirmSubmitBtn").click(ConFirmSubmitClick);
    //录入农田信息 Reset事件
    $("#EnteringResetBtn").click(ResetBtnClick);
    //绑定市区
    UpLoadProvince();
    //----------------------------------------------------------------录入农田信息结束

    //上传图片按钮的改变事件
    //img();
    //录入框的功能按钮事件
    FunctionBtn();

    //Logo的点击事件
    $("#LogoImg").click(function(){
        location.reload() ;
        document.body.scrollTop = 0;
    });



    //读取coockie写入text
    document.getElementById("UserIdText").value = getCookie("UserName");
    document.getElementById("UserPwdText").value = getCookie("pwd");
    var boolLog = getCookie("IsLogin");
    if(boolLog == "OK"){
        isLog = true;
        $("#btnLogin").val("已登录,点击退出登录");
    }

    //设置文档加载2秒后，功能按钮显示
    setTimeout(function(){
         $("#FuncitonBtn").css("display","block");
    },2000);


    //用空格时，手动调用开始按钮的点击事件
    $(document).keydown(function(e){
    if(!e) var ew = window.event; 
    if(e.keyCode==32){
       $("#beginBtn").click();
    }
 });

    

    //图片上传处理
       $("#file-1").fileinput({
        uploadUrl: '../Ashx/PicUpload.ashx', // you must set a valid URL here else you will get an error
        allowedFileExtensions : ['jpg', 'png','gif','jpeg'],
        overwriteInitial: false,
         language: 'zh', //设置语言
        maxFileSize: 10000,
        maxFilesNum: 10,
        showRemove:false,
         previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        allowedFileTypes: ['jpg', 'png','gif','jpeg'],
        deleteUrl:'../Ashx/PicUpload.ashx',   // String删除图片时的请求路径
        slugCallback: function(filename) {
            return filename.replace('(', '_').replace(']', '_');
        }
	});

    //	异步上传成功结果处理
	$("#file-1").on("fileuploaded", function (event, data, previewId, index) {
         //向隐藏域中添加数据
            hiddenPic=  $("#hidSavePicPath").val();
            if(hiddenPic.length==0){
            $("#hidSavePicPath").val(data.response.msg);
            }else{
            $("#hidSavePicPath").val(hiddenPic+";"+data.response.msg);
            }
	})

});


//获取cookie
function getCookie(name){
   var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
   if(arr != null){
  	 return unescape(arr[2]).toString(); 
   }else{
  	 return null;
   }
} 

 //录入框的功能按钮事件
function FunctionBtn(){
     var trigger = $('.hamburger'),
		      overlay = $('.overlay'),
		     isClosed = false;

		    trigger.click(function () {
		      hamburger_cross();      
		    });

		    function hamburger_cross() {

		      if (isClosed == true) {          
		        overlay.hide();
		        trigger.removeClass('is-open');
		        trigger.addClass('is-closed');
		        isClosed = false;
		      } else {   
		        overlay.show();
		        trigger.removeClass('is-closed');
		        trigger.addClass('is-open');
		        isClosed = true;
		      }
		  }
		  
		  $('[data-toggle="offcanvas"]').click(function () {
		        $('#wrapper').toggleClass('toggled');
                 $(this).toggle();
		  });  
           $(".overlay").click(function(){
                $('[data-toggle="offcanvas"]').click();
          });
}

//录入界面div的点击事件
function Divclick(thisDiv){
     $(thisDiv).find('a')[0].click();
}

//函数：获取尺寸
function boxheight() { 
    //获取浏览器窗口高度
    var winHeight = 0;
    if (window.innerHeight)
        winHeight = window.innerHeight;
    else if ((document.body) && (document.body.clientHeight))
        winHeight = document.body.clientHeight;
    //通过Document对body进行检测，获取浏览器可视化高度
    if (document.documentElement && document.documentElement.clientHeight)
        winHeight = document.documentElement.clientHeight;
    //DIV高度为浏览器窗口高度
    document.getElementById("mapDiv").style.height = winHeight + "px";

}


//-----------------------------------------------------------------------------------------------------------------显示农田开始
//鼠标触碰标记展开信息窗口
    //content 标记的文字信息
function openInfo(content, e) {
    //获取坐标
    var point = e.lnglat;
    //创建一个信息窗实例
    var markerInfoWin = new T.InfoWindow(content, { offset: new T.Point(0, -30) }); // 创建信息窗口对象
    map.openInfoWindow(markerInfoWin, point); //开启信息窗口
}

//鼠标离开标记关闭信息窗口
function closeInfo() {
    //关闭信息窗
    map.closeInfoWindow();
}
//点击标记展示详细信息窗口
function clickOpenWindow(dataId) {        
    //清空窗体数据
    ClearThisWindowData();  
       
    //点击标记时，将IsEnteringDataLegal设置为true
    IsEnteringDataLegal= true;
    
                     
    //将id赋值给隐藏的id域
    $("#hiddenIdInMarkeWindow").val(dataId);

    //将展示图片的窗口恢复原状
        RestitutionShowWind("MarkerOL","MarkerImgDiv","MarkerImgOutDiv","MarkerLi","MarkerImgNearDiv","MarkerDefaultImg");

        //异步读取所有数据
        ClickMarkerLoadData(dataId,"#FarmInfoDiv .EnteringText","#Select2","#SoilNutrientInfoDiv .EnteringText","#CropDataDiv .EnteringText","#FarmSugDiv .EnteringText","#MarkerImgDiv","#MarkerOL","#MarkerImgOutDiv","MarkerImgNearDiv");



    $("#FarmInfoModal").modal('show');
}
//异步读取所有数据
function ClickMarkerLoadData(dataId,selectorOfDiv,selectorOfCountry,showDiv2,showDiv3,showDiv4,ImgDivselector,OlSelect,ImgOutDivSelect,imgNearDivId){
     $.getJSON("../Ashx/ClickMarkerLoadData.ashx",{id:dataId},function(data){
            var FarmlandMegData =  data.FarmlandMegObject;
            var SoilNutrientMegData = data.SoilNutrientMegObject;
            var CropsMegData = data.CropsMegObject;
            var FarmlandMSugData = data.FarmlandMSugObject;
            var PicData = data.PicObject;

            //加载“农田信息”窗口
            LoadFarmlandMegData(selectorOfDiv,selectorOfCountry,FarmlandMegData);
            //加载第二个窗口数据
            LoadOthersData(showDiv2,SoilNutrientMegData);
            LoadOthersData(showDiv3,CropsMegData);
            //加载第三个窗口数据
            LoadOthersData(showDiv4,FarmlandMSugData);
            //加载图片
            LoadPictureData(ImgDivselector,OlSelect,ImgOutDivSelect,imgNearDivId,PicData);
     });
}
//加载“农田信息”窗口
function LoadFarmlandMegData(selectorOfDiv,selectorOfCountry,data){
    //获取表单中所有的数据域
    var dataArea = $(selectorOfDiv);
    //返回的data数据中第一个数据是id 故num需要从-1开始 
    var num = -1;
    //遍历处理data
        $.each(data,function(index,element){
        if (num>=0&&dataArea[num].type!='select-one') {
            //text类型的标签
            dataArea[num].value=element;
        }else if (num>=0&&dataArea[num].type=='select-one')
            {
            //是select类型的标签
            //如果是选择“县/区”
            if (index=="County") {
                //拿到市的id
                //$("#select1").change();
                var cId = dataArea[num-1].value
                FarmInfoCountyId = element;
                //通过异步提交给一般处理程序 给县区选项赋值
                $.ajax({
                    url:"../Ashx/LoadCounty.ashx",
                    type:"POST",
                    dataType:"Json",
                    data: {PID:cId},
                    success:function(Backdata){
                        $("#select2").empty();
                        $("#FarmDetailManaCountySelect").empty();
                        $.each(Backdata, function(index, item){
                            $("#select2").append("<option value='"+Backdata[index].AreaID+"'>" + Backdata[index].AreaName+"</option>");
                            $("#FarmDetailManaCountySelect").append("<option value='"+Backdata[index].AreaID+"'>" + Backdata[index].AreaName+"</option>");
                        });
                        $("#select2").val(FarmInfoCountyId);
                        $("#FarmDetailManaCountySelect").val(FarmInfoCountyId);
                    }
                });
            }else {
                //是“市”
                dataArea[num].value = element;
                }
            }
        num++;
        });
}

//加载其他三个窗口
function LoadOthersData(showDiv,data){
     //获取表单中所有的数据域
        var dataArea = $(showDiv);
        var num = -1;
        //遍历处理data
        $.each(data,function(index,element){
               if (num>=0&&num<dataArea.length&&dataArea[num].type!='select-one') {
               //text类型的标签
                  dataArea[num].value=element;
                }
                num++;
         });
}

//加载图片窗口
function LoadPictureData(ImgDivselector,OlSelect,ImgOutDivSelect,imgNearDivId,data){
       for (var i = 0; i < data.length; i++) {
                            //加载服务器上的图片
                            AddPicture(ImgDivselector, OlSelect,data[i].picPath,ImgOutDivSelect,imgNearDivId);
                            }   
}
//调用此函数可将所有text类型的标签中的数据清空
function ClearThisWindowData() {
        //*****************************************************
        //修改
        //将农田详细信息管理的隐藏域清空
        $("#hiddenIdInMarkeWindow1").val("");

        //将点击标记弹出的窗口中的隐藏域清空
        $("#hiddenIdInMarkeWindow").val("");
        //*****************************************************
        //所有text类型的标签集合
        var dataArea = $(".EnteringText");
        //将表格中的数据清空
        $.each(dataArea,function(index,item){
            //只清空text类型的标签的数据和背景色
            if(item.type=="text"){
                //内容清空
                item.value="";
                //背景色恢复
                item.style.backgroundColor = "#FFFFFF";
            }
        });

        //将所有的输入错误按钮去除
        $(".popover-content, .arrow").parent().popover('destroy');
}

 //将展示图片的窗口恢复原状
 //OlId         olId
 //imgDivId     图片外层id
 //imgOutDivId 最外层divID
 //liId       li的id
 //imgNearDivId   最近的divid
 //defaultImgId  默认图片的id
function RestitutionShowWind(OlId,imgDivId,imgOutDivId,liId,imgNearDivId,defaultImgId){
    var newLi = document.createElement("li");
    newLi.setAttribute("id",liId);
    newLi.setAttribute("data-target","#"+imgOutDivId);
    newLi.setAttribute("data-slide-to",0);
    newLi.setAttribute("class","active");

    //将li清空，并添加新的li
    var olObject = document.getElementById(OlId);
    olObject.innerHTML = "";
    olObject.appendChild(newLi);
//    $(OlSelect).empty().append(newLi);



    var newDiv = document.createElement("div");
    //defaultImg
    newDiv.setAttribute("id",imgNearDivId);
    newDiv.setAttribute("class","item active");

    var newImg = document.createElement("img");
    newImg.setAttribute("class","d-block w-100 img-responsive img-rounded");
    newImg.setAttribute("src","../Resource/Default.png");
    newImg.setAttribute("alt","First slide");
    newImg.setAttribute("id",defaultImgId)

    //将图片标签加入div中
    newDiv.appendChild(newImg);

    var divObject =document.getElementById(imgDivId);
    divObject.innerHTML="";
    divObject.appendChild(newDiv);
}

 //上传图片的窗口恢复原状
function RestitutionUpLoadWind(){
        //将上传图片位置清空
        $(".form-group .close.fileinput-remove").click();
        //将隐藏域的val清空
        $("#hidSavePicPath").val("");
}


//动态加载图片
//ImgDivselector 图片的最外层div  id
//OlSelect   指示标的ol  id
//PicPath   图片路径
//ImgOutDivSelect   图片预览的最外层div的id 确保li按钮的可用性
function AddPicture(ImgDivselector,OlSelect, PicPath,ImgOutDivSelect,imgNearDivId) {
    
    //获取li的个数
    var StrIndex = $(OlSelect)[0].childElementCount; 
    

     var defaultImgPath = document.getElementById(imgNearDivId).children[0].src;
      var defaultImgName =  defaultImgPath.substring(defaultImgPath.lastIndexOf("/")+1);

    //查看默认实例图片
    if(defaultImgName=="Default.png"){
        document.getElementById(imgNearDivId).children[0].src = PicPath;

    }else{
         var newDiv = $("<div class='item '></div>");
        //添加图片
        var myImg = document.createElement("img");
        myImg.setAttribute("class", "d-block w-100 img-responsive img-rounded");
        myImg.setAttribute("src", PicPath);
        myImg.setAttribute("alt",StrIndex+1+ " slide");
        myImg.setAttribute("name",StrIndex+"img");
        //将图片添加至div中
        newDiv.append(myImg);
        $(ImgDivselector).append(newDiv)

        //设置li
        var li = document.createElement("li");
        li.setAttribute("data-target", ImgOutDivSelect);
        li.setAttribute("data-slide-to", StrIndex);
        $(OlSelect).append(li);
    }

}

//-----------------------------------------------------------------------------------------------------------------显示农田结束
//-----------------------------------------------------------------------------------------------------------------农田详细管理开始
    //获取树状结构
  
  //获取树状结构数据
function getTree() {
        //Some logic to retrieve, or generate tree structure
        $.ajax({
            url:"../Ashx/GetTreeData.ashx",
            type:"Post",
            dataType:"Json",
            success:
                function(BackData){
                    $('#treeUlDiv').treeview({
                        data: BackData,
                        onNodeSelected: function(event, data) {
                            //data["tags"]  选中的ID
                            if(data["tags"] >= 0){
                               
                        //点击节点时，将IsEnteringDataLegal设置为true
                        IsEnteringDataLegal= true;
    
                        //调用此函数可将所有text类型的标签中的数据清空
                        ClearThisWindowData()
                        //将展示图片的窗口恢复原状
                         RestitutionShowWind("DetailOl","DetailImgDiv","DetailImgOutDiv","DetailLI","DetailImgNearDiv","DetailDefaultImg");



                          //将id赋值给隐藏的id域
                                $("#hiddenIdInMarkeWindow1").val(data["tags"]);
                                 //异步读取所有数据
                          ClickMarkerLoadData($("#hiddenIdInMarkeWindow1").val(),"#FarmDetailInfoDiv .EnteringText","#FarmDetailManaCountySelect","#SoilNutrientMegDivofDetail .EnteringText","#CropsMegDivofDetail .EnteringText","#FarmlandMSugDivofDetail .EnteringText","#DetailImgDiv","#DetailOl","#DetailImgOutDiv","DetailImgNearDiv");
                            }
                        }
                    });
                }
        });
    }

//-----------------------------------------------------------------------------------------------------------------农田详细管理结束
//-----------------------------------------------------------------------------------------------------------------综合查询开始
//绑定“综合查询中”市的下拉列表事件
function initSeoCitySelect() {
    $.post("../Ashx/LoadSeoCitySelect.ashx", "", function (data) {
        //清空
        $("#seoSelectCity").empty();
        //填充
        $("#seoSelectCity").html(data);


        //绑定市的下拉列表改变事件
        bindSeoCityChangeEvent();
        //触发城市的改变
        $("#seoSelectCity").change();
    });
}

//绑定“综合查询中”区/县的下拉列表改变事件
function bindSeoCityChangeEvent() {
    $("#seoSelectCity").change(function () {
        //拿到市的id
        var cId = $(this).val();
        

        //通过异步读取数据
        $.post("../Ashx/LoadSeoCountrySelect.ashx", { cId: cId }, function (data) {
                //清空
            $("#seoSelectCountry").empty();
            //填充
                $("#seoSelectCountry").html(data);
        });

    });
}


//添加柱状图div
function AddChart(ChartLabels,ChartDataN,ChartDataP,ChartDataK,ChartDataHydrolyticN,ChartDataQuickP,ChartDataQUicK,ChartDataOrganicMatter,ChartDataPH)
{
//查询时，将上次的图清空
$('#myChart').remove();
$('#container').append( '<canvas class="my-4" id="myChart"></canvas>');

var ctx = document.getElementById("myChart");
var myChart = new Chart(ctx, {
type: 'bar',
data: {
    labels: ChartLabels,
    datasets:[{
        label: '氮',
        fill:true,
        data:ChartDataN,
        lineTension: 2,
        backgroundColor: '#007bff',
        borderColor: '#007bff',
        borderWidth: 4,
        pointBackgroundColor: '#007bff'
},{
        label: '磷',
        fill:true,
        backgroundColor:  '#6BC235',
        borderColor: '#6BC235',
        data:ChartDataP,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#6BC235'
},{
        label: '钾',
        fill:true,
        backgroundColor:  '#FEF143',
        borderColor: '#FEF143',
        data:ChartDataK,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#FEF143'
},{
        label: '水解性氮',
        fill:true,
        backgroundColor:  '#FE4365',
        borderColor: '#FE4365',
        data:ChartDataHydrolyticN,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#FE4365'
},{
        label: '速效磷',
        fill:true,
        backgroundColor:  '#6BC235',
        borderColor: '#6BC235',
        data:ChartDataQuickP,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#6BC235'
},{
        label: '速效钾',
        fill:true,
        backgroundColor:  '#83AF9B',
        borderColor: '#83AF9B',
        data:ChartDataQUicK,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#83AF9B'
},{
        label: '有机质',
        fill:true,
        backgroundColor:  '#F9CCAD',
        borderColor: '#F9CCAD',
        data:ChartDataOrganicMatter,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#F9CCAD'
},{
        label: 'PH',
        fill:true,
        backgroundColor:  '#3EBCCA',
        borderColor: '#3EBCCA',
        data:ChartDataPH,
        lineTension: 2,
        borderWidth: 4,
        pointBackgroundColor: '#3EBCCA'
}
]},
options: {
    //刻度
    scales: {
    //y轴
    yAxes: [{
        ticks: {
        //y轴是否从0开始
        beginAtZero: true
        }
    }]
    },
    //示例
    legend: {
    display: true
    }
}
});
        
}
//重置按钮
function MultiPleClickReset(){
    $("#resetSeo").click(function(){
        //清除并添加话图框
            $('#myChart').remove();
            $('#container').append( '<canvas class="my-4" id="myChart"></canvas>');
            //将市赋为全选
            $("#seoSelectCity").val("-1");
            //$("#seoSelectCountry").val("-1");
            //触发城市的改变
            $("#seoSelectCity").change();
    });
}
function removeData(chart) {
    $.each(chart.data.labels, function(){
        chart.data.labels.pop(); 
    });
        
    chart.data.datasets.forEach((dataset) => { dataset.data.pop(); }); 
    chart.update(); 
}

//-----------------------------------------------------------------------------------------------------------------综合查询结束
//-----------------------------------------------------------------------------------------------------------------录入农田信息开始
//录入农田信息Modal 提交
function ConFirmSubmitClick() {
    //判断是否登录
    if(isLog==false){
         swal({ 
                title: "请先登录",
                type: "warning",
                timer:1500
                });
       openLoginModal();
    }else{

    //获取用户输入的经纬度,并进行初略判断是否是四川省内的坐标点
    var logD = $("#LogD").val();
    var logM = $("#LogM").val();
    var logS = $("#LogS").val();
    
    var log = logD*1+logM/60.0*1+logS/3600.0*1;

    var latD = $("#LatD").val();
    var latM = $("#LatM").val();
    var latS = $("#LatS").val();

    var lat = latD*1+latM/60.0*1+latS/3600.0*1;
//    在东经97°21′~108°31′，北纬26°03′~34°19′之间。
    //判断是否输入经纬度
      if(log>97.35&&log<108.51666666666667&&lat>26.05&&lat<34.31666666666667){
         //判断数据是否合法
         if (IsEnteringDataLegal == false|| $(".popover-content, .arrow").length>0) {
                   swal({ 
                        title: "请检查数据格式是否正确",
                        type: "error",
                        timer:1500
                             });
                     return;
            }
            else{
            var postdata = $("#EnteringFarmInfoForm").serializeArray();
            $.ajax({
                url:"../Ashx/ProcesAllFormData.ashx",
                data:postdata,
                type:"Post",
                //dataType:"Json",
                success:function(Backdata){
                    if(Backdata == "ok"){
                            swal({ 
                            title: "录入成功",
                            type: "success",
                            timer:1500
                         });
                         //上传图片的窗口恢复原状
                        RestitutionUpLoadWind();
                        //刷新显示农田
                        $("#ShowFarmBtn").click();
                        $("#ShowFarmBtn").click();
                        //清除窗体数据
                         ClearThisWindowData();
                         //关闭
                        $("#EnteringCloseBtn").click();
                    }
                    else{
                        swal({
                            title:"录入失败！",
                             type: "error",
                            timer:1500
                        });
                    }
                }
            });
    }
       }else{
         swal({ 
                title: "请输入正确的四川省内的经纬度坐标",
                type: "error",
                timer:2000
                     });
       }
    }
}
//Modal清空  录入界面的重置按钮事件
function ResetBtnClick(){
    //上传图片的窗口恢复原状
    RestitutionUpLoadWind();
    //将数据清空
    $(":text").val("");
    IsEnteringDataLegal = false;
    
        //将所有的输入错误按钮去除
    $(".popover-content, .arrow").parent().popover('destroy');

    swal({ 
            title: "重置成功",
            type: "success",
            timer:2000
                    });
}

//判断PhoneNumber是否合法
function IsPhoneNumLegal(tagObject) {
    //判断11位电话号码的正则表达式
    var reg = new RegExp("^[0-9]{11}$");
    //获取当前标签上的值
    var str = tagObject.value;
    if (reg.test(str) == false) {
        $(tagObject).data("toogle", "right").data("placement", "right").data("container", $(tagObject).parent()).popover({ "trigger": "manual","html":"true","content":"<p ><font color='#fc4343' >请输入正确的电话号码</font></p>" }).popover("show");
        IsEnteringDataLegal = false;
    }
    else {
        tagObject.style.backgroundColor = "#FFFFFF";
        IsEnteringDataLegal = true;
          $(tagObject).popover('destroy');
    }
}

//判断 土块面积 kg/亩 经纬度 是否合法
function IsDataLegal(tagObject) {
    //判断非负数的正则表达式
    var reg = /^\d+(\.{0,1}\d+){0,1}$/;
    //获取标签上的value
    var str = tagObject.value;
    //判断是否满足正则
    if (reg.test(str) == false) {
        $(tagObject).data("toogle", "right").data("placement", "right").data("container", $(tagObject).parent()).popover({ "trigger": "manual","html":"true","content":"<p ><font color='#fc4343'>请输入合法数据</font></p>" }).popover("show");
        IsEnteringDataLegal = false;
        return;
    }
    else {
        IsEnteringDataLegal = true;
          $(tagObject).popover('destroy');
    }
}
//判断 灌溉次数 是否合法
function IsIrrigationTimesLeagal(tagObject) {
    //判断是否是正整数
    var reg = /^[1-9]\d*$/;
    //获取标签上的value
    var str = tagObject.value;
    //判断是否满足正则
    if (reg.test(str) == false) {
        $(tagObject).data("toogle", "right").data("placement", "right").data("container", $(tagObject).parent()).popover({ "trigger": "manual","html":"true","content":"<p ><font color='#fc4343'>请输入合法数据</font></p>" }).popover("show");
        IsEnteringDataLegal = false;
        return;
    }
    else {
        IsEnteringDataLegal = true;
         $(tagObject).popover('destroy');
    }
}

//-----------------------------------------------------------------------------------------------------------------录入农田信息结束

//对地图的处理
//添加放大缩小按钮，比例尺按钮
function addControl() {


     //添加比例尺
    var control2 = new T.Control.Scale();
    control2.setPosition(T_ANCHOR_BOTTOM_RIGHT);
    map.addControl(control2);

    //添加鹰眼
     var miniMap = new T.Control.OverviewMap({
                isOpen: true,
                size: new T.Point(150, 150)
            });
    map.addControl(miniMap);

 


     //添加放大缩小按钮
    var control1 = new T.Control.Zoom();
    control1.setPosition(T_ANCHOR_TOP_LEFT);
    map.addControl(control1);
}

//绘制各个市之间的边界
function PaintEdge(){
    points = [];
    var Provinces = ["成都市", "绵阳市", "自贡市", "攀枝花市", "泸州市", "德阳市", "广元市", "遂宁市", "内江市", "乐山市", "资阳市", "宜宾市", "南充市", "达州市", "雅安市", "阿坝藏族羌族自治州", "甘孜藏族自治州", "凉山彝族自治州", "广安市", "巴中市", "眉山市"];

    $.each(Provinces, function (iP, valueP) {
    var URL = "http://www.tianditu.com/query.shtml?postStr={'keyWord':'@PROVINCE','level':'7','mapBound':'110.57288,36.77153,122.21838,43.05571','queryType':'1','count':'20','start':'0'}&type=query".replace("@PROVINCE", valueP);
        $.getJSON(URL, function (data) {
            $.each(data.area.points, function (i, value) {

                points = [];
                //value["region"] 是存储经纬度的
                //tmp是一个经纬度的json字符串
                var tmp = value["region"].replace(/,/g, "},{\"LON\":");
                tmp = "[{\"LON\":" + tmp.replace(/ /g, ",\"LAT\":") + "}]";
                //tmpObj是一个Json数组
                var tmpObj = JSON.parse(tmp);
                $.each(tmpObj, function (index, item) {
                    points.push(new T.LngLat(item.LON, item.LAT));
                });

                //创建线对象
                var line = new T.Polyline(points, {
                    color: "#FF0000",
                    weight: 5,
                    opacity: 1,
                });

                //面的线的样式
                var style = {
                    strokeColor: "#FF0000",
                    fillColor: getRandomColor(),
                    strokeWeight: 1,
                    strokeOpacity: 0.000001,
                    fillOpacity: 0.3
                }

                var poly = new T.Polygon(points, style);
                //向地图上添加线
                map.addOverLay(poly);
            });
        });
    });
}

//获取随机颜色
function getRandomColor() {

    return '#' +
        (function (color) {
            return (color += '0123456789abcdef'[Math.floor(Math.random() * 16)])
            && (color.length == 6) ? color : arguments.callee(color);
        })('');
}

//加载录入、详细信息管理界面市区
function UpLoadProvince(){
    $.ajax({
        url:"../Ashx/LoadProvince.ashx",
        type:"POST",
        dataType:"Json",
        success: function(data){
            $("#EnteringcitySelect").empty();
            $("#FarmDetailManaProSelect").empty();
            $.each(data, function(index, item){
                $("#EnteringcitySelect").append("<option value='"+data[index].AreaID+"'>" + data[index].AreaName+"</option>");
                $("#FarmDetailManaProSelect").append("<option value='"+data[index].AreaID+"'>" + data[index].AreaName+"</option>");
                $("#select1").append("<option value='"+data[index].AreaID+"'>" + data[index].AreaName+"</option>");
            });
            //成功之后给select绑定一个改变事件
            $("#EnteringcitySelect").change(UpLoadCounty);
            $("#EnteringcitySelect").change();
            $("#FarmDetailManaProSelect").change(UpLoadFarmDetailInfoCounty);
            $("#FarmDetailManaProSelect").change();
            $("#select1").change(UpLoadSelect2County);
            $("#select1").change();
        }
    });
}

//动态加载录入界面区县
function UpLoadCounty(){
    $.ajax({
        url:"../Ashx/LoadCounty.ashx",
        type:"POST",
        dataType:"Json",
        data: {PID:$(this).val()},
        success:function(Backdata){
            $("#EnteringCountrySelect").empty();
            $.each(Backdata, function(index, item){
                $("#EnteringCountrySelect").append("<option value='"+Backdata[index].AreaID+"'>" + Backdata[index].AreaName+"</option>");
            });
        }
    });
}

//动态加载农田详情信息界面区县
function UpLoadFarmDetailInfoCounty(){
    $.ajax({
        url:"../Ashx/LoadCounty.ashx",
        type:"POST",
        dataType:"Json",
        data: {PID:$(this).val()},
        success:function(Backdata){
            $("#FarmDetailManaCountySelect").empty();
            $.each(Backdata, function(index, item){
                $("#FarmDetailManaCountySelect").append("<option value='"+Backdata[index].AreaID+"'>" + Backdata[index].AreaName+"</option>");
            });
        }
    });
}

//动态点击marker界面区县
function UpLoadSelect2County(){
    $.ajax({
        url:"../Ashx/LoadCounty.ashx",
        type:"POST",
        dataType:"Json",
        data: {PID:$(this).val()},
        success:function(Backdata){
            $("#select2").empty();
            $.each(Backdata, function(index, item){
                $("#select2").append("<option value='"+Backdata[index].AreaID+"'>" + Backdata[index].AreaName+"</option>");
            });
        }
    });
}

//****************************************************登录
function showRegisterForm(){
    $('.loginBox').fadeOut('fast',function(){
        $('.registerBox').fadeIn('fast');
        $('.login-footer').fadeOut('fast',function(){
            $('.register-footer').fadeIn('fast');
        });
        //清空注册窗口数据
        $("#RegistForm .form-control").val("");
        $('.modal-title.logIn').html('注册');
    }); 
    $('.error').removeClass('alert alert-danger').html('');
       
}
function showLoginForm(){
    $('#loginModal .registerBox').fadeOut('fast',function(){
        $('.loginBox').fadeIn('fast');
        $('.register-footer').fadeOut('fast',function(){
            $('.login-footer').fadeIn('fast');    
        });
        
        $('.modal-title.logIn').html('登录');
    });       
     $('.error').removeClass('alert alert-danger').html(''); 
}

//打开登录窗口
function openLoginModal(){
   
    $('[data-toggle="offcanvas"]').click();
    showLoginForm();
    $('#loginModal').modal('show');    
//    setTimeout(function(){
//        $('#loginModal').modal('show');    
//    }, 20);
    
}

//打开注册窗口
function openRegisterModal(){
    $('[data-toggle="offcanvas"]').click();
    showRegisterForm();
    $('#loginModal').modal('show'); 
//    setTimeout(function(){
//        $('#loginModal').modal('show');    
//    }, 20);
    
}

//验证登录
function loginAjax() {
    //如果已经登录，点击将退出登录
    if(isLog==true){
    logoff(function(){
        isLog=false;
        swal({ title: "退出登录成功！",
                type: "success",
                timer:1500
                    });
         $("#btnLogin").val("登录");
        });
    }else{
    //将表单整体序列化成一个数组提交到后台
    var postData = $("#loginForm").serializeArray();
    $.post( "../Ashx/VerifyIogin.ashx",postData, function( data ) {
            if(data == "ok"){
                $('#loginModal').modal('hide');
                //禁用登录按钮
                $("#btnLogin").val("已登录,点击退出登录");
                isLog=true;
                  swal({
                    title: "登录成功！",
                    type: "success",
                    timer: 1500
                });
            } else {
                 shakeModal(data); 
            }
        });
      }
}

//验证注册
function registerAjax(){
    //将表单整体序列化成一个数组提交到后台
    var postData = $("#RegistForm").serializeArray();
    $.post( "../Ashx/registerCount.ashx",postData, function( data ) {
            if(data == "ok"){
                swal({ title: "注册成功!",
                            type: "success",
                            timer:1500
                        })
                isLog = false;
                $("#btnLogin").val("登录");
                //将登录名密码填好
                $("#UserIdText").val( $("#email_register").val());
                $("#UserPwdText").val($("#password_register").val());
                showLoginForm();
            } else {
                 shakeModal(data); 
            }
        });
}

//登录窗口或者注册窗口震动
function shakeModal(data) {
    $('#loginModal .modal-dialog').addClass('shake');
    $('.error').addClass('alert alert-danger').html(data);
             $('input[type="password"]').val('');
             setTimeout( function(){ 
                $('#loginModal .modal-dialog').removeClass('shake'); 
    }, 400 ); 
}

//*********弹出框*****************
//使用sweetalert的弹出框操作
 function check(Func) {
            swal({ 
                    title: "您确定要删除这条数据吗",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "确定删除！",
                    cancelButtonText: "取消",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                function (isConfirm) {
                    if (isConfirm) {
                       Func();
                    }
                    else {
                        swal({ title: "已取消",
                            type: "error",
                            timer:1500
                        })
                    }
                }
            )
            }
function save(Func) {
       swal( { 
                    title: "您确定要保存吗",
                    type: "info",
                    showCancelButton: true,
                    confirmButtonColor: "#6CE26C",
                    confirmButtonText: "确定保存！",
                    cancelButtonText: "取消",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                function (isConfirm) {
                    if (isConfirm) {
                       Func();
                    }
                    else {
                        swal({ title: "已取消",
                            type: "error",
                            timer:1500
                        })
                    }
                }
            )
    }

function logoff(Func) {
       swal( { 
                    title: "您确定要退出登录吗",
                    type: "info",
                    showCancelButton: true,
                    confirmButtonColor: "#6CE26C",
                    confirmButtonText: "确定退出！",
                    cancelButtonText: "取消",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                function (isConfirm) {
                    if (isConfirm) {
                       Func();
                    }
                    else {
                        swal({ title: "已取消",
                            type: "error",
                            timer:1500
                        })
                    }
                }
            )
    }