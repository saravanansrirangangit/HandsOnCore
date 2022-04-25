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
        window.alert("need to have atleast one menu...");
    }
    else {
        parent.removeChild(node);
    }
}
