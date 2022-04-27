var id = 0;

function addData() {
    var parent = document.getElementById("menu");
    var original = document.getElementById("menuDiv");
    var clone = original.cloneNode(true);
    clone.id = "duplicate_" + ++id;
    parent.appendChild(clone);
}

function removeData() {
    var parent = document.getElementById("menu");
    var node = parent.lastChild;
    if (node.id == "menudiv") {
        window.alert("Need to have atleast one menu...");
    }
    else {
        parent.removeChild(node);
    }
}

//function sendData() {
//    var restaurantName = $("#restaurantName").val();
//    var area = $("#area").val();
//    var city = $("#city").val();
//    var pinCode = $("#pinCode").val();
//    var phoneNumber = $("#phoneNumber").val();
//    var contactName = $("#contactName").val();
//    var deliveryTime = $("#deliveryTime").val();
//    var cost42 = $("#cost42").val();
//    var restaurantImage = $("#restaurantImage").val();

//    var menuData = [];
//    $('.clone').each(function () {
//        debugger;
//        var foodName = $(this).find("#foodName").val();
//        var description = $(this).find("#description").val();
//        var menuImage = $(this).find("#menuImage").val();
//        var price = $(this).find("#price").val();
//        var isVeg = $(this).find("#isVeg").val();
//        var menuItem = {
//            RestaurantId: 0,
//            FoodName: foodName,
//            Description: description,
//            Image: menuImage,
//            Price: price,
//            IsVeg: isVeg
//        };
//        menuData.push(menuItem);
//    });
//    console.log(menuData);

//    var data = {
//        Name: restaurantName,
//        Area: area,
//        City: city,
//        PinCode: pinCode,
//        PhoneNumber: phoneNumber,
//        ContactPersonName: contactName,
//        MinDeliveryTime: deliveryTime,
//        CostForTwo: cost42,
//        Image: restaurantImage,
//        Menus: menuData
//    }
//    console.log(data);
//    $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: "post",
//        url: "/Restaurant/Create",
//        data: JSON.stringify({ obj: data }),
//        success: function (response) {
//            console.log(response);
//        }
//    })
//}
