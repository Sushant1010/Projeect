function getCalendarDivString(t) {
    var e = '<div id="ndp-nepali-box" class="ndp-corner-all" style="display:none">';
    return e += '<span id="ndp-target-id" style="display:none"></span>', e += '<div class="ndp-corner-all ndp-header">', e += '<a href="javascript:void(0)" id="prev" title="Previous Month" class="ndp-prev"></a>', e += '<a href="javascript:void(0)" id="next" title="Next Month" class="ndp-next"></a>', e += '<span id="currentMonth"></span>', e += "</div>", e += '<div id="npd-table-div">', e += "<table>", e += '<tr class="ndp-days">', e += "<th>आ</th>", e += "<th>सो</th>", e += "<th>मं</th>", e += "<th>बु</th>", e += "<th>बि</th>", e += "<th>शु</th>", e += "<th>श</th>", e += "</tr>", e += "</table>", e += "</div>", e += "</div>"
}

function showNdpCalendarBox(t) {
    if (npdCalendarVisible) hideCalendarBox(!1);
    else {
        ndpData[t] && (ndpAttr = ndpData[t]);
        var e = $("#" + t).val();
        $("#ndp-target-id").html(t);
        var r = $("#" + t).offset();
        $("#ndp-nepali-box").css("top", r.top + $("#" + t).outerHeight()), $("#ndp-nepali-box").css("left", r.left), showCalendar(e), npdCalendarVisible = !0
    }
}

function setSelectedDay(t) {
    var e = $("#ndp-target-id").html();
    $("#" + e).val(t), ndpAttr.ndpEnglishInput && $("#" + ndpAttr.ndpEnglishInput).val(BS2AD(t)), hideCalendarBox()
}

function showCalendar(t) {
    $("#ndp-nepali-box table").find("tr:gt(0)").remove(), "" === t ? $("#ndp-nepali-box table").append(getDateTable("")) : $("#ndp-nepali-box table").append(getDateTable(t)), "block" == $("#ndp-nepali-box").css("display") && $("#ndp-nepali-box").hide(), $("#ndp-nepali-box").fadeIn(100)
}

function getDateTable(t) {
    var e = "",
        r = "";
    if ("" === t) {
        var n = getNepaliDate();
        return e = getMonthParameters(n), r = getDateRows(e[0], e[1], e[2], e[3], e[4])
    }
    return e = getMonthParameters(t), r = getDateRows(e[0], e[1], e[2], e[3], e[4])
}

function performSelectMonth(t) {
    showCalendar((ndpAttr.npdYear ? $("#npd-year-select").val() : t) + "-" + $("#npd-month-select").val() + "-01")
}

function performSelectYear(t) {
    showCalendar($("#npd-year-select").val() + "-" + (ndpAttr.npdMonth ? $("#npd-month-select").val() : t) + "-01")
}

function getMonthSelect(t, e) {
    var r = getNepaliMonthsInNepali(),
        n = '<select id="npd-month-select" onchange="performSelectMonth(' + e + ')">';
    return r.forEach(function(e, r) {
        n += '<option value="' + (r + 1) + '"' + (r + 1 == t ? " selected" : "") + ">" + e + "<li>"
    }), n += "<select>"
}

function getYearSelect(t, e, r) {
    console.log(r);
    var n = '<select id="npd-year-select" onchange="performSelectYear(' + t + ')">';
    for (i = Math.round(r / 2) > 0 && parseInt(e) - Math.round(r / 2) >= 2e3 ? parseInt(e) - Math.round(r / 2) : 2e3; i <= (Math.round(r / 2) > 0 && parseInt(e) + Math.round(r / 2) <= 2090 ? parseInt(e) + Math.round(r / 2) : 2090); i++) n += '<option value="' + i + '"' + (e == i ? " selected" : "") + ">" + englishNo2Nep(i) + "<li>";
    return n += "<select>"
}

function getMonthParameters(t) {
    var e = t.split("-"),
        r = e[0],
        n = e[1],
        a = ndpAttr.npdYearCount || 0;
    ndpAttr.npdMonth && ndpAttr.npdYear ? $("#currentMonth").html(getMonthSelect(n, r) + getYearSelect(n, r, a)) : ndpAttr.npdMonth ? $("#currentMonth").html(getMonthSelect(n, r) + " " + englishNo2Nep(r)) : ndpAttr.npdYear ? $("#currentMonth").html(getNepaliMonth(n - 1) + " " + getYearSelect(n, r, a)) : $("#currentMonth").html(getNepaliMonth(n - 1) + "&nbsp;" + englishNo2Nep(r)), nYear = pYear = r, nMonth = parseInt(n, 10) + 1, nMonth > 12 && (nYear++, nMonth = "01"), pMonth = parseInt(n, 10) - 1, pMonth < 1 && (pYear--, pMonth = "12"), $("#prev").attr("onclick", "showCalendar('" + pYear + "-" + pMonth + "-01')"), $("#next").attr("onclick", "showCalendar('" + nYear + "-" + nMonth + "-01')");
    var s = e[2],
        i = numberOfBsDays(r, n - 1),
        o = new NepaliDateConverter,
        d = n + "/1/" + r,
        h = o.bs2ad(d),
        u = h.split("-"),
        p = u[0],
        l = u[1],
        b = u[2],
        y = new Date(p, l - 1, b),
        c = y.getDay();
    return new Array(c, i, r, n, s)
}

function getDateRows(t, e, r, n, a) {
    for (var s = getNepaliDate(), i = s.split("-"), o = i[0], d = get2DigitNo(i[1]), h = get2DigitNo(i[2]), u = "", p = 0; t + e > p; p++) {
        p % 7 === 0 && (u += "<tr>");
        var l = p - t + 1,
            b = r.toString() + "-" + get2DigitNo(n) + "-" + get2DigitNo(l),
            y = "";
        y = r == o && get2DigitNo(n) == d && h == get2DigitNo(l) ? "ndp-selected" : l == a ? "ndp-current" : "ndp-date", t > p ? u += "<td></td>\n" : (u += "<td class='" + y + "'>", u += "<a href='javascript:void(0)' onclick=\"setSelectedDay('" + b + "')\">" + englishNo2Nep(l) + "</a>", u += "</td>\n"), p % 7 == 6 && (u += "</tr>\n")
    }
    return u
}

function hideCalendarBox(t) {
    t = "undefined" != typeof t ? t : !0, $("#ndp-nepali-box").fadeOut(100), npdCalendarVisible = !1, t && ndpAttr.onChange && ndpAttr.onChange()
}

function get2DigitNo(t) {
    return t = parseInt(t, 10), 10 > t ? "0" + t.toString() : t.toString()
}

function getMonths() {
    var t = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
    return t
}

function getNepaliMonths() {
    var t = new Array("Baisakh", "Jestha", "Ashar", "Shrawan", "Bhadra", "Ashoj", "Kartik", "Mangsir", "Poush", "Magh", "Falgun", "Chaitra");
    return t
}

function getNepaliDaysShort() {
    var t = new Array("आ", "सो", "मं", "बु", "बि", "शु", "श");
    return t
}

function getNepaliDayFromDate(t) {
    var e = t.split("-"),
        r = e[2],
        n = e[1],
        a = e[0],
        s = new Date(a, n - 1, r),
        i = s.getDay(),
        o = new Array("आइतबार", "सोमवार", "मंगलबार", "बुधवार", "बिहीबार", "शुक्रबार", "शनिबार ");
    return o[i]
}

function getNepaliMonthsInNepali() {
    return new Array("बैशाख", "जेठ", "अषाढ", "श्रावण", "भाद्र", "आश्विन", "कार्तिक", "मङ्सिर", "पौष", "माघ", "फाल्गुन", "चैत्र")
}

function getNepaliMonth(t) {
    t = parseInt(t, 10);
    var e = getNepaliMonthsInNepali();
    return e[t]
}

function getCurrentDayName() {
    var t = new Date,
        e = t.getDay(),
        r = new Array(7);
    return r[0] = "Sunday", r[1] = "Monday", r[2] = "Tuesday", r[3] = "Wednesday", r[4] = "Thursday", r[5] = "Friday", r[6] = "Saturday", r[e]
}

function getDayFromDate(t) {
    var e = t.split("-"),
        r = e[2],
        n = e[1],
        a = e[0],
        s = new Date(a, n - 1, r),
        i = s.getDay(),
        o = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat");
    return o[i]
}

function numberOfBsDays(t, e) {
    var r = new NepaliDateConverter;
    return r.bs[t][e]
}

function numberOfDays(t, e) {
    var r = new Date(t, e, 0);
    return r.getDate()
}

function AD2BS(t) {
    var e = new NepaliDateConverter;
    return e.ad2bs(getNepaliFormat(t))
}

function BS2AD(t) {
    var e = new NepaliDateConverter;
    return e.bs2ad(getNepaliFormat(t))
}

function getNepaliDate() {
    var t = new NepaliDateConverter;
    return t.ad2bs(getDateInNo("/"))
}

function getDateInWords() {
    var t = getMonths(),
        e = new Date,
        r = e.getDate(),
        n = e.getMonth() + 1,
        a = e.getYear(),
        s = 1e3 > a ? a + 1900 : a;
    return r + " " + t[n] + ", " + s
}

function getDateInNo(t) {
    var e = new Date,
        r = e.getDate(),
        n = e.getMonth() + 1,
        a = e.getFullYear();
    return n + t + r + t + a
}

function getNepaliFormat(t) {
    var e = t.split("-"),
        r = e[2],
        n = e[1],
        a = e[0];
    return n + "/" + r + "/" + a
}

function getAdDateInWords(t) {
    var e = getMonths(),
        r = t.split("-"),
        n = r[2],
        a = r[1],
        s = r[0];
    return n + " " + e[a] + ", " + s
}

function getNepaliDateInWords(t) {
    var e = getNepaliMonthsInNepali(),
        r = t.split("-"),
        n = r[2],
        a = r[1],
        s = r[0];
    return englishNo2Nep(s) + " " + e[a - 1] + " " + englishNo2Nep(n) + " गते"
}

function getCurrentYear() {
    var t = new Date,
        e = t.getFullYear();
    return e
}

function getCurrentMonth() {
    var t = new Date,
        e = t.getMonth() + 1;
    return e
}

function getCurrentDay() {
    var t = new Date,
        e = t.getDate();
    return e
}

function makeArray() {
    for (i = 0; i < makeArray.arguments.length; i++) this[i + 1] = makeArray.arguments[i]
}

function englishNo2Nep(t) {
    t = t.toString();
    for (var e = "", r = 0; r < t.length; r++) e += convertNos(t[r]);
    return e
}

function convertNos(t) {
    switch (t) {
        case "०":
            return 0;
        case "१":
            return 1;
        case "२":
            return 2;
        case "३":
            return 3;
        case "४":
            return 4;
        case "५":
            return 5;
        case "६":
            return 6;
        case "७":
            return 7;
        case "८":
            return 8;
        case "९":
            return 9;
        case "0":
            return "०";
        case "1":
            return "१";
        case "2":
            return "२";
        case "3":
            return "३";
        case "4":
            return "४";
        case "5":
            return "५";
        case "6":
            return "६";
        case "7":
            return "७";
        case "8":
            return "८";
        case "9":
            return "९"
    }
}

function NepaliDateConverter() {
    this.bs_date_eq = "09/17/2000", this.ad_date_eq = "01/01/1944", this.bs = [], this.bs[2e3] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2001] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2002] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2003] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2004] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2005] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2006] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2007] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2008] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31), this.bs[2009] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2010] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2011] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2012] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2013] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2014] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2015] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2016] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2017] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2018] = new Array(31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2019] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2020] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2021] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2022] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2023] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2024] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2025] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2026] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2027] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2028] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2029] = new Array(31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30), this.bs[2030] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2031] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2032] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2033] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2034] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2035] = new Array(30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31), this.bs[2036] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2037] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2038] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2039] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2040] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2041] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2042] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2043] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2044] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2045] = new Array(31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2046] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2047] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2048] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2049] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2050] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2051] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2052] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2053] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2054] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2055] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2056] = new Array(31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30), this.bs[2057] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2058] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2059] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2060] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2061] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2062] = new Array(30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31), this.bs[2063] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2064] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2065] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2066] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31), this.bs[2067] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2068] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2069] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2070] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2071] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2072] = new Array(31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2073] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2074] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2075] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2076] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2077] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2078] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2079] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2080] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2081] = new Array(31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2082] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2083] = new Array(31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2084] = new Array(31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2085] = new Array(31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30), this.bs[2086] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2087] = new Array(31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30), this.bs[2088] = new Array(30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30), this.bs[2089] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2090] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.count_ad_days = count_ad_days, this.count_bs_days = count_bs_days, this.add_bs_days = add_bs_days, this.add_ad_days = add_ad_days, this.bs2ad = bs2ad, this.ad2bs = ad2bs
}

function count_ad_days(t, e) {
    var r = 864e5,
        n = t.split("/"),
        a = e.split("/");
    n[2] = Number(n[2]), n[1] = Number(n[1]), n[0] = Number(n[0]), a[2] = Number(a[2]), a[1] = Number(a[1]), a[0] = Number(a[0]);
    var s = new Date(n[2], n[0] - 1, n[1]),
        i = new Date(a[2], a[0] - 1, a[1]),
        o = Math.ceil((i.getTime() - s.getTime()) / r);
    return o
}

function count_bs_days(t, e) {
    var r = t.split("/"),
        n = e.split("/"),
        a = Number(r[2]),
        s = Number(r[0]),
        i = Number(r[1]),
        o = Number(n[2]),
        d = Number(n[0]),
        h = Number(n[1]),
        u = 0,
        p = 0;
    for (p = a; o >= p; p++) u += arraySum(this.bs[p]);
    for (p = 0; s > p; p++) u -= this.bs[a][p];
    for (u += this.bs[a][11], p = d - 1; 12 > p; p++) u -= this.bs[o][p];
    return u -= i + 1, u += h - 1
}

function add_ad_days(t, e) {
    var r = new Date(t);
    return r.setDate(r.getDate() + e), ad_month = r.getMonth() + 1, ad_day = r.getDate(), r.getFullYear() + "-" + (ad_month < 10 ? "0" + ad_month.toString() : ad_month) + "-" + (ad_day < 10 ? "0" + ad_day.toString() : ad_day)
}

function add_bs_days(t, e) {
    var r = t.split("/"),
        n = Number(r[2]),
        a = Number(r[0]),
        s = Number(r[1]);
    for (s += e; s > this.bs[n][a - 1];) s -= this.bs[n][a - 1], a++, a > 12 && (a = 1, n++);
    return n + "-" + (10 > a ? "0" + a.toString() : a) + "-" + (10 > s ? "0" + s.toString() : s)
}

function bs2ad(t) {
    return days_count = this.count_bs_days(this.bs_date_eq, t), this.add_ad_days(this.ad_date_eq, days_count)
}

function ad2bs(t) {
    return days_count = this.count_ad_days(this.ad_date_eq, t), this.add_bs_days(this.bs_date_eq, days_count)
}
ndpAttr = {}, npdCalendarVisible = "", npdIgnoreMouseUp = !1, ndpData = [],
    function(t) {
        t.fn.nepaliDatePicker = function(e) {
            e = "undefined" != typeof e ? e : {}, ndpAttr = e, this.each(function() {
                var r = t(this).attr("id");
                t(this).addClass("ndp-nepali-calendar"), ndpData[r] = e, ndpAttr.onFocus !== !1 && t(this).attr("onfocus", "showNdpCalendarBox('" + r + "')"), ndpAttr.ndpTriggerButton && t(this).after('<button type="button" class="ndp-click-trigger ' + (ndpAttr.ndpTriggerButtonClass ? ndpAttr.ndpTriggerButtonClass : "") + '" onclick="showNdpCalendarBox(&quot;' + r + '&quot;)">' + (ndpAttr.ndpTriggerButtonText ? ndpAttr.ndpTriggerButtonText : "Pick Date") + "</button>")
            }), t("body").append(getCalendarDivString(ndpAttr)), t(".ndp-nepali-calendar, #ndp-nepali-box").hover(function() {
                mouse_is_inside = !0
            }, function() {
                mouse_is_inside = !1
            }), t("html").mouseup(function(e) {
                t(e.target).is(".ndp-click-trigger") || npdCalendarVisible && !mouse_is_inside && hideCalendarBox(!1)
            })
        }
    }(jQuery);
var mouse_is_inside = !1;
arraySum = function(t) {
    for (var e = 0, r = t.length; r; e += t[--r]);
    return e
};