// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const icons = ["../../background/icons/day-night.png", "../../background/icons/heart1.png", "../../background/icons/temp-avg1.png", "../../background/icons/sunrise1.png", "../../background/icons/sunset1.png", "../../background/icons/fos2.png", "../../background/icons/sunrise1.png", "../../background/icons/sunset1.png"];
let rowCounter = 0;
let rowIndex = []
let dataType = "";
let manufacturerName;
let ProtocolVal;
let sensorDiv;
let notClicked = true;
let manufactuterName;
function getPostData(manufacturer) {
    var tmp;
    console.log(manufacturer);
    if (manufacturer === null || manufacturer === undefined) {
        
    }
    else {
        manufactuterName = manufacturer;
    }
    $.ajax({
        type: "GET",
        url: "/Home/getJson",
        dataType: "json"
    }).done(function (data) {
        if (tmp != data) {
            tmp = data;
        }
        if (Object.keys(data).length !== 0) {
            document.getElementById("content-wrapper").innerHTML = "";
            Object.keys(data).forEach(function (key) {

                document.getElementById("rightSideExtraData").style.display = "none";

                if (manufactuterName.toLowerCase() == "wario") {
                    if (data[key]["name"]) {
                        if (data[key]["name"].toLowerCase() === "wind direction") {
                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-compass";
                            icon.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "humidity"){
                            
                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-moisture";
                            icon.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "pressure") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-speedometer";
                            icon.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "light") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-lightbulb";
                            icon.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "wind speed") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-tornado";
                            icon.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "rain") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-cloud-rain";
                            icon.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "dew point") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-thermometer";
                            icon.style.fontSize = "1.4em";
                            var temp = document.createElement("i")
                            temp.className = "bi bi-droplet";
                            temp.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            element.append(temp);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "t apparent") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-thermometer";
                            icon.style.fontSize = "1.4em";
                            var temp = document.createElement("i")
                            temp.className = "bi bi-person";
                            temp.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            element.append(temp);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "wind gust") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-tornado";
                            icon.style.fontSize = "1.4em";
                            var temp = document.createElement("i")
                            temp.className = "bi bi-compass";
                            temp.style.fontSize = "1.4em";
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            element.append(temp);
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "exposure ideal") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi bi-lightbulb";
                            icon.style.fontSize = "1.4em";
                            
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
   
                            document.getElementById("content-wrapper").appendChild(element);
                        }
                        else if (data[key]["name"].toLowerCase() === "t28c04004b") {

                            var element = document.createElement("div");
                            var br1 = document.createElement("br");
                            var br2 = document.createElement("br");
                            var icon = document.createElement("i")
                            icon.className = "bi-thermometer";
                            icon.style.fontSize = "1.4em";
  
                            //documentFragment.appendChild(element);
                            element.className = "content-wrapper-item";

                            var name = data[key]["name"];
                            //name = name.replace('_', ' ');
                            //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                            var value = data[key]["value"];
                            element.append(name);
                            element.append(br1);
                            element.append(value);
                            element.append(br2);
                            element.append(icon);
                            document.getElementById("content-wrapper").appendChild(element);
                        }

                    }
                }
                else {
                    if (data[key]["name"]) {
                        document.getElementById("rightSideExtraData").style.display = "none";
                        var element = document.createElement("div");
                        var br = document.createElement("br");
                        //documentFragment.appendChild(element);
                        element.className = "content-wrapper-item";

                        var name = data[key]["name"];
                        //name = name.replace('_', ' ');
                        //name = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
                        var value = data[key]["value"];
                        element.append(name);
                        element.append(br);
                        element.append(value);

                        document.getElementById("content-wrapper").appendChild(element);
                    }
                }
            });
        }
    });
};

function setDate() {
    var imageParent = document.getElementById("date");
    imageParent.innerHTML = "";
    var p = document.createElement("p");
    p.className = "dateSet";
    var dt = new Date();
    var date = dt.toDateString().slice(0, 15);
    var day = date.slice(0, 3);
    var dayNum = date.slice(8, 10);
    var month = date.slice(4, 7);
    var year = date.slice(11, 15);
    p.innerHTML = day + ', ' + dayNum + ' ' + month + ' ' + year;
    imageParent.appendChild(p);
}
function setTime() {
    var dt = new Date();
    var imageParent = document.getElementById("time");
    document.getElementById("time").innerHTML = "";
    var p = document.createElement("p");
    p.className = "timeSet";
    var time = dt.toTimeString().slice(0, 5);
    p.innerHTML = time;
    document.getElementById("time").appendChild(p);
}
function greetings(name) {
    console.log(name);
    var dt = new Date();
    if (dt.getHours() >= 6 && dt.getHours() <= 12) {
        document.getElementById("greetings-text").innerHTML = "Good morning " + name + "!";
    }
    else if (dt.getHours() >= 13 && dt.getHours() <= 18) {
        document.getElementById("greetings-text").innerHTML = "Good afternoon " + name + "!";
    }
    else {
        document.getElementById("greetings-text").innerHTML = "Good evening " + name + "!";
    }
    
}

function fillIcons() {
    
    console.log(icons.length)
    for (let i = 0; i < icons.length; i++) {
        var p = document.createElement("p");
        var imageParent = document.getElementById("icons");
        var element = document.createElement("img");
        element.src = icons[i];
        element.className = "icons";
        p.append(element);
        imageParent.appendChild(p);
    }
}
function fillText(array) {
    if (array === 0 || array === null || array === undefined) {
        console.log("array null");
    }
    else { 
        for (let i = 0; i < icons.length; i++) {
            if (i === 1) {
                var p = document.createElement("p");
                var imageParent = document.getElementById("icons-text1");
                p.innerHTML = array[i];
            
                //imageParent.appendChild(p);
            }
            else {
                var p = document.createElement("p");
                var imageParent = document.getElementById("icons-text");
                p.innerHTML = array[i];
                p.className = "pStyle";
                //imageParent.appendChild(p); 
            }
       
            }
    }
}
var counter = 1;
function addSensorRow() {
    $('#btnSensor').click(function () {
        $('<tr id="tablerow' + counter + '"><td>' +
            '<input type="text" class="text-box single-line" name="ClientSampleID[' + counter + ']" value="" required="required" />' +
            '</td>' +
            '<td>' +
            '<button type="button" class="btn btn-primary" onclick="removeTr(' + counter + ');">Delete</button>' +
            '</td>' +
            '</tr>').appendTo('#addSensor');
        counter++;
        return false;
        });
}
function removeTr(index) {
    if (counter > 1) {
        $('#tablerow' + index).remove();
        counter--;
    }
    return false;
}

function DeviceAdd() {

    rowIndex.push("item" + rowCounter);
    const itemId = "item" + rowCounter++;
    

    const removeButton = document.createElement("button");
    const removeIcon = document.createElement("i");
    removeIcon.className = "bi bi-trash2";
    removeButton.appendChild(removeIcon);
    removeButton.className = "btn btn-outline-danger";
    removeButton.type = "button";
    removeButton.id = itemId;
    //impelement
    removeButton.onclick = function () { removeItemID(removeButton.id);}

    const itemName = document.createElement("input");
    itemName.id = itemId + "_name";
    itemName.name = "ItemName";
    itemName.className = "form-control";
    itemName.required = true;
    itemName.autocomplete = "off";
    //ItemNameInput.onkeyup = function () {
    //FillFromItemsList(itemId);
    //};

    //columns
    const removeButtonColumn = document.createElement("div");
    removeButtonColumn.className = "column-1";
    removeButtonColumn.appendChild(removeButton);

    const ItemNameColumn = document.createElement("div");
    ItemNameColumn.className = "column-3";
    ItemNameColumn.style ="padding-left:10px;"
    ItemNameColumn.appendChild(itemName);

    //rows
    const itemRow = document.createElement("div");
    itemRow.className = "row justify-content-center";
    itemRow.style = "padding-top: 30px;"
    itemRow.appendChild(ItemNameColumn);
    itemRow.appendChild(removeButtonColumn);
    itemRow.id = itemId;

    const parent = document.getElementById("DeviceItems");
    parent.appendChild(itemRow);
}


function DeviceAddWithValues(name) {
    rowIndex.push("item" + rowCounter);
    const itemId = "item" + rowCounter++;


    const removeButton = document.createElement("button");
    const removeIcon = document.createElement("i");
    removeIcon.className = "bi bi-trash2";
    removeButton.appendChild(removeIcon);
    removeButton.className = "btn btn-outline-danger";
    removeButton.type = "button";
    removeButton.id = itemId;
    //impelement
    removeButton.onclick = function () { removeItemID(removeButton.id); }

    const itemName = document.createElement("input");
    itemName.id = itemId + "_name";
    itemName.name = "ItemName";
    itemName.className = "form-control";
    itemName.required = true;
    itemName.value = name;
    //ItemNameInput.onkeyup = function () {
    //FillFromItemsList(itemId);
    //};

    //columns
    const removeButtonColumn = document.createElement("div");
    removeButtonColumn.className = "column-1";
    removeButtonColumn.appendChild(removeButton);

    const ItemNameColumn = document.createElement("div");
    ItemNameColumn.className = "column-3";
    ItemNameColumn.style = "padding-left:10px;"
    ItemNameColumn.appendChild(itemName);

    //rows
    const itemRow = document.createElement("div");
    itemRow.className = "row justify-content-center";
    itemRow.style = "padding-top: 30px;"
    itemRow.appendChild(ItemNameColumn);
    itemRow.appendChild(removeButtonColumn);
    itemRow.id = itemId;

    const parent = document.getElementById("DeviceItems");
    parent.appendChild(itemRow);
}
function removeItemID(ID) {
    if (rowIndex.length > 1) { 
        var rowItem = document.getElementById(ID);
        rowItem.remove();
        for (var item in rowIndex) {
            if (rowIndex[item] == ID) {
                rowIndex.splice(item,1);
            }
        }
    }
}
function changeButtonName() {
    $('.dropdown-menu a').click(function () {
        document.getElementById("test123").innerHTML = $(this).text();
        document.getElementById("test123").value = $(this).text();
    });
}

function onAdressChange(data) {
    sensorDiv = document.getElementById("SensorsDiv");
    sensorDiv.innerHTML = "";
    if (manufacturerName == "Wario") {
        if (data === "XML") {
            if (ProtocolVal === "IP") {
                //value from input form Address
                var input = document.getElementById("inputAdd");
                let url = input.value;
                // Option selector

                var labelChoose = document.createElement("label");
                labelChoose.innerHTML = "Choose one or more from sensors below";


                var sensors = document.createElement("select");
                sensors.className = "form-select";
                sensors.name = "sensorsSelect";
                sensors.multiple = "multiple";
                sensors.style = "color:white";
                //new element for input

                sensorDiv.appendChild(labelChoose);
                var rightBox = [];
                var dict = [];
                let counter = 0;
                fetch(url)
                    .then(function (resp) {
                        return resp.text();
                    })
                    .then(function (data) {
                        //console.log(data);
                        let parser = new DOMParser(), xmlDoc = parser.parseFromString(data, 'text/xml');
                        let names = xmlDoc.getElementsByTagName("name");
                        let values = xmlDoc.getElementsByTagName("value");


                        var dayNight = xmlDoc.getElementsByTagName("isday");
                        var bio = xmlDoc.getElementsByTagName("bio");
                        var sunrise = xmlDoc.getElementsByTagName("sunrise");
                        var sunset = xmlDoc.getElementsByTagName("sunset");
                        var fog = xmlDoc.getElementsByTagName("fog");
                        var astroStart = xmlDoc.getElementsByTagName("astrostart");
                        var astroEnd = xmlDoc.getElementsByTagName("astroend");

                        /*
                        rightBox.push(bio.item(0).innerHTML);
                        rightBox.push(sunrise.item(0).innerHTML);
                        rightBox.push(sunset.item(0).innerHTML);
                        rightBox.push(fog.item(0).innerHTML);
                        rightBox.push(astroStart.item(0).innerHTML);
                        rightBox.push(astroEnd.item(0).innerHTML);
                        fillText(rightBox);*/
                        //console.log(names);
                        for (let item of names) {
                            if (values[counter].innerHTML !== "INACTIVE" || !item.innerHTML.startsWith("PING")) {
                                if (!item.innerHTML.startsWith("OU")) {
                                    dict.push(item.innerHTML);
                                }
                            }
                            counter++;
                        }

                        console.log(dict);
                        //co je potreba dodelat
                        //bude nejspise potreba si udelat nejaky dict nebo neco, do ktereho si prvne pridas jako klic nazev senzoru a jako hodnotu hodnotu z daneho senzoru. To potom projdes a pokud bude klic out nebo inactive, tak smazes jak klic, tak hodnotu
                        for (var i = 0; i < dict.length; i++) {
                            var element = document.createElement("option");
                            element.value = dict[i];
                            element.innerHTML = dict[i];
                            sensors.appendChild(element);

                        }
                        sensorDiv.appendChild(sensors);

                    });
            }
            else {
                console.log("not IP protocol");
                sensorDiv.innerHTML = "";
            }
        }
        else if (data ==="URL"){
            console.log(" im in url");
            sensorDiv.innerHTML = "";
            var labelURL = document.createElement("label");
            labelURL.innerHTML = "Input data format";
            var inputText = document.createElement("input");
            inputText.className = "form-control";
            inputText.name = "URLFormat";
            
            sensorDiv.appendChild(labelURL);
            sensorDiv.appendChild(inputText);
        }
    }
    else {
        console.log("not implemented manufactuerer");
        sensorDiv.innerHTML = "";
    }
    //element.value = input.value;
    //element.innerHTML = input.value;
    //console.log(input.value);

    
}

function Manufacturer(manufacturer) {
    manufacturerName = manufacturer;
}
function protocolValue(value) {
    ProtocolVal = value;
}

function EditSensors(usedSensors) {

    
    if (notClicked) {
        notClicked = false;
        document.getElementById("selectNewSensors").style.display = "block";
        var sensorsDiv = document.getElementById("selectNewSensors");
        sensorsDiv.innerHTML = "";


        var input = document.getElementById("editAddressInput");
        let url = input.value;
        let counter = 0;

        var sensors = document.createElement("select");
        sensors.className = "form-select";
        sensors.name = "editSensorsSel";
        sensors.multiple = "multiple";
        sensors.style = "color:white";
        let dict = [];

        fetch(url)
            .then(function (resp) {
                return resp.text();
            })
            .then(function (data) {
                //console.log(data);
                let parser = new DOMParser(), xmlDoc = parser.parseFromString(data, 'text/xml');
                let names = xmlDoc.getElementsByTagName("name");
                let values = xmlDoc.getElementsByTagName("value");

                for (let item of names) {
                    if (values[counter].innerHTML !== "INACTIVE" || !item.innerHTML.startsWith("PING")) {
                        if (!item.innerHTML.startsWith("OU")) {
                            dict.push(item.innerHTML);
                        }
                    }
                    counter++;
                }


                var textDict = [];
                for (var i = 0; i < dict.length; i++) {
                    for (var j = 0; j < usedSensors.length; j++) {
                        if (dict[i] === usedSensors[j]) {
                            if (i === 0) {
                                dict.splice(i, i+1);
                            }

                            dict.splice(i,i);
                        }
                        
                    }
                }


                for (var i = 0; i < dict.length; i++) {
                    var element = document.createElement("option");
                    element.value = dict[i];
                    element.innerHTML = dict[i];
                    sensors.appendChild(element);

                }
                sensorsDiv.appendChild(sensors);

            });
    }
    else {
        document.getElementById("selectNewSensors").style.display = "none";
        notClicked = true;
    }
}

function getStations() {
    $.ajax({
        type: "GET",
        url: "/Meteo/getJsonDevices",
        dataType: "json"
    }).done(function (data) {
        console.log(data);
        if (Object.keys(data).length !== 0) {
            Object.keys(data).forEach(function (key) {
                

                
            });
        }
    });
}