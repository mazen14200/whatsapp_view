//remove text node from element
function removeTextFromFirstLevelTag(element) {
    var ele = element[0];
    for (var i = 0; i < ele.childNodes.length; i++) {
        if (ele.childNodes[i].nodeType == 3)//TEXT_NODE
        {
            ele.removeChild(ele.childNodes[i]);
            i--;

        }
        else {
            continue;
        }
    }
}


