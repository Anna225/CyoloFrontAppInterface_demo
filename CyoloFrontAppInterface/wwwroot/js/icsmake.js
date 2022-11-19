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
    var summary = "Meeting";
    var description =
        "CourtCaseNo:" + $(tds[1]).text() +
        ", Jurisdiction:" + $(tds[2]).text() +
        ", Chamber ID: " + $(tds[4]).text() +
        ", Hearing Date: " + $(tds[4]).text() +
        ", Hearing Time: " + $(tds[5]).text() +
        ", Hearing Time: " + $(tds[6]).text();
    var a = document.createElement('a');
    a.download = 'scheduler.ics';
    a.href = makeIcsFile(eventDate, summary, description);
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

function customConvertTime(time) {
    if (time == "") {
        return "000000";    
    }
    var event = time;
    event = event.split(":");
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
function makeIcsFile(date, summary, description) {

    var test =
        "BEGIN:VCALENDAR\n" +
        "CALSCALE:GREGORIAN\n" +
        "METHOD:PUBLISH\n" +
        "PRODID:-//Test Cal//EN\n" +
        "VERSION:2.0\n" +
        "BEGIN:VEVENT\n" +
        "SUMMARY:" +
        summary +
        "\n" +
        "DESCRIPTION:" +
        description +
        "\n" +
        "UID:" + generateUniqSerial() + "\n" +
        "DTSTART;VALUE=DATE:" +
        customConvertDate(date.start.day) + "T" + customConvertTime(date.start.time) + 
        "\n" +
        "DTEND;VALUE=DATE:" +
        customConvertDate(date.end.day) + "T" + customConvertTime(date.end.time)
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
