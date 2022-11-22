// thanks to https://jsfiddle.net/UselessCode/qm5AG/ as a starting point
(function () {
    // Some code for initialization on page load here.
})();

var icsFile = null;
function downloadCourtCase(elem, id) {
    var tr = $(elem).parent().parent();
    var tds = $(tr).find("td");
    var eventDate = {
        start: {
            day: $(tds[4]).text(),
            time: $(tds[5]).text()
        },
        end: {
            day: $(tds[4]).text(),
            time: $(tds[5]).text()
        }
    };
    var summary = "Hearing";
    var description =
        "CourtCaseNo:" + $(tds[1]).text() +
        ",=0D=0A Chamber ID: " + $(tds[3]).text() +
        ",=0D=0A Hearing Type: " + $(tds[6]).text();
    var location = $(tds[2]).text();
    var a = document.createElement('a');
    a.download = 'scheduler.ics';
    a.href = makeIcsFile(eventDate, summary, description, location);
    a.click();

    $('.toast-body').text("Downloaded sucessuflly!");
    $('.toast').toast({ animation: true, delay: 2000 });
    $('.toast').toast('show');
    
    return;
}
function convertDate(date) {
    var event = new Date(date).toISOString();
    event = event.split("T")[0];
    event = event.split("-");
    event = event.join("");
    return event;
}

function customConvertDate(date) {
    var event = date;
    event = event.split("-");
    event = event.join("");
    return event;
}

function customConvertTime(time, hour) {
    if (time == "") {
        return "000000";    
    }
    var event = time;
    event = event.split(":");
    if (hour != 0) {
        if (event[0] == "00") {
            event[0] = "02";
        } else if (event[0] == "01") {
            event[0] = "03";
        } else if (event[0] == "02") {
            event[0] = "04";
        } else if (event[0] == "03") {
            event[0] = "05";
        } else if (event[0] == "04") {
            event[0] = "06";
        } else if (event[0] == "05") {
            event[0] = "07";
        } else if (event[0] == "06") {
            event[0] = "08";
        } else if (event[0] == "07") {
            event[0] = "09";
        } else if (event[0] == "08") {
            event[0] = "10";
        } else if (event[0] == "09") {
            event[0] = "11";
        } else if (event[0] == "10") {
            event[0] = "12";
        } else if (event[0] == "11") {
            event[0] = "13";
        } else if (event[0] == "12") {
            event[0] = "14";
        } else if (event[0] == "13") {
            event[0] = "15";
        } else if (event[0] == "14") {
            event[0] = "16";
        } else if (event[0] == "15") {
            event[0] = "17";
        } else if (event[0] == "16") {
            event[0] = "18";
        } else if (event[0] == "17") {
            event[0] = "19";
        } else if (event[0] == "18") {
            event[0] = "20";
        } else if (event[0] == "19") {
            event[0] = "21";
        } else if (event[0] == "20") {
            event[0] = "22";
        } else if (event[0] == "21") {
            event[0] = "23";
        } else if (event[0] == "22") {
            event[0] = "24";
        } else if (event[0] == "23") {
            event[0] = "01";
        } else if (event[0] == "24") {
            event[0] = "02";
        }
    }
    event = event.join("");
    event = event + "00";
    return event;
}
function generateUniqSerial() {
    return 'xxxxxxxx-xxxx-xxx-xxxx-xxxxxxxxxxxx'.replace(/[x]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
function makeIcsFile(date, summary, description, location) {
    description = description.replace("=0D=0A", "\\n");
    var test =
        "BEGIN:VCALENDAR\n" +
        "CALSCALE:GREGORIAN\n" +
        "METHOD:PUBLISH\n" +
        "PRODID:-//Test Cal//EN\n" +
        "VERSION:2.0\n" +
        "BEGIN:VEVENT\n" +
        "LOCATION: " + location + "\n" +
        "SUMMARY:" +
        summary +
        "\n" +
        "DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" +
        //"DESCRIPTION:" +
        description +
        "\n" +
        "UID:" + generateUniqSerial() + "\n" +
        "DTSTART;VALUE=DATE:" +
        customConvertDate(date.start.day) + "T" + customConvertTime(date.start.time, 0) + 
        "\n" +
        "DTEND;VALUE=DATE:" +
        customConvertDate(date.end.day) + "T" + customConvertTime(date.end.time, 1)
        "\n" +
        "END:VEVENT\n" +
        "END:VCALENDAR";
    var data = new File([test], { type: "text/calendar" });
    // If we are replacing a previously generated file we need to
    // manually revoke the object URL to avoid memory leaks.
    if (icsFile !== null) {
        window.URL.revokeObjectURL(icsFile);
    }

    icsFile = window.URL.createObjectURL(data);
    
    return icsFile;
}
